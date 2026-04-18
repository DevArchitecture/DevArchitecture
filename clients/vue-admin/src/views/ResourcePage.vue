<script setup lang="ts">
import { computed, onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import Card from "primevue/card";
import Tag from "primevue/tag";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import InputText from "primevue/inputtext";
import Textarea from "primevue/textarea";
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import Checkbox from "primevue/checkbox";
import MultiSelect from "primevue/multiselect";
import Dropdown from "primevue/dropdown";
import { useToast } from "primevue/usetoast";
import { useConfirm } from "primevue/useconfirm";
import { authStore, crudApi } from "../services/api";
import { CLIENT_MODULES } from "../config/modules";

type FieldType = "text" | "textarea" | "number" | "boolean" | "lookup";
type RelationDialogType = "userClaims" | "userGroups" | "groupClaims" | "groupUsers" | "password";
type LookupOption = { label: string; value: number };

const extractRelationSelectedIds = (
  type: Exclude<RelationDialogType, "password">,
  relationRows: Record<string, unknown>[]
): number[] => {
  const pickId = (row: Record<string, unknown>, ...keys: string[]) => {
    for (const key of keys) {
      const value = Number(row[key] ?? 0);
      if (Number.isFinite(value) && value > 0) return value;
    }
    return 0;
  };
  const ids = relationRows
    .map((row) => {
      switch (type) {
        case "userClaims":
        case "groupClaims":
          return pickId(row, "id", "Id", "operationClaimId", "claimId");
        case "userGroups":
          return pickId(row, "id", "Id", "groupId");
        case "groupUsers":
          return pickId(row, "userId", "id", "Id");
        default:
          return 0;
      }
    })
    .filter((n) => n > 0);
  return [...new Set(ids)];
};

type FieldDef = {
  key: string;
  label: string;
  type: FieldType;
  lookupPath?: string;
  required?: boolean;
};

type ModuleDescriptor = {
  idKey: string;
  columns: string[];
  fields: FieldDef[];
  createClaim?: string;
  updateClaim?: string;
  deleteClaim?: string;
};

const MODULE_DESCRIPTORS: Record<string, ModuleDescriptor> = {
  user: {
    idKey: "userId",
    columns: ["userId", "fullName", "email", "status", "mobilePhones", "address", "notes"],
    fields: [
      { key: "fullName", label: "Full Name", type: "text", required: true },
      { key: "email", label: "Email", type: "text", required: true },
      { key: "mobilePhones", label: "Phone", type: "text" },
      { key: "address", label: "Address", type: "text" },
      { key: "notes", label: "Notes", type: "textarea" },
      { key: "status", label: "Status", type: "boolean" }
    ],
    createClaim: "CreateUserCommand",
    updateClaim: "UpdateUserCommand",
    deleteClaim: "DeleteUserCommand"
  },
  group: {
    idKey: "id",
    columns: ["id", "groupName"],
    fields: [{ key: "groupName", label: "Group Name", type: "text", required: true }],
    createClaim: "CreateGroupCommand",
    updateClaim: "UpdateGroupCommand",
    deleteClaim: "DeleteGroupCommand"
  },
  language: {
    idKey: "id",
    columns: ["id", "name", "code"],
    fields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "code", label: "Code", type: "text", required: true }
    ],
    createClaim: "CreateLanguageCommand",
    updateClaim: "UpdateLanguageCommand",
    deleteClaim: "DeleteLanguageCommand"
  },
  translate: {
    idKey: "id",
    columns: ["id", "langId", "code", "value"],
    fields: [
      { key: "langId", label: "Language", type: "lookup", lookupPath: "/languages", required: true },
      { key: "code", label: "Code", type: "text", required: true },
      { key: "value", label: "Value", type: "textarea", required: true }
    ],
    createClaim: "CreateTranslateCommand",
    updateClaim: "UpdateTranslateCommand",
    deleteClaim: "DeleteTranslateCommand"
  },
  operationclaim: {
    idKey: "id",
    columns: ["id", "name", "alias", "description"],
    fields: [
      { key: "name", label: "Name", type: "text" },
      { key: "alias", label: "Alias", type: "text" },
      { key: "description", label: "Description", type: "textarea" }
    ],
    updateClaim: "UpdateOperationClaimCommand"
  },
  log: {
    idKey: "id",
    columns: ["id", "level", "exceptionMessage", "timeStamp", "user", "value", "type"],
    fields: []
  }
};

