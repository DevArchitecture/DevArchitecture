import { useEffect, useRef, useState, type ComponentProps } from "react";
import { useParams } from "react-router-dom";
import { Card } from "primereact/card";
import { Tag } from "primereact/tag";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { InputTextarea } from "primereact/inputtextarea";
import { Dialog } from "primereact/dialog";
import { Checkbox } from "primereact/checkbox";
import { MultiSelect } from "primereact/multiselect";
import { Dropdown } from "primereact/dropdown";
import { Toast } from "primereact/toast";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";
import { FilterMatchMode } from "primereact/api";
import { authStore, crudApi } from "../api/client";
import { CLIENT_MODULES } from "../config/modules";

type FieldType = "text" | "number" | "boolean" | "textarea" | "lookup";
type FieldDef = { key: string; label: string; type: FieldType; lookupPath?: string; required?: boolean };
type ModuleDescriptor = {
  idKey: string;
  columns: string[];
  fields: FieldDef[];
  createClaim?: string;
  updateClaim?: string;
  deleteClaim?: string;
};
type RelationDialogType = "userClaims" | "userGroups" | "groupClaims" | "groupUsers" | "changePassword" | null;
type LookupOption = { label: string; value: number };

const extractRelationSelectedIds = (
  type: Exclude<RelationDialogType, null | "changePassword">,
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

const EMPTY_DESCRIPTOR: ModuleDescriptor = { idKey: "id", columns: ["raw"], fields: [] };

type TableFilterState = NonNullable<ComponentProps<typeof DataTable>["filters"]>;

function buildTableFilters(columns: string[]): TableFilterState {
  const state: TableFilterState = {
    global: { value: null, matchMode: FilterMatchMode.CONTAINS }
  };
  for (const col of columns) {
    state[col] = { value: null, matchMode: FilterMatchMode.CONTAINS };
  }
  return state;
}

export function ResourcePage() {
  const toast = useRef<Toast>(null);
  const { moduleKey = "" } = useParams();
  const moduleConfig = CLIENT_MODULES.find((item) => item.key === moduleKey);
  const descriptor = MODULE_DESCRIPTORS[moduleKey] ?? EMPTY_DESCRIPTOR;

  const [rows, setRows] = useState<Record<string, unknown>[]>([]);
  const [selectedRow, setSelectedRow] = useState<Record<string, unknown> | null>(null);
  const [showFormDialog, setShowFormDialog] = useState(false);
  const [formMode, setFormMode] = useState<"create" | "edit">("create");
  const [formData, setFormData] = useState<Record<string, unknown>>({});
  const [formError, setFormError] = useState("");
  const [relationDialogType, setRelationDialogType] = useState<RelationDialogType>(null);
  const [relationSelectedIds, setRelationSelectedIds] = useState<number[]>([]);
  const [relationLookupOptions, setRelationLookupOptions] = useState<LookupOption[]>([]);
  const [relationLookupLoading, setRelationLookupLoading] = useState(false);
  const [passwordValue, setPasswordValue] = useState("");
  const [confirmPasswordValue, setConfirmPasswordValue] = useState("");
  const [filters, setFilters] = useState<TableFilterState>(() => buildTableFilters(MODULE_DESCRIPTORS[moduleKey]?.columns ?? []));
  const [, setVersion] = useState(0);
  const [languageLookupOptions, setLanguageLookupOptions] = useState<LookupOption[]>([]);
  const [langIdToLabel, setLangIdToLabel] = useState<Record<number, string>>({});

  useEffect(() => {
    const unsubscribe = authStore.subscribe(() => setVersion((prev) => prev + 1));
    return unsubscribe;
  }, []);

  const t = (key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback));

  const notifyToast = (severity: "success" | "info" | "warn" | "error", detail: string) => {
    const summary =
      severity === "success"
        ? t("notify.severity.success", "Success")
        : severity === "info"
          ? t("notify.severity.info", "Info")
          : severity === "warn"
            ? t("notify.severity.warning", "Warning")
            : t("notify.severity.error", "Error");
    toast.current?.show({ severity, summary, detail });
  };

  const canClaim = (claim?: string) => !claim || authStore.hasClaim(claim);

  const selectedId = (): number => {
    if (!selectedRow) return 0;
    const raw = selectedRow[descriptor.idKey];
    const value = Number(raw);
    return Number.isFinite(value) ? value : 0;
  };

  const getDefaultFormData = (): Record<string, unknown> => {
    const defaults: Record<string, unknown> = {};
    descriptor.fields.forEach((field) => {
      if (field.type === "boolean") defaults[field.key] = true;
      else if (field.type === "lookup") defaults[field.key] = null;
      else if (field.type === "number") defaults[field.key] = 0;
      else defaults[field.key] = "";
    });
    return defaults;
  };

  const loadLanguageLookupOptions = async () => {
    if (moduleKey !== "translate") return;
    try {
      const { data } = await crudApi.list("/languages");
      const rows = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
      const map: Record<number, string> = {};
      const options = rows
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
      setLangIdToLabel(map);
      setLanguageLookupOptions(options);
    } catch {
      setLanguageLookupOptions([]);
      notifyToast("warn", t("notify.lookupLoadFailed", "Lookup data load failed"));
    }
  };

  const upsertRow = (id: number, payload: Record<string, unknown>) => {
    setRows((prev) =>
      prev.map((row) => {
        const rowId = Number(row[descriptor.idKey] ?? 0);
        if (rowId !== id) return row;
        return { ...row, ...payload, [descriptor.idKey]: id };
      })
    );
    setSelectedRow((prev) => (prev ? { ...prev, ...payload, [descriptor.idKey]: id } : prev));
  };

  const removeRow = (id: number) => {
    setRows((prev) => prev.filter((row) => Number(row[descriptor.idKey] ?? 0) !== id));
    setSelectedRow(null);
  };

  const refresh = async (showMessage = false) => {
    if (!moduleConfig?.resourcePath) {
      setRows([]);
      return;
    }
    if (moduleKey === "translate") {
      await loadLanguageLookupOptions();
    }
    try {
      const { data } = await crudApi.list(moduleConfig.resourcePath);
      setRows(Array.isArray(data) ? (data as Record<string, unknown>[]) : []);
      if (showMessage) {
        notifyToast("info", t("notify.listRefreshed", "List refreshed"));
      }
    } catch {
      setRows([]);
      notifyToast("error", t("notify.listLoadFailed", "List load failed"));
    }
  };

  const mutate = async (operation: () => Promise<unknown>, successMessage: string) => {
    try {
      await operation();
      notifyToast("success", successMessage);
      await refresh(false);
      window.setTimeout(() => {
        void refresh(false);
      }, 450);
    } catch {
      notifyToast("error", t("notify.operationFailed", "Operation failed"));
    }
  };

  const openCreateDialog = () => {
    if (!canClaim(descriptor.createClaim)) {
      notifyToast("warn", t("notify.noCreatePermission", "No create permission"));
      return;
    }
    void loadLanguageLookupOptions();
    setFormMode("create");
    setFormData(getDefaultFormData());
    setFormError("");
    setShowFormDialog(true);
  };

  const openEditDialog = () => {
    if (!selectedRow) {
      notifyToast("warn", t("notify.selectRowFirst", "Select a row first"));
      return;
    }
    if (!canClaim(descriptor.updateClaim)) {
      notifyToast("warn", t("notify.noUpdatePermission", "No update permission"));
      return;
    }
    void loadLanguageLookupOptions();
    setFormMode("edit");
    const payload = getDefaultFormData();
    descriptor.fields.forEach((field) => {
      const raw = selectedRow[field.key];
      if (field.type === "lookup") {
        const n = Number(raw ?? 0);
        payload[field.key] = Number.isFinite(n) && n > 0 ? n : null;
      } else {
        payload[field.key] = raw ?? payload[field.key];
      }
    });
    payload[descriptor.idKey] = selectedId();
    setFormData(payload);
    setFormError("");
    setShowFormDialog(true);
  };

  const saveFormDialog = async () => {
    if (!moduleConfig?.resourcePath) return;
    const missing = descriptor.fields
      .filter((field) => field.required)
      .filter((field) => {
        const v = formData[field.key];
        if (field.type === "lookup") {
          const n = Number(v ?? 0);
          return !Number.isFinite(n) || n <= 0;
        }
        return v === null || v === undefined || v === "";
      });
    if (missing.length > 0) {
      const message = `${t("validation.missingRequiredPrefix", "Missing required fields:")} ${missing.map((item) => item.key).join(", ")}`;
      setFormError(message);
      notifyToast("warn", message);
      return;
    }
    const payload: Record<string, unknown> = { ...formData };
    for (const field of descriptor.fields) {
      if (field.type === "lookup") {
        payload[field.key] = Number(formData[field.key] ?? 0);
      }
    }
    if (formMode === "edit") {
      payload[descriptor.idKey] = selectedId();
    }
    if (formMode === "edit") {
      try {
        await crudApi.update(moduleConfig.resourcePath as string, payload);
        notifyToast("success", t("notify.updated", "Updated successfully"));
        upsertRow(selectedId(), payload);
      } catch {
        notifyToast("error", t("notify.operationFailed", "Operation failed"));
        return;
      }
    } else {
      await mutate(
        () => crudApi.create(moduleConfig.resourcePath as string, payload),
        t("notify.created", "Created successfully")
      );
    }
    setShowFormDialog(false);
  };

  const deleteSelected = () => {
    if (!moduleConfig?.resourcePath) return;
    const id = selectedId();
    if (!id) {
      notifyToast("warn", t("notify.selectRowFirst", "Select a row first"));
      return;
    }
    if (!canClaim(descriptor.deleteClaim)) {
      notifyToast("warn", t("notify.noDeletePermission", "No delete permission"));
      return;
    }
    confirmDialog({
      header: t("dialog.confirmDelete", "Confirm delete"),
      message: t("dialog.deleteConfirm", "Are you sure you want to delete this record?"),
      icon: "pi pi-exclamation-triangle",
      accept: async () => {
        try {
          await crudApi.remove(moduleConfig.resourcePath as string, String(id));
          notifyToast("success", t("notify.deleted", "Deleted successfully"));
          removeRow(id);
        } catch {
          notifyToast("error", t("notify.operationFailed", "Operation failed"));
        }
      }
    });
  };

  const openRelationDialog = (type: RelationDialogType) => {
    if (!selectedId()) {
      notifyToast("warn", t("notify.selectRowFirst", "Select a row first"));
      return;
    }
    setRelationDialogType(type);
    setRelationSelectedIds([]);
    setRelationLookupOptions([]);
    setPasswordValue("");
    setConfirmPasswordValue("");
    if (type && type !== "changePassword") {
      void loadRelationLookup(type);
    }
  };

  const loadRelationCurrentSelection = async (type: Exclude<RelationDialogType, null | "changePassword">) => {
    const id = selectedId();
    if (!id) return;
    const pathMap: Record<Exclude<RelationDialogType, null | "changePassword">, string> = {
      userClaims: `/user-claims/users/${id}`,
      userGroups: `/user-groups/users/${id}/groups`,
      groupClaims: `/group-claims/groups/${id}`,
      groupUsers: `/user-groups/groups/${id}/users`
    };
    try {
      const { data } = await crudApi.list(pathMap[type]);
      const relationRows = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
      setRelationSelectedIds(extractRelationSelectedIds(type, relationRows));
    } catch {
      setRelationSelectedIds([]);
    }
  };

  const loadRelationLookup = async (type: Exclude<RelationDialogType, null | "changePassword">) => {
    const pathMap: Record<Exclude<RelationDialogType, null | "changePassword">, string> = {
      userClaims: "/operation-claims",
      userGroups: "/groups",
      groupClaims: "/operation-claims",
      groupUsers: "/users"
    };
    const resourcePath = pathMap[type];
    if (!resourcePath) {
      setRelationLookupOptions([]);
      return;
    }

    setRelationLookupLoading(true);
    try {
      const { data } = await crudApi.list(resourcePath);
      const list = Array.isArray(data) ? (data as Record<string, unknown>[]) : [];
      const options = list
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
      setRelationLookupOptions(options);
      await loadRelationCurrentSelection(type);
    } catch {
      setRelationLookupOptions([]);
      notifyToast("warn", t("notify.lookupLoadFailed", "Lookup data load failed"));
    } finally {
      setRelationLookupLoading(false);
    }
  };

  const saveRelationDialog = async () => {
    const id = selectedId();
    if (!id || !relationDialogType) return;
    if (relationDialogType === "changePassword") {
      if (!passwordValue || !confirmPasswordValue) {
        notifyToast("warn", t("notify.passwordRequired", "Password fields are required"));
        return;
      }
      if (passwordValue !== confirmPasswordValue) {
        notifyToast("warn", t("notify.passwordsDoNotMatch", "Passwords do not match"));
        return;
      }
      await mutate(
        () => crudApi.update("/Auth/user-password", { userId: id, password: passwordValue, confirmPassword: confirmPasswordValue }),
        t("notify.passwordChanged", "Password changed")
      );
      setRelationDialogType(null);
      return;
    }

    const ids = relationSelectedIds;
    if (ids.length === 0) {
      notifyToast("warn", t("notify.selectAtLeastOne", "Select at least one item"));
      return;
    }

    const operation = {
      userClaims: () => crudApi.update("/user-claims", { UserId: id, ClaimIds: ids }),
      userGroups: () => crudApi.update("/user-groups", { UserId: id, GroupId: ids }),
      groupClaims: () => crudApi.update("/group-claims", { GroupId: id, ClaimIds: ids }),
      groupUsers: () => crudApi.update("/user-groups/groups", { GroupId: id, UserIds: ids })
    }[relationDialogType];

    if (!operation) return;
    await mutate(operation, t("notify.savedSuccessfully", "Saved successfully"));
    setRelationDialogType(null);
  };

  const loadRelationData = () => {
    if (!relationDialogType || relationDialogType === "changePassword") return;
    void loadRelationCurrentSelection(relationDialogType);
  };

  useEffect(() => {
    setSelectedRow(null);
    setShowFormDialog(false);
    setRelationDialogType(null);
    setFilters(buildTableFilters((MODULE_DESCRIPTORS[moduleKey] ?? EMPTY_DESCRIPTOR).columns));
    void refresh(false);
  }, [moduleKey, moduleConfig?.resourcePath]);

  return (
    <Card title={t(`menu.${moduleKey}`, moduleConfig?.label ?? "Module")}>
      <Toast ref={toast} />
      <ConfirmDialog />
      <Tag value={moduleConfig?.resourcePath ?? t("module.noEndpoint", "No API endpoint")} severity="info" />

      <div className="toolbar">
        <Button icon="pi pi-refresh" rounded severity="secondary" tooltip={t("action.refresh", "Refresh")} onClick={() => void refresh(true)} />
        <Button icon="pi pi-plus" rounded severity="success" tooltip={t("action.create", "Create")} disabled={!descriptor.createClaim || !canClaim(descriptor.createClaim)} onClick={openCreateDialog} />
        <Button icon="pi pi-pencil" rounded severity="help" tooltip={t("action.edit", "Edit")} disabled={!selectedRow || !descriptor.updateClaim || !canClaim(descriptor.updateClaim)} onClick={openEditDialog} />
        <Button icon="pi pi-trash" rounded severity="danger" tooltip={t("action.delete", "Delete")} disabled={!selectedRow || !descriptor.deleteClaim || !canClaim(descriptor.deleteClaim)} onClick={deleteSelected} />
        {moduleKey === "user" && (
          <>
            <Button icon="pi pi-shield" rounded severity="info" tooltip={t("action.userClaims", "User Claims")} disabled={!selectedRow || !authStore.hasClaim("UpdateUserClaimCommand")} onClick={() => openRelationDialog("userClaims")} />
            <Button icon="pi pi-users" rounded severity="contrast" tooltip={t("action.userGroups", "User Groups")} disabled={!selectedRow || !authStore.hasClaim("UpdateGroupClaimCommand")} onClick={() => openRelationDialog("userGroups")} />
            <Button icon="pi pi-key" rounded severity="warning" tooltip={t("action.changePassword", "Change Password")} disabled={!selectedRow || !authStore.hasClaim("UserChangePasswordCommand")} onClick={() => openRelationDialog("changePassword")} />
          </>
        )}
        {moduleKey === "group" && (
          <>
            <Button icon="pi pi-shield" rounded severity="info" tooltip={t("action.groupClaims", "Group Claims")} disabled={!selectedRow || !authStore.hasClaim("UpdateGroupClaimCommand")} onClick={() => openRelationDialog("groupClaims")} />
            <Button icon="pi pi-users" rounded severity="contrast" tooltip={t("action.groupUsers", "Group Users")} disabled={!selectedRow || !authStore.hasClaim("UpdateUserGroupByGroupIdCommand")} onClick={() => openRelationDialog("groupUsers")} />
          </>
        )}
        <InputText
          placeholder={t("action.filterTable", "Filter table...")}
          value={String((filters.global as { value?: unknown } | undefined)?.value ?? "")}
          onChange={(event) => {
            const value = event.target.value;
            setFilters((prev) => ({
              ...prev,
              global: { value, matchMode: FilterMatchMode.CONTAINS }
            }));
          }}
        />
      </div>

      <DataTable
        value={rows}
        paginator
        rows={10}
        rowsPerPageOptions={[10, 25, 50]}
        paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
        currentPageReportTemplate="{first}-{last} / {totalRecords}"
        className="table-top-gap"
        selectionMode="single"
        selection={selectedRow}
        onSelectionChange={(event) => setSelectedRow((event.value as Record<string, unknown>) ?? null)}
        filters={filters}
        onFilter={(e) => setFilters(e.filters)}
        globalFilterFields={descriptor.columns}
        filterDisplay="menu"
        sortMode="multiple"
        removableSort
        emptyMessage={t("table.empty", "No records found")}
      >
        {descriptor.columns.map((col) => (
          <Column
            key={col}
            field={col}
            header={col}
            sortable
            filter
            showFilterMatchModes={false}
            body={(item: Record<string, unknown>) => {
              if (moduleKey === "translate" && col === "langId") {
                const id = Number(item[col] ?? 0);
                if (Number.isFinite(id) && id > 0 && langIdToLabel[id]) {
                  return langIdToLabel[id];
                }
              }
              const value = item[col];
              if (value === null || value === undefined) return "";
              if (typeof value === "object") return JSON.stringify(value);
              return String(value);
            }}
          />
        ))}
      </DataTable>

      <Dialog
        header={formMode === "create" ? `${t("action.create", "Create")} ${t(`menu.${moduleKey}`, moduleConfig?.label ?? "")}` : `${t("action.edit", "Edit")} ${t(`menu.${moduleKey}`, moduleConfig?.label ?? "")}`}
        visible={showFormDialog}
        style={{ width: "44rem" }}
        modal
        onHide={() => setShowFormDialog(false)}
      >
        <div className="form-grid">
          {descriptor.fields.map((field) => (
            <div key={field.key}>
              {field.type === "textarea" && (
                <InputTextarea
                  rows={4}
                  placeholder={field.label}
                  value={String(formData[field.key] ?? "")}
                  onChange={(event) => setFormData((prev) => ({ ...prev, [field.key]: event.target.value }))}
                />
              )}
              {field.type === "text" && (
                <InputText
                  placeholder={field.label}
                  value={String(formData[field.key] ?? "")}
                  onChange={(event) => setFormData((prev) => ({ ...prev, [field.key]: event.target.value }))}
                />
              )}
              {field.type === "number" && (
                <InputText
                  placeholder={field.label}
                  value={String(formData[field.key] ?? "")}
                  onChange={(event) => setFormData((prev) => ({ ...prev, [field.key]: Number(event.target.value) }))}
                />
              )}
              {field.type === "lookup" && (
                <Dropdown
                  options={languageLookupOptions}
                  optionLabel="label"
                  optionValue="value"
                  value={formData[field.key] as number | null}
                  onChange={(event) => setFormData((prev) => ({ ...prev, [field.key]: event.value }))}
                  placeholder={field.label}
                  showClear
                  appendTo={typeof document !== "undefined" ? document.body : undefined}
                  style={{ width: "100%" }}
                />
              )}
              {field.type === "boolean" && (
                <div>
                  <Checkbox
                    inputId={`field-${field.key}`}
                    checked={Boolean(formData[field.key])}
                    onChange={(event) => setFormData((prev) => ({ ...prev, [field.key]: event.checked }))}
                  />
                  <label htmlFor={`field-${field.key}`} style={{ marginLeft: "0.5rem" }}>
                    {field.label}
                  </label>
                </div>
              )}
            </div>
          ))}
          {formError && <small style={{ color: "#c62828" }}>{formError}</small>}
        </div>
        <div className="crud-actions" style={{ marginTop: "1rem" }}>
          <Button icon="pi pi-times" severity="secondary" tooltip={t("action.cancel", "Cancel")} onClick={() => setShowFormDialog(false)} />
          <Button icon="pi pi-save" tooltip={t("action.save", "Save")} onClick={() => void saveFormDialog()} />
        </div>
      </Dialog>

      <Dialog
        header={t("relation.title", "Relation")}
        visible={relationDialogType !== null}
        style={{ width: "min(34rem, calc(100vw - 2rem))" }}
        contentStyle={{ overflow: "visible" }}
        modal
        onHide={() => setRelationDialogType(null)}
      >
        {relationDialogType !== "changePassword" && (
          <div className="form-grid relation-dialog-fields">
            <MultiSelect
              options={relationLookupOptions}
              optionLabel="label"
              optionValue="value"
              value={relationSelectedIds}
              onChange={(event) => setRelationSelectedIds((event.value as number[]) ?? [])}
              filter
              showClear
              placeholder={t("relation.selectItems", "Select items")}
              loading={relationLookupLoading}
              appendTo={typeof document !== "undefined" ? document.body : undefined}
              style={{ width: "100%", minWidth: 0, maxWidth: "100%" }}
              panelStyle={{ maxWidth: "min(96vw, 34rem)" }}
            />
          </div>
        )}
        {relationDialogType === "changePassword" && (
          <div className="form-grid">
            <InputText
              placeholder={t("field.password", "Password")}
              value={passwordValue}
              onChange={(event) => setPasswordValue(event.target.value)}
            />
            <InputText
              placeholder={t("field.confirmPassword", "Confirm Password")}
              value={confirmPasswordValue}
              onChange={(event) => setConfirmPasswordValue(event.target.value)}
            />
          </div>
        )}
        <div className="crud-actions" style={{ marginTop: "1rem" }}>
          {relationDialogType !== "changePassword" && <Button icon="pi pi-download" severity="secondary" tooltip={t("action.load", "Load")} onClick={loadRelationData} />}
          <Button icon="pi pi-save" tooltip={t("action.save", "Save")} onClick={() => void saveRelationDialog()} />
        </div>
      </Dialog>
    </Card>
  );
}
