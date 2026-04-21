import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CardModule } from "primeng/card";
import { TagModule } from "primeng/tag";
import { TableLazyLoadEvent, TableModule } from "primeng/table";
import { ApiService } from "../core/api.service";
import { TranslationService } from "../core/translation.service";

type ShowcaseRow = {
  id: number;
  code: string;
  amount: number;
  createdAt: string;
  category: string;
  status: boolean;
};

type ShowcaseResponse = {
  items: ShowcaseRow[];
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  serverProcessingMs?: number;
  actualGeneratedRows?: number;
  actualGenerationMs?: number;
  actualNsPerRow?: number;
  simulatedTotalRecords?: number;
  estimatedServerMsForSimulatedTotal?: number;
  estimatedRowsPerSecond?: number;
};

@Component({
  selector: "app-showcase-page",
  standalone: true,
  imports: [CommonModule, CardModule, TagModule, TableModule],
  template: `
    <p-card [header]="translate('menu.showcase', 'Showcase')">
      <p-tag value="/showcase/rows" severity="info"></p-tag>
      <p-table
        [value]="rows"
        [lazy]="true"
        [paginator]="true"
        [rows]="pageSize"
        [first]="first"
        [totalRecords]="totalRecords"
        [rowsPerPageOptions]="[25, 50, 100, 200]"
        [loading]="loading"
        [tableStyle]="{ 'margin-top': '1rem' }"
        [showGridlines]="true"
        [size]="'small'"
        (onLazyLoad)="onLazyLoad($event)"
      >
        <ng-template pTemplate="header">
          <tr>
            <th>id</th>
            <th>code</th>
            <th>amount</th>
            <th>createdAt</th>
            <th>category</th>
            <th>status</th>
          </tr>
        </ng-template>
        <ng-template pTemplate="body" let-item>
          <tr>
            <td>{{ item.id }}</td>
            <td>{{ item.code }}</td>
            <td>{{ item.amount }}</td>
            <td>{{ item.createdAt }}</td>
            <td>{{ item.category }}</td>
            <td>{{ item.status }}</td>
          </tr>
        </ng-template>
      </p-table>
      <div class="metric-box"><strong>KPI / Anlik:</strong> {{ timingText }}</div>
      <div *ngIf="actualApiGenerationText" class="metric-box"><strong>KPI / API Gercek:</strong> {{ actualApiGenerationText }}</div>
      <div *ngIf="estimatedText" class="metric-box"><strong>KPI / Tahmin:</strong> {{ estimatedText }}</div>
    </p-card>
  `,
  styles: [
    `
      .timing-row {
        display: block;
        margin-top: 0.75rem;
        opacity: 0.85;
      }
      .metric-box {
        margin-top: 0.75rem;
        padding: 0.55rem 0.7rem;
        border: 1px solid #d7dce2;
        border-radius: 8px;
        opacity: 0.92;
      }
    `
  ]
})
export class ShowcasePageComponent implements OnInit {
  rows: ShowcaseRow[] = [];
  first = 0;
  pageSize = 50;
  totalRecords = 1_000_000;
  loading = false;
  timingText = "Loading...";
  actualApiGenerationText = "";
  estimatedText = "";

  private lastLoadMs: number | null = null;
  private serverProcessingMs: number | null = null;
  private actualGeneratedRows: number | null = null;
  private actualGenerationMs: number | null = null;
  private actualNsPerRow: number | null = null;
  private simulatedTotalRecords: number | null = null;
  private estimatedServerMs: number | null = null;
  private estimatedRowsPerSecond: number | null = null;
  private loadError: string | null = null;

  constructor(
    private readonly apiService: ApiService,
    private readonly translationService: TranslationService
  ) {}

  ngOnInit(): void {
    this.loadPage(0, this.pageSize);
  }

  onLazyLoad(event: TableLazyLoadEvent): void {
    this.loadPage(event.first ?? 0, event.rows ?? this.pageSize);
  }

  translate(key: string, fallback: string): string {
    return this.translationService.t(key, fallback);
  }

