import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ActivatedRoute } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { TableModule } from "primeng/table";
import { CardModule } from "primeng/card";
import { TagModule } from "primeng/tag";
import { InputTextModule } from "primeng/inputtext";
import { InputTextarea } from "primeng/inputtextarea";
import { ButtonModule } from "primeng/button";
import { DialogModule } from "primeng/dialog";
import { CheckboxModule } from "primeng/checkbox";
import { MultiSelectModule } from "primeng/multiselect";
import { DropdownModule } from "primeng/dropdown";
import { ConfirmationService, MessageService } from "primeng/api";
import { ApiService, CLIENT_MODULES } from "../core/api.service";
import { AuthService } from "../core/auth.service";
import { TranslationService } from "../core/translation.service";

type FieldType = "text" | "textarea" | "number" | "boolean" | "lookup";

type FieldDef = {
  key: string;
  label: string;
  type: FieldType;
  /** When type is "lookup", list is loaded from this API path (e.g. /languages for langId). */
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

type RelationDialogType = "userClaims" | "userGroups" | "groupClaims" | "groupUsers" | "password";
type LookupOption = { label: string; value: number };

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

@Component({
  selector: "app-resource-page",
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    CardModule,
    TagModule,
    InputTextModule,
    InputTextarea,
    ButtonModule,
    DialogModule,
    CheckboxModule,
    MultiSelectModule,
    DropdownModule
  ],
  styles: [
    `
      .toolbar {
        margin-top: 1rem;
        display: flex;
        flex-wrap: wrap;
        gap: 0.5rem;
      }
      .form-grid {
        display: grid;
        gap: 0.75rem;
      }
      .checkbox-row {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }
      .error-text {
        color: #c62828;
        display: block;
        margin-top: 0.5rem;
      }
      .table-filter {
        min-width: 16rem;
      }
      .relation-dialog-fields {
        max-width: 100%;
        min-width: 0;
      }
    `
  ],
  template: `
    <p-card [header]="title">
      <p-tag [value]="resourcePath || translate('module.noEndpoint', 'No API endpoint')" severity="info"></p-tag>
      <small *ngIf="formError" class="error-text">{{ formError }}</small>

      <div class="toolbar" *ngIf="resourcePath">
        <button
          pButton
          type="button"
          icon="pi pi-refresh"
          severity="secondary"
          [attr.title]="translate('action.refresh', 'Refresh')"
          (click)="refresh()"
        ></button>
        <button
          pButton
          type="button"
          icon="pi pi-plus"
          severity="success"
          [attr.title]="translate('action.create', 'Create')"
          [disabled]="!canCreateAction()"
          (click)="openCreateDialog()"
        ></button>
        <button
          pButton
          type="button"
          icon="pi pi-pencil"
          severity="warn"
          [attr.title]="translate('action.edit', 'Edit')"
          [disabled]="!selectedRow || !canUpdateAction()"
          (click)="openEditDialog()"
        ></button>
        <button
          pButton
          type="button"
          icon="pi pi-trash"
          severity="danger"
          [attr.title]="translate('action.delete', 'Delete')"
          [disabled]="!selectedRow || !canDeleteAction()"
          (click)="deleteSelected()"
        ></button>

        <button
          *ngIf="moduleKey === 'user' && canClaim('UpdateUserClaimCommand')"
          pButton
          type="button"
          icon="pi pi-shield"
          [attr.title]="translate('action.userClaims', 'User Claims')"
          [disabled]="!selectedRow"
          (click)="openRelationDialog('userClaims')"
        ></button>
        <button
          *ngIf="moduleKey === 'user' && canClaim('UpdateGroupClaimCommand')"
          pButton
          type="button"
          icon="pi pi-users"
          [attr.title]="translate('action.userGroups', 'User Groups')"
          [disabled]="!selectedRow"
          (click)="openRelationDialog('userGroups')"
        ></button>
        <button
          *ngIf="moduleKey === 'user' && canClaim('UserChangePasswordCommand')"
          pButton
          type="button"
          icon="pi pi-key"
          severity="contrast"
          [attr.title]="translate('action.changePassword', 'Change Password')"
          [disabled]="!selectedRow"
          (click)="openRelationDialog('password')"
        ></button>

        <button
          *ngIf="moduleKey === 'group' && canClaim('UpdateGroupClaimCommand')"
          pButton
          type="button"
          icon="pi pi-shield"
          [attr.title]="translate('action.groupClaims', 'Group Claims')"
          [disabled]="!selectedRow"
          (click)="openRelationDialog('groupClaims')"
        ></button>
        <button
          *ngIf="moduleKey === 'group' && canClaim('UpdateUserGroupByGroupIdCommand')"
          pButton
          type="button"
          icon="pi pi-users"
          [attr.title]="translate('action.groupUsers', 'Group Users')"
          [disabled]="!selectedRow"
          (click)="openRelationDialog('groupUsers')"
        ></button>
        <input
          pInputText
          class="table-filter"
          [placeholder]="translate('action.filterTable', 'Filter table...')"
          [(ngModel)]="tableFilterValue"
          (input)="dt.filterGlobal($any($event.target).value, 'contains')"
        />
      </div>

      <p-table
        #dt
        [value]="rows"
        selectionMode="single"
        [(selection)]="selectedRow"
        [paginator]="true"
        [rows]="10"
        [rowsPerPageOptions]="[10, 25, 50]"
        [globalFilterFields]="descriptor.columns"
        sortMode="multiple"
        [showCurrentPageReport]="true"
        currentPageReportTemplate="{first}-{last} / {totalRecords}"
        [filterDelay]="200"
        [tableStyle]="{ 'margin-top': '1rem' }"
      >
        <ng-template pTemplate="header">
          <tr>
            <th *ngFor="let col of descriptor.columns" [pSortableColumn]="col">
              {{ col }}
              <p-sortIcon [field]="col"></p-sortIcon>
            </th>
          </tr>
          <tr>
            <th *ngFor="let col of descriptor.columns">
              <p-columnFilter [field]="col" type="text" display="menu"></p-columnFilter>
            </th>
          </tr>
        </ng-template>
        <ng-template pTemplate="body" let-item>
          <tr [pSelectableRow]="item">
            <td *ngFor="let col of descriptor.columns">{{ displayValue(item, col) }}</td>
          </tr>
        </ng-template>
        <ng-template pTemplate="emptymessage">
          <tr>
            <td [attr.colspan]="descriptor.columns.length" class="p-text-center">{{ translate("table.empty", "No records found") }}</td>
          </tr>
        </ng-template>
      </p-table>

      <p-dialog
        [header]="formMode === 'create' ? translate('form.createRecord', 'Create Record') : translate('form.editRecord', 'Edit Record')"
        [modal]="true"
        [(visible)]="showFormDialog"
        [style]="{ width: '46rem' }"
      >
        <div class="form-grid">
          <div *ngFor="let field of descriptor.fields">
            <ng-container [ngSwitch]="field.type">
              <input
                *ngSwitchCase="'text'"
                pInputText
                [placeholder]="field.label"
                [(ngModel)]="formData[field.key]"
              />
              <input
                *ngSwitchCase="'number'"
                pInputText
                [placeholder]="field.label"
                [(ngModel)]="formData[field.key]"
              />
              <p-dropdown
                *ngSwitchCase="'lookup'"
                [options]="languageLookupOptions"
                optionLabel="label"
                optionValue="value"
                [(ngModel)]="formData[field.key]"
                [placeholder]="field.label"
                [showClear]="true"
                appendTo="body"
                [style]="{ width: '100%' }"
              ></p-dropdown>
              <textarea
                *ngSwitchCase="'textarea'"
                pInputTextarea
                rows="4"
                [placeholder]="field.label"
                [(ngModel)]="formData[field.key]"
              ></textarea>
              <div *ngSwitchCase="'boolean'" class="checkbox-row">
                <p-checkbox [binary]="true" [(ngModel)]="formData[field.key]"></p-checkbox>
                <span>{{ field.label }}</span>
              </div>
            </ng-container>
          </div>
        </div>
        <ng-template pTemplate="footer">
          <button pButton type="button" icon="pi pi-times" severity="secondary" [attr.title]="translate('action.cancel', 'Cancel')" (click)="showFormDialog = false"></button>
          <button pButton type="button" icon="pi pi-save" [attr.title]="translate('action.save', 'Save')" (click)="saveFormDialog()"></button>
        </ng-template>
      </p-dialog>

      <p-dialog
        [header]="translate('relation.title', 'Relation')"
        [modal]="true"
        [(visible)]="showRelationDialog"
        [style]="{ width: 'min(38rem, calc(100vw - 2rem))' }"
        [contentStyle]="{ overflow: 'visible' }"
      >
        <div class="form-grid relation-dialog-fields">
          <input pInputText [value]="selectedId()" disabled />
          <p-multiSelect
            *ngIf="relationDialogType !== 'password'"
            [options]="relationLookupOptions"
            optionLabel="label"
            optionValue="value"
            [filter]="true"
            [showClear]="true"
            [loading]="relationLookupLoading"
            [defaultLabel]="translate('relation.selectItems', 'Select items')"
            appendTo="body"
            [style]="{ width: '100%' }"
            [panelStyle]="{ maxWidth: 'min(96vw, 36rem)' }"
            [(ngModel)]="relationSelectedIds"
          ></p-multiSelect>
          <input
            *ngIf="relationDialogType === 'password'"
            pInputText
            type="password"
            [placeholder]="translate('field.password', 'Password')"
            [(ngModel)]="passwordValue"
          />
          <input
            *ngIf="relationDialogType === 'password'"
            pInputText
            type="password"
            [placeholder]="translate('field.confirmPassword', 'Confirm Password')"
            [(ngModel)]="confirmPasswordValue"
          />
        </div>
        <ng-template pTemplate="footer">
          <button
            *ngIf="relationDialogType !== 'password'"
            pButton
            type="button"
            severity="secondary"
            icon="pi pi-download"
            [attr.title]="translate('action.load', 'Load')"
            (click)="loadRelationData()"
          ></button>
          <button pButton type="button" icon="pi pi-times" severity="secondary" [attr.title]="translate('action.cancel', 'Cancel')" (click)="showRelationDialog = false"></button>
          <button pButton type="button" icon="pi pi-save" [attr.title]="translate('action.save', 'Save')" (click)="saveRelationDialog()"></button>
        </ng-template>
      </p-dialog>
    </p-card>
  `
})
export class ResourcePageComponent implements OnInit {
  title = "";
  resourcePath = "";
  moduleKey = "";
  rows: Record<string, unknown>[] = [];
  selectedRow: Record<string, unknown> | null = null;
  formError = "";
  showFormDialog = false;
  formMode: "create" | "edit" = "create";
  formData: Record<string, unknown> = {};
  showRelationDialog = false;
  relationDialogType: RelationDialogType = "userClaims";
  relationSelectedIds: number[] = [];
  relationLookupOptions: LookupOption[] = [];
  relationLookupLoading = false;
  /** Language rows for translate.langId dropdown and grid labels */
  languageLookupOptions: LookupOption[] = [];
  langIdToLabel: Record<number, string> = {};
  passwordValue = "";
  confirmPasswordValue = "";
  tableFilterValue = "";
  descriptor: ModuleDescriptor = {
    idKey: "id",
    columns: ["id"],
    fields: []
  };