const route = useRoute();
const toast = useToast();
const confirm = useConfirm();

const rows = ref<Record<string, unknown>[]>([]);
const selectedRow = ref<Record<string, unknown> | null>(null);
const showFormDialog = ref(false);
const formMode = ref<"create" | "edit">("create");
const formData = ref<Record<string, any>>({});
const formError = ref("");
const showRelationDialog = ref(false);
const relationDialogType = ref<RelationDialogType>("userClaims");
const relationSelectedIds = ref<number[]>([]);
const relationLookupOptions = ref<LookupOption[]>([]);
const relationLookupLoading = ref(false);
const passwordValue = ref("");
const confirmPasswordValue = ref("");
const tableFilterValue = ref("");
const filters = ref<Record<string, any>>({});
const languageLookupOptions = ref<LookupOption[]>([]);
const langIdToLabel = ref<Record<number, string>>({});

const moduleKey = computed(() => String(route.params.moduleKey ?? ""));
const moduleConfig = computed(() => CLIENT_MODULES.find((item) => item.key === moduleKey.value));
const moduleTitle = computed(() => {
  const fallback = moduleConfig.value?.label ?? "Module";
  return authStore.t(`menu.${moduleKey.value}`, authStore.t(fallback, fallback));
});
const t = (key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback));

const toastNotify = (severity: "success" | "info" | "warn" | "error", detail: string) => {
  const summary =
    severity === "success"
      ? t("notify.severity.success", "Success")
      : severity === "info"
        ? t("notify.severity.info", "Info")
        : severity === "warn"
          ? t("notify.severity.warning", "Warning")
          : t("notify.severity.error", "Error");
  const life = severity === "error" ? 3000 : severity === "warn" ? 2800 : 2000;
  toast.add({ severity, summary, detail, life });
};
const descriptor = computed<ModuleDescriptor>(() => {
  return MODULE_DESCRIPTORS[moduleKey.value] ?? {
    idKey: "id",
    columns: ["id"],
    fields: []
  };
});

const resetFilters = () => {
  const next: Record<string, any> = {
    global: { value: null, matchMode: "contains" }
  };
  for (const column of descriptor.value.columns) {
    next[column] = { value: null, matchMode: "contains" };
  }
  filters.value = next;
  tableFilterValue.value = "";
};

const canClaim = (claim?: string) => !!claim && authStore.hasClaim(claim);
const canCreateAction = computed(() => canClaim(descriptor.value.createClaim));
const canUpdateAction = computed(() => canClaim(descriptor.value.updateClaim));
const canDeleteAction = computed(() => canClaim(descriptor.value.deleteClaim));

const selectedId = computed(() => {
  if (!selectedRow.value) return 0;
  const value = Number(selectedRow.value[descriptor.value.idKey] ?? 0);
  return Number.isFinite(value) ? value : 0;
});

const displayValue = (item: Record<string, unknown>, col: string) => {
  if (moduleKey.value === "translate" && col === "langId") {
    const id = Number(item[col] ?? 0);
    if (Number.isFinite(id) && id > 0 && langIdToLabel.value[id]) {
      return langIdToLabel.value[id];
    }
  }
  const value = item[col];
  if (value === null || value === undefined) return "";
  if (typeof value === "object") return JSON.stringify(value);
  return String(value);
};

const getDefaultFormData = () => {
  const defaults: Record<string, any> = {};
  for (const field of descriptor.value.fields) {
    if (field.type === "boolean") defaults[field.key] = field.key === "status";
    else if (field.type === "lookup") defaults[field.key] = null;
    else if (field.type === "number") defaults[field.key] = 0;
    else defaults[field.key] = "";
  }
  return defaults;
};

const buildPayload = () => {
  const payload: Record<string, unknown> = {};
  for (const field of descriptor.value.fields) {
    const value = formData.value[field.key];
    if (field.type === "number" || field.type === "lookup") payload[field.key] = Number(value ?? 0);
    else if (field.type === "boolean") payload[field.key] = Boolean(value);
    else payload[field.key] = String(value ?? "");
  }
  return payload;
};