  private loadPage(nextFirst: number, nextPageSize: number): void {
    const page = Math.floor(nextFirst / nextPageSize) + 1;
    const startedAt = performance.now();
    this.loading = true;
    this.loadError = null;
    this.updateTimingText();

    this.apiService.getByPath("/showcase/rows", { page, pageSize: nextPageSize }).subscribe({
      next: (response) => {
        const payload = response as ShowcaseResponse;
        this.rows = Array.isArray(payload?.items) ? payload.items : [];
        this.pageSize = Number(payload?.pageSize ?? nextPageSize);
        this.totalRecords = Number.isFinite(payload?.totalRecords) ? Number(payload.totalRecords) : 1_000_000;
        this.first = (Math.max(1, Number(payload?.pageNumber ?? page)) - 1) * this.pageSize;
        this.lastLoadMs = performance.now() - startedAt;
        this.serverProcessingMs = Number.isFinite(payload?.serverProcessingMs as number) ? Number(payload.serverProcessingMs) : null;
        this.actualGeneratedRows = Number.isFinite(payload?.actualGeneratedRows as number) ? Number(payload.actualGeneratedRows) : null;
        this.actualGenerationMs = Number.isFinite(payload?.actualGenerationMs as number) ? Number(payload.actualGenerationMs) : null;
        this.actualNsPerRow = Number.isFinite(payload?.actualNsPerRow as number) ? Number(payload.actualNsPerRow) : null;
        this.simulatedTotalRecords = Number.isFinite(payload?.simulatedTotalRecords as number)
          ? Number(payload.simulatedTotalRecords)
          : null;
        this.estimatedServerMs = Number.isFinite(payload?.estimatedServerMsForSimulatedTotal as number)
          ? Number(payload.estimatedServerMsForSimulatedTotal)
          : null;
        this.estimatedRowsPerSecond = Number.isFinite(payload?.estimatedRowsPerSecond as number)
          ? Number(payload.estimatedRowsPerSecond)
          : null;
        this.loading = false;
        this.updateTimingText();
      },
      error: () => {
        this.rows = [];
        this.lastLoadMs = null;
        this.serverProcessingMs = null;
        this.actualGeneratedRows = null;
        this.actualGenerationMs = null;
        this.actualNsPerRow = null;
        this.simulatedTotalRecords = null;
        this.estimatedServerMs = null;
        this.estimatedRowsPerSecond = null;
        this.loadError = this.translate("notify.listLoadFailed", "List load failed");
        this.loading = false;
        this.updateTimingText();
      }
    });
  }

  private updateTimingText(): void {
    if (this.loadError) {
      this.timingText = this.loadError;
      this.actualApiGenerationText = "";
      this.estimatedText = "";
      return;
    }
    if (this.lastLoadMs == null) {
      this.timingText = this.translate("common.loading", "Loading...");
      this.actualApiGenerationText = "";
      this.estimatedText = "";
      return;
    }

    const roundTrip = `Son yukleme: ${this.lastLoadMs.toFixed(1)} ms (round-trip)`;
    this.timingText = this.serverProcessingMs == null
      ? roundTrip
      : `${roundTrip} | Sunucu: ${this.serverProcessingMs.toFixed(1)} ms`;

    if (this.actualGeneratedRows != null && this.actualGenerationMs != null && this.actualNsPerRow != null) {
      this.actualApiGenerationText = `API gercek uretim: ${this.actualGeneratedRows.toLocaleString()} satir / ${this.actualGenerationMs.toFixed(2)} ms | ${this.actualNsPerRow.toLocaleString()} ns/satir`;
    } else {
      this.actualApiGenerationText = "";
    }

    if (
      this.simulatedTotalRecords != null &&
      this.estimatedServerMs != null &&
      this.estimatedRowsPerSecond != null
    ) {
      const seconds = this.estimatedServerMs / 1000;
      this.estimatedText = `1 milyar satir tahmini sunucu uretim suresi: ${seconds.toFixed(2)} s | ${this.estimatedRowsPerSecond.toLocaleString()} satir/s`;
      return;
    }

    this.estimatedText = "";
  }
}