  constructor(
    private readonly route: ActivatedRoute,
    private readonly apiService: ApiService,
    private readonly authService: AuthService,
    private readonly translationService: TranslationService,
    private readonly messageService: MessageService,
    private readonly confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    const key = this.route.snapshot.data["moduleKey"] as string;
    this.moduleKey = key;
    const moduleConfig = CLIENT_MODULES.find((module) => module.key === key);
    this.title = this.translate(`menu.${key}`, moduleConfig?.label ?? key);
    this.resourcePath = moduleConfig?.resourcePath ?? "";
    this.descriptor = MODULE_DESCRIPTORS[this.moduleKey] ?? this.descriptor;

    if (this.resourcePath) {
      if (this.moduleKey === "translate") {
        this.loadLanguageLookupOptions();
      }
      this.refresh();
    }
  }

  refresh(showMessage = true): void {
    if (!this.resourcePath) {
      this.rows = [];
      return;
    }

    if (this.moduleKey === "translate") {
      this.loadLanguageLookupOptions();
    }

    this.apiService.getList(this.resourcePath).subscribe({
      next: (response) => {
        this.rows = Array.isArray(response) ? (response as Record<string, unknown>[]) : [];
        this.selectedRow = null;
        this.tableFilterValue = "";
        if (showMessage) {
          this.notifyInfo(this.translate("notify.listRefreshed", "List refreshed"));
        }
      },
      error: () => {
        this.rows = [];
        this.notifyError(this.translate("notify.listLoadFailed", "List load failed"));
      }
    });
  }