const loadLanguageLookupOptions = async () => {
  if (moduleKey.value !== "translate") return;
  try {
    const { data } = await crudApi.list("/languages");
    const rows = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
    const map: Record<number, string> = {};
    languageLookupOptions.value = rows
      .map((row) => {
        const id = Number(row["id"] ?? row["Id"] ?? 0);
        if (!Number.isFinite(id) || id <= 0) return null;
        const name = String(row["name"] ?? row["Name"] ?? "").trim();
        const code = String(row["code"] ?? row["Code"] ?? "").trim();
        const label = name && code ? `${name} (${code})` : name || code || `#${id}`;
        map[id] = label;
        return { value: id, label } as LookupOption;
      })
      .filter((item): item is LookupOption => item !== null);
    langIdToLabel.value = map;
  } catch {
    languageLookupOptions.value = [];
    toastNotify("warn", t("notify.lookupLoadFailed", "Lookup data load failed"));
  }
};

const upsertRow = (id: number, payload: Record<string, unknown>) => {
  rows.value = rows.value.map((row) => {
    const rowId = Number(row[descriptor.value.idKey] ?? 0);
    if (rowId !== id) return row;
    return { ...row, ...payload, [descriptor.value.idKey]: id };
  });
  selectedRow.value = rows.value.find((row) => Number(row[descriptor.value.idKey] ?? 0) === id) ?? null;
};

const removeRow = (id: number) => {
  rows.value = rows.value.filter((row) => Number(row[descriptor.value.idKey] ?? 0) !== id);
  selectedRow.value = null;
};

const loadData = async (showMessage = false) => {
  if (!moduleConfig.value?.resourcePath) {
    rows.value = [];
    return;
  }
  if (moduleKey.value === "translate") {
    await loadLanguageLookupOptions();
  }
  try {
    const { data } = await crudApi.list(moduleConfig.value.resourcePath);
    rows.value = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
    selectedRow.value = null;
    if (showMessage) {
      toastNotify("info", t("notify.listRefreshed", "List refreshed"));
    }
  } catch {
    rows.value = [];
    toastNotify("error", t("notify.listLoadFailed", "List load failed"));
  }
};

const mutate = async (operation: () => Promise<unknown>, successMessage: string) => {
  try {
    await operation();
    toastNotify("success", successMessage);
  } catch {
    toastNotify("error", t("notify.operationFailed", "Operation failed"));
  } finally {
    await loadData(false);
    window.setTimeout(() => {
      void loadData(false);
    }, 450);
  }
};

const openCreateDialog = () => {
  if (!canCreateAction.value) return;
  void loadLanguageLookupOptions();
  formMode.value = "create";
  formError.value = "";
  formData.value = getDefaultFormData();
  showFormDialog.value = true;
};

const openEditDialog = () => {
  if (!selectedRow.value || !canUpdateAction.value) return;
  void loadLanguageLookupOptions();
  formMode.value = "edit";
  formError.value = "";
  formData.value = getDefaultFormData();
  for (const field of descriptor.value.fields) {
    const raw = selectedRow.value[field.key];
    if (field.type === "lookup") {
      const n = Number(raw ?? 0);
      formData.value[field.key] = Number.isFinite(n) && n > 0 ? n : null;
    } else {
      formData.value[field.key] = raw ?? formData.value[field.key];
    }
  }
  showFormDialog.value = true;
};

const saveFormDialog = async () => {
  const missing = descriptor.value.fields
    .filter((field) => field.required)
    .filter((field) => {
      const value = formData.value[field.key];
      if (field.type === "lookup" || field.type === "number") {
        const n = Number(value ?? 0);
        return !Number.isFinite(n) || n <= 0;
      }
      return value === undefined || value === null || (typeof value === "string" && value.trim() === "");
    })
    .map((field) => field.key);

  if (missing.length > 0) {
    formError.value = `${t("validation.missingRequiredPrefix", "Missing required fields:")} ${missing.join(", ")}`;
    toastNotify("warn", formError.value);
    return;
  }

  const payload = buildPayload();
  if (formMode.value === "edit") {
    payload[descriptor.value.idKey] = selectedId.value;
  }

  const resourcePath = moduleConfig.value?.resourcePath;
  if (!resourcePath) return;
  if (formMode.value === "edit") {
    try {
      await crudApi.update(resourcePath, payload);
      toastNotify("success", t("notify.updated", "Updated successfully"));
      upsertRow(selectedId.value, payload);
    } catch {
      toastNotify("error", t("notify.operationFailed", "Operation failed"));
      return;
    }
  } else {
    await mutate(
      () => crudApi.create(resourcePath, payload),
      t("notify.created", "Created successfully")
    );
  }
  showFormDialog.value = false;
};

const deleteSelected = () => {
  const resourcePath = moduleConfig.value?.resourcePath;
  if (!canDeleteAction.value || !selectedId.value || !resourcePath) return;
  confirm.require({
    header: t("dialog.confirmDelete", "Confirm delete"),
    message: t("dialog.deleteConfirm", "Are you sure you want to delete this record?"),
    icon: "pi pi-exclamation-triangle",
    accept: async () => {
      try {
        await crudApi.remove(resourcePath, String(selectedId.value));
        toastNotify("success", t("notify.deleted", "Deleted successfully"));
        removeRow(selectedId.value);
      } catch {
        toastNotify("error", t("notify.operationFailed", "Operation failed"));
      }
    }
  });
};

const openRelationDialog = (type: RelationDialogType) => {
  if (!selectedId.value) return;
  relationDialogType.value = type;
  relationSelectedIds.value = [];
  relationLookupOptions.value = [];
  passwordValue.value = "";
  confirmPasswordValue.value = "";
  showRelationDialog.value = true;
  if (type !== "password") {
    void loadRelationLookup(type);
  }
};

const loadRelationCurrentSelection = async (type: Exclude<RelationDialogType, "password">) => {
  const id = selectedId.value;
  if (!id) return;
  const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
    userClaims: `/user-claims/users/${id}`,
    userGroups: `/user-groups/users/${id}/groups`,
    groupClaims: `/group-claims/groups/${id}`,
    groupUsers: `/user-groups/groups/${id}/users`
  };
  try {
    const { data } = await crudApi.list(pathMap[type]);
    const relationRows = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
    relationSelectedIds.value = extractRelationSelectedIds(type, relationRows);
  } catch {
    relationSelectedIds.value = [];
  }
};

const loadRelationLookup = async (type: Exclude<RelationDialogType, "password">) => {
  const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
    userClaims: "/operation-claims",
    userGroups: "/groups",
    groupClaims: "/operation-claims",
    groupUsers: "/users"
  };
  const resourcePath = pathMap[type];
  if (!resourcePath) {
    relationLookupOptions.value = [];
    return;
  }

  relationLookupLoading.value = true;
  try {
    const { data } = await crudApi.list(resourcePath);
    const list = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
    relationLookupOptions.value = list
      .map((row) => {
        const getId = (...keys: string[]) => {
          for (const key of keys) {
            const value = Number(row[key] ?? 0);
            if (Number.isFinite(value) && value > 0) return value;
          }
          return 0;
        };
        const getText = (...keys: string[]) => {
          for (const key of keys) {
            const value = row[key];
            if (value !== null && value !== undefined && String(value).trim() !== "") {
              return String(value);
            }
          }
          return "";
        };

        const id =
          type === "groupUsers"
            ? getId("userId", "id")
            : type === "userClaims" || type === "groupClaims"
              ? getId("id", "operationClaimId")
              : getId("id", "groupId");
        if (!id) return null;
        const label =
          type === "groupUsers"
            ? getText("fullName", "email", "userName")
            : type === "userClaims" || type === "groupClaims"
              ? getText("name", "alias")
              : getText("groupName", "name");
        return {
          value: id,
          label: label ? `${label} (#${id})` : `#${id}`
        } as LookupOption;
      })
      .filter((item): item is LookupOption => item !== null);
    await loadRelationCurrentSelection(type);
  } catch {
    relationLookupOptions.value = [];
    toastNotify("warn", t("notify.lookupLoadFailed", "Lookup data load failed"));
  } finally {
    relationLookupLoading.value = false;
  }
};

const loadRelationData = async () => {
  if (relationDialogType.value === "password") return;
  await loadRelationCurrentSelection(relationDialogType.value);
};