  openCreateDialog(): void {
    if (!this.canCreateAction()) {
      return;
    }

    this.formMode = "create";
    this.formError = "";
    if (this.moduleKey === "translate") {
      this.loadLanguageLookupOptions();
    }
    this.formData = this.getDefaultFormData();
    this.showFormDialog = true;
  }

  openEditDialog(): void {
    if (!this.selectedRow || !this.canUpdateAction()) {
      return;
    }

    this.formMode = "edit";
    this.formError = "";
    if (this.moduleKey === "translate") {
      this.loadLanguageLookupOptions();
    }
    this.formData = this.getDefaultFormData();
    for (const field of this.descriptor.fields) {
      const raw = this.selectedRow[field.key];
      if (field.type === "lookup") {
        const n = Number(raw ?? 0);
        this.formData[field.key] = Number.isFinite(n) && n > 0 ? n : null;
      } else {
        this.formData[field.key] = raw ?? this.formData[field.key];
      }
    }
    this.showFormDialog = true;
  }

  saveFormDialog(): void {
    if (!this.resourcePath) {
      return;
    }

    const missing = this.descriptor.fields
      .filter((field) => field.required)
      .filter((field) => this.isFormFieldEmpty(field))
      .map((field) => field.key);

    if (missing.length > 0) {
      this.formError = `${this.translate("validation.missingRequiredPrefix", "Missing required fields:")} ${missing.join(", ")}`;
      this.notifyWarn(this.formError);
      return;
    }

    const payload = this.buildPayload();
    if (this.formMode === "edit") {
      payload[this.descriptor.idKey] = this.selectedId();
    }

    const request =
      this.formMode === "create"
        ? this.apiService.create(this.resourcePath, payload)
        : this.apiService.update(this.resourcePath, payload);

    request.subscribe({
      next: () => {
        this.showFormDialog = false;
        this.notifySuccess(
          this.formMode === "create"
            ? this.translate("notify.created", "Created successfully")
            : this.translate("notify.updated", "Updated successfully")
        );
        if (this.formMode === "edit") {
          const id = this.selectedId();
          if (id > 0) {
            this.upsertRow(id, payload);
          }
          return;
        }
        this.refresh(false);
      },
      error: () => {
        this.notifyError(this.translate("notify.saveFailed", "Save failed"));
      }
    });
  }

  deleteSelected(): void {
    if (!this.canDeleteAction()) {
      return;
    }

    const id = this.selectedId();
    if (!id) {
      return;
    }

    this.confirmDelete(() => {
      this.apiService.delete(this.resourcePath, String(id)).subscribe({
        next: () => {
          this.notifySuccess(this.translate("notify.deleted", "Deleted successfully"));
          this.removeRow(id);
        },
        error: () => {
          this.notifyError(this.translate("notify.deleteFailed", "Delete failed"));
        }
      });
    });
  }

  openRelationDialog(type: RelationDialogType): void {
    if (!this.selectedId()) {
      return;
    }
    this.relationDialogType = type;
    this.relationSelectedIds = [];
    this.relationLookupOptions = [];
    this.passwordValue = "";
    this.confirmPasswordValue = "";
    this.showRelationDialog = true;
    if (type !== "password") {
      this.loadRelationLookup(type);
    }
  }

  loadRelationData(): void {
    if (this.relationDialogType === "password") {
      return;
    }
    this.loadRelationCurrentSelection(this.relationDialogType);
  }

  saveRelationDialog(): void {
    const id = this.selectedId();
    if (!id) {
      return;
    }

    if (this.relationDialogType === "password") {
      if (!this.passwordValue || !this.confirmPasswordValue) {
        this.notifyWarn(this.translate("notify.passwordRequired", "Password fields are required"));
        return;
      }
      this.command("/Auth/user-password", {
        userId: id,
        password: this.passwordValue,
        confirmPassword: this.confirmPasswordValue
      });
      this.showRelationDialog = false;
      return;
    }

    const ids = this.relationSelectedIds;
    if (ids.length === 0) {
      this.notifyWarn(this.translate("notify.selectAtLeastOne", "Select at least one item"));
      return;
    }
    const payloadMap: Record<Exclude<RelationDialogType, "password">, unknown> = {
      userClaims: { UserId: id, ClaimIds: ids },
      userGroups: { UserId: id, GroupId: ids },
      groupClaims: { GroupId: id, ClaimIds: ids },
      groupUsers: { GroupId: id, UserIds: ids }
    };
    const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
      userClaims: "/user-claims",
      userGroups: "/user-groups",
      groupClaims: "/group-claims",
      groupUsers: "/user-groups/groups"
    };