const saveRelationDialog = async () => {
  if (!selectedId.value) return;
  const id = selectedId.value;

  if (relationDialogType.value === "password") {
    if (!passwordValue.value || !confirmPasswordValue.value) {
      toastNotify("warn", t("notify.passwordRequired", "Password fields are required"));
      return;
    }
    await mutate(
      () => crudApi.update("/Auth/user-password", { userId: id, password: passwordValue.value, confirmPassword: confirmPasswordValue.value }),
      t("notify.operationCompleted", "Operation completed")
    );
    showRelationDialog.value = false;
    return;
  }

  const ids = relationSelectedIds.value;
  if (ids.length === 0) {
    toastNotify("warn", t("notify.selectAtLeastOne", "Select at least one item"));
    return;
  }
  const rt = relationDialogType.value as Exclude<RelationDialogType, "password">;
  const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
    userClaims: "/user-claims",
    userGroups: "/user-groups",
    groupClaims: "/group-claims",
    groupUsers: "/user-groups/groups"
  };
  const payloadMap: Record<Exclude<RelationDialogType, "password">, Record<string, unknown>> = {
    userClaims: { UserId: id, ClaimIds: ids },
    userGroups: { UserId: id, GroupId: ids },
    groupClaims: { GroupId: id, ClaimIds: ids },
    groupUsers: { GroupId: id, UserIds: ids }
  };
  await mutate(
    () => crudApi.update(pathMap[rt], payloadMap[rt]),
    t("notify.operationCompleted", "Operation completed")
  );
  showRelationDialog.value = false;
};

watch(
  () => route.params.moduleKey,
  () => {
    formError.value = "";
    showFormDialog.value = false;
    showRelationDialog.value = false;
    resetFilters();
    void loadData(false);
  }
);

onMounted(() => {
  resetFilters();
  void loadData(false);
});
</script>