    this.command(pathMap[this.relationDialogType], payloadMap[this.relationDialogType]);
    this.showRelationDialog = false;
  }

  selectedId(): number {
    if (!this.selectedRow) {
      return 0;
    }
    const raw = this.selectedRow[this.descriptor.idKey];
    const value = Number(raw ?? 0);
    return Number.isFinite(value) ? value : 0;
  }

  canClaim(claim: string): boolean {
    return this.authService.hasClaim(claim);
  }

  canCreateAction(): boolean {
    return !!this.descriptor.createClaim && this.canClaim(this.descriptor.createClaim);
  }

  canUpdateAction(): boolean {
    return !!this.descriptor.updateClaim && this.canClaim(this.descriptor.updateClaim);
  }

  canDeleteAction(): boolean {
    return !!this.descriptor.deleteClaim && this.canClaim(this.descriptor.deleteClaim);
  }

  displayValue(item: Record<string, unknown>, col: string): string {
    if (this.moduleKey === "translate" && col === "langId") {
      const id = Number(item[col] ?? 0);
      if (Number.isFinite(id) && id > 0 && this.langIdToLabel[id]) {
        return this.langIdToLabel[id];
      }
    }
    const value = item[col];
    if (value === null || value === undefined) {
      return "";
    }
    if (typeof value === "object") {
      return JSON.stringify(value);
    }
    return String(value);
  }

  private getDefaultFormData(): Record<string, unknown> {
    const defaults: Record<string, unknown> = {};
    for (const field of this.descriptor.fields) {
      if (field.type === "boolean") {
        defaults[field.key] = field.key === "status";
      } else if (field.type === "lookup") {
        defaults[field.key] = null;
      } else {
        defaults[field.key] = "";
      }
    }
    return defaults;
  }

  private buildPayload(): Record<string, unknown> {
    const payload: Record<string, unknown> = {};
    for (const field of this.descriptor.fields) {
      if (field.type === "number" || field.type === "lookup") {
        payload[field.key] = Number(this.formData[field.key] ?? 0);
      } else if (field.type === "boolean") {
        payload[field.key] = Boolean(this.formData[field.key]);
      } else {
        payload[field.key] = String(this.formData[field.key] ?? "");
      }
    }
    return payload;
  }

  private isFormFieldEmpty(field: FieldDef): boolean {
    const v = this.formData[field.key];
    if (field.type === "lookup" || field.type === "number") {
      const n = Number(v ?? 0);
      return !Number.isFinite(n) || n <= 0;
    }
    return this.isEmpty(v);
  }

  private loadLanguageLookupOptions(): void {
    this.apiService.getList("/languages").subscribe({
      next: (response) => {
        const rows = Array.isArray(response) ? (response as Record<string, unknown>[]) : [];
        const map: Record<number, string> = {};
        this.languageLookupOptions = rows
          .map((row) => {
            const id = Number(row["id"] ?? row["Id"] ?? 0);
            if (!Number.isFinite(id) || id <= 0) {
              return null;
            }
            const name = String(row["name"] ?? row["Name"] ?? "").trim();
            const code = String(row["code"] ?? row["Code"] ?? "").trim();
            const label = name && code ? `${name} (${code})` : name || code || `#${id}`;
            map[id] = label;
            return { value: id, label } as LookupOption;
          })
          .filter((item): item is LookupOption => item !== null);
        this.langIdToLabel = map;
      },
      error: () => {
        this.languageLookupOptions = [];
        this.notifyWarn(this.translate("notify.lookupLoadFailed", "Lookup data load failed"));
      }
    });
  }

  private loadRelationLookup(type: Exclude<RelationDialogType, "password">): void {
    const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
      userClaims: "/operation-claims",
      userGroups: "/groups",
      groupClaims: "/operation-claims",
      groupUsers: "/users"
    };
    const resourcePath = pathMap[type];
    if (!resourcePath) {
      this.relationLookupOptions = [];
      return;
    }

    this.relationLookupLoading = true;
    this.apiService.getList(resourcePath).subscribe({
      next: (response) => {
        const rows = Array.isArray(response) ? (response as Record<string, unknown>[]) : [];
        this.relationLookupOptions = rows
          .map((row) => this.toLookupOption(type, row))
          .filter((item): item is LookupOption => item !== null);
        this.relationLookupLoading = false;
        this.loadRelationCurrentSelection(type);
      },
      error: () => {
        this.relationLookupOptions = [];
        this.relationLookupLoading = false;
        this.notifyWarn(this.translate("notify.lookupLoadFailed", "Lookup data load failed"));
      }
    });
  }

  private extractRelationSelectedIds(
    type: Exclude<RelationDialogType, "password">,
    relationRows: Record<string, unknown>[]
  ): number[] {
    const pickId = (row: Record<string, unknown>, ...keys: string[]) => {
      for (const key of keys) {
        const value = Number(row[key] ?? 0);
        if (Number.isFinite(value) && value > 0) {
          return value;
        }
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
  }

  private loadRelationCurrentSelection(type: Exclude<RelationDialogType, "password">): void {
    const id = this.selectedId();
    if (!id) {
      return;
    }
    const pathMap: Record<Exclude<RelationDialogType, "password">, string> = {
      userClaims: `/user-claims/users/${id}`,
      userGroups: `/user-groups/users/${id}/groups`,
      groupClaims: `/group-claims/groups/${id}`,
      groupUsers: `/user-groups/groups/${id}/users`
    };
    this.apiService.getByPath(pathMap[type]).subscribe({
      next: (response) => {
        const relationRows = Array.isArray(response) ? (response as Record<string, unknown>[]) : [];
        this.relationSelectedIds = this.extractRelationSelectedIds(type, relationRows);
      },
      error: () => {
        this.relationSelectedIds = [];
      }
    });
  }

  private toLookupOption(type: Exclude<RelationDialogType, "password">, row: Record<string, unknown>): LookupOption | null {
    const idByKey = (...keys: string[]) => {
      for (const key of keys) {
        const value = Number(row[key] ?? 0);
        if (Number.isFinite(value) && value > 0) {
          return value;
        }
      }
      return 0;
    };

    const textByKey = (...keys: string[]) => {
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
        ? idByKey("userId", "id")
        : type === "userClaims" || type === "groupClaims"
          ? idByKey("id", "operationClaimId")
          : idByKey("id", "groupId");
    if (!id) {
      return null;
    }

    const label =
      type === "groupUsers"
        ? textByKey("fullName", "email", "userName")
        : type === "userClaims" || type === "groupClaims"
          ? textByKey("name", "alias")
          : textByKey("groupName", "name");

    return {
      value: id,
      label: label ? `${label} (#${id})` : `#${id}`
    };
  }

  private isEmpty(value: unknown): boolean {
    return value === undefined || value === null || (typeof value === "string" && value.trim() === "");
  }

  private query(path: string): void {
    this.apiService.getByPath(path).subscribe({
      next: (response) => {
        this.rows = Array.isArray(response) ? (response as Record<string, unknown>[]) : [response as Record<string, unknown>];
        this.selectedRow = null;
        this.notifyInfo(this.translate("notify.queryCompleted", "Query completed"));
      },
      error: () => {
        this.rows = [];
        this.notifyError(this.translate("notify.queryFailed", "Query failed"));
      }
    });
  }

  private command(path: string, payload: unknown): void {
    this.apiService.putByPath(path, payload).subscribe({
      next: () => {
        this.notifySuccess(this.translate("notify.operationCompleted", "Operation completed"));
        this.refresh(false);
      },
      error: () => {
        this.notifyError(this.translate("notify.operationFailed", "Operation failed"));
      }
    });
  }

  private upsertRow(id: number, payload: Record<string, unknown>): void {
    this.rows = this.rows.map((row) => {
      const rowId = Number(row[this.descriptor.idKey] ?? 0);
      if (rowId !== id) {
        return row;
      }
      return { ...row, ...payload, [this.descriptor.idKey]: id };
    });
    this.selectedRow = this.rows.find((row) => Number(row[this.descriptor.idKey] ?? 0) === id) ?? null;
  }

  private removeRow(id: number): void {
    this.rows = this.rows.filter((row) => Number(row[this.descriptor.idKey] ?? 0) !== id);
    this.selectedRow = null;
  }

  private confirmDelete(accept: () => void): void {
    this.confirmationService.confirm({
      message: this.translate("dialog.deleteConfirm", "Are you sure you want to delete this record?"),
      header: this.translate("dialog.confirmDelete", "Confirm delete"),
      icon: "pi pi-exclamation-triangle",
      accept
    });
  }

  private notifySuccess(detail: string): void {
    this.messageService.add({
      severity: "success",
      summary: this.translate("notify.severity.success", "Success"),
      detail
    });
  }

  private notifyError(detail: string): void {
    this.messageService.add({
      severity: "error",
      summary: this.translate("notify.severity.error", "Error"),
      detail
    });
  }

  private notifyWarn(detail: string): void {
    this.messageService.add({
      severity: "warn",
      summary: this.translate("notify.severity.warning", "Warning"),
      detail
    });
  }

  private notifyInfo(detail: string): void {
    this.messageService.add({
      severity: "info",
      summary: this.translate("notify.severity.info", "Info"),
      detail
    });
  }

  translate(key: string, fallback: string): string {
    return this.translationService.t(key, fallback);
  }
}