<template>
  <Card>
    <template #title>{{ moduleTitle }}</template>
    <template #content>
      <Tag severity="info" :value="moduleConfig?.resourcePath ?? t('module.noEndpoint', 'No API endpoint')" />
      <small v-if="formError" style="color:#c62828">{{ formError }}</small>

      <div v-if="moduleConfig?.resourcePath" class="toolbar">
        <Button icon="pi pi-refresh" severity="secondary" :title="t('action.refresh', 'Refresh')" @click="loadData(true)" />
        <Button icon="pi pi-plus" severity="success" :title="t('action.create', 'Create')" :disabled="!canCreateAction" @click="openCreateDialog" />
        <Button icon="pi pi-pencil" severity="warning" :title="t('action.edit', 'Edit')" :disabled="!selectedRow || !canUpdateAction" @click="openEditDialog" />
        <Button icon="pi pi-trash" severity="danger" :title="t('action.delete', 'Delete')" :disabled="!selectedRow || !canDeleteAction" @click="deleteSelected" />

        <Button
          v-if="moduleKey === 'user' && authStore.hasClaim('UpdateUserClaimCommand')"
          icon="pi pi-shield"
          :title="t('action.userClaims', 'User Claims')"
          :disabled="!selectedRow"
          @click="openRelationDialog('userClaims')"
        />
        <Button
          v-if="moduleKey === 'user' && authStore.hasClaim('UpdateGroupClaimCommand')"
          icon="pi pi-users"
          :title="t('action.userGroups', 'User Groups')"
          :disabled="!selectedRow"
          @click="openRelationDialog('userGroups')"
        />
        <Button
          v-if="moduleKey === 'user' && authStore.hasClaim('UserChangePasswordCommand')"
          icon="pi pi-key"
          severity="contrast"
          :title="t('action.changePassword', 'Change Password')"
          :disabled="!selectedRow"
          @click="openRelationDialog('password')"
        />
        <Button
          v-if="moduleKey === 'group' && authStore.hasClaim('UpdateGroupClaimCommand')"
          icon="pi pi-shield"
          :title="t('action.groupClaims', 'Group Claims')"
          :disabled="!selectedRow"
          @click="openRelationDialog('groupClaims')"
        />
        <Button
          v-if="moduleKey === 'group' && authStore.hasClaim('UpdateUserGroupByGroupIdCommand')"
          icon="pi pi-users"
          :title="t('action.groupUsers', 'Group Users')"
          :disabled="!selectedRow"
          @click="openRelationDialog('groupUsers')"
        />
        <InputText
          v-model="tableFilterValue"
          :placeholder="t('action.filterTable', 'Filter table...')"
          @input="filters.global.value = tableFilterValue"
        />
      </div>

      <DataTable
        class="table-top-gap"
        :value="rows"
        paginator
        :rows="10"
        :rowsPerPageOptions="[10, 25, 50]"
        paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
        currentPageReportTemplate="{first}-{last} / {totalRecords}"
        v-model:selection="selectedRow"
        selectionMode="single"
        v-model:filters="filters"
        :globalFilterFields="descriptor.columns"
        filterDisplay="menu"
        sortMode="multiple"
        removableSort
        :emptyMessage="t('table.empty', 'No records found')"
      >
        <Column v-for="col in descriptor.columns" :key="col" :header="col" :field="col" sortable filter :showFilterMatchModes="false">
          <template #body="{ data }">
            {{ displayValue(data as Record<string, unknown>, col) }}
          </template>
        </Column>
      </DataTable>

      <Dialog
        v-model:visible="showFormDialog"
        :header="formMode === 'create' ? t('form.createRecord', 'Create Record') : t('form.editRecord', 'Edit Record')"
        modal
        :style="{ width: '46rem' }"
      >
        <div class="form-grid">
          <div v-for="field in descriptor.fields" :key="field.key">
            <InputText
              v-if="field.type === 'text' || field.type === 'number'"
              v-model="formData[field.key]"
              :placeholder="field.label"
            />
            <Dropdown
              v-else-if="field.type === 'lookup'"
              v-model="formData[field.key]"
              :options="languageLookupOptions"
              optionLabel="label"
              optionValue="value"
              :placeholder="field.label"
              showClear
              append-to="body"
              :style="{ width: '100%' }"
            />
            <Textarea v-else-if="field.type === 'textarea'" v-model="formData[field.key]" rows="4" :placeholder="field.label" />
            <div v-else style="display:flex;align-items:center;gap:0.5rem;">
              <Checkbox v-model="formData[field.key]" binary />
              <span>{{ field.label }}</span>
            </div>
          </div>
        </div>
        <template #footer>
          <Button icon="pi pi-times" severity="secondary" :title="t('action.cancel', 'Cancel')" @click="showFormDialog = false" />
          <Button icon="pi pi-save" :title="t('action.save', 'Save')" @click="saveFormDialog" />
        </template>
      </Dialog>

      <Dialog
        v-model:visible="showRelationDialog"
        :header="t('relation.title', 'Relation')"
        modal
        :style="{ width: 'min(38rem, calc(100vw - 2rem))' }"
        :content-style="{ overflow: 'visible' }"
      >
        <div class="form-grid relation-dialog-fields">
          <InputText :model-value="String(selectedId)" disabled />
          <MultiSelect
            v-if="relationDialogType !== 'password'"
            v-model="relationSelectedIds"
            :options="relationLookupOptions"
            optionLabel="label"
            optionValue="value"
            :filter="true"
            :showClear="true"
            :loading="relationLookupLoading"
            :placeholder="t('relation.selectItems', 'Select items')"
            append-to="body"
            :panel-style="{ maxWidth: 'min(96vw, 38rem)' }"
            class="relation-multiselect"
          />
          <InputText v-if="relationDialogType === 'password'" v-model="passwordValue" type="password" :placeholder="t('field.password', 'Password')" />
          <InputText
            v-if="relationDialogType === 'password'"
            v-model="confirmPasswordValue"
            type="password"
            :placeholder="t('field.confirmPassword', 'Confirm Password')"
          />
        </div>
        <template #footer>
          <Button v-if="relationDialogType !== 'password'" icon="pi pi-download" severity="secondary" :title="t('action.load', 'Load')" @click="loadRelationData" />
          <Button icon="pi pi-times" severity="secondary" :title="t('action.cancel', 'Cancel')" @click="showRelationDialog = false" />
          <Button icon="pi pi-save" :title="t('action.save', 'Save')" @click="saveRelationDialog" />
        </template>
      </Dialog>
    </template>
  </Card>
</template>
