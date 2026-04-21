import { useCallback, useEffect, useMemo, useState } from "react";
import { Card } from "primereact/card";
import { Tag } from "primereact/tag";
import { DataTable, type DataTablePageEvent } from "primereact/datatable";
import { Column } from "primereact/column";
import { apiClient, authStore } from "../api/client";

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
  totalPages: number;
  serverProcessingMs?: number;
  actualGeneratedRows?: number;
  actualGenerationMs?: number;
  actualNsPerRow?: number;
  simulatedTotalRecords?: number;
  estimatedServerMsForSimulatedTotal?: number;
  estimatedRowsPerSecond?: number;
};

const DEFAULT_PAGE_SIZE = 50;

export function ShowcasePage() {
  const [rows, setRows] = useState<ShowcaseRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [first, setFirst] = useState(0);
  const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE);
  const [totalRecords, setTotalRecords] = useState(1_000_000);
  const [lastLoadMs, setLastLoadMs] = useState<number | null>(null);
  const [serverProcessingMs, setServerProcessingMs] = useState<number | null>(null);
  const [actualGeneratedRows, setActualGeneratedRows] = useState<number | null>(null);
  const [actualGenerationMs, setActualGenerationMs] = useState<number | null>(null);
  const [actualNsPerRow, setActualNsPerRow] = useState<number | null>(null);
  const [simulatedTotalRecords, setSimulatedTotalRecords] = useState<number | null>(null);
  const [estimatedServerMs, setEstimatedServerMs] = useState<number | null>(null);
  const [estimatedRowsPerSecond, setEstimatedRowsPerSecond] = useState<number | null>(null);
  const [errorText, setErrorText] = useState<string | null>(null);

  const t = useCallback((key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback)), []);

  const loadPage = useCallback(async (nextFirst: number, nextPageSize: number) => {
    const page = Math.floor(nextFirst / nextPageSize) + 1;
    setLoading(true);
    setErrorText(null);
    const startedAt = performance.now();
    try {
      const { data } = await apiClient.get<ShowcaseResponse>("/showcase/rows", {
        params: {
          page,
          pageSize: nextPageSize
        }
      });

      const elapsed = performance.now() - startedAt;
      setRows(Array.isArray(data?.items) ? data.items : []);
      setTotalRecords(Number.isFinite(data?.totalRecords) ? Number(data.totalRecords) : 1_000_000);
      setLastLoadMs(elapsed);
      setServerProcessingMs(
        Number.isFinite(data?.serverProcessingMs as number) ? Number(data.serverProcessingMs) : null
      );
      setActualGeneratedRows(
        Number.isFinite(data?.actualGeneratedRows as number) ? Number(data.actualGeneratedRows) : null
      );
      setActualGenerationMs(
        Number.isFinite(data?.actualGenerationMs as number) ? Number(data.actualGenerationMs) : null
      );
      setActualNsPerRow(
        Number.isFinite(data?.actualNsPerRow as number) ? Number(data.actualNsPerRow) : null
      );
      setSimulatedTotalRecords(
        Number.isFinite(data?.simulatedTotalRecords as number) ? Number(data.simulatedTotalRecords) : null
      );
      setEstimatedServerMs(
        Number.isFinite(data?.estimatedServerMsForSimulatedTotal as number) ? Number(data.estimatedServerMsForSimulatedTotal) : null
      );
      setEstimatedRowsPerSecond(
        Number.isFinite(data?.estimatedRowsPerSecond as number) ? Number(data.estimatedRowsPerSecond) : null
      );
      setFirst((Math.max(1, Number(data?.pageNumber ?? page)) - 1) * nextPageSize);
      setPageSize(Number(data?.pageSize ?? nextPageSize));
    } catch {
      setRows([]);
      setErrorText(t("notify.listLoadFailed", "List load failed"));
      setLastLoadMs(null);
      setServerProcessingMs(null);
      setActualGeneratedRows(null);
      setActualGenerationMs(null);
      setActualNsPerRow(null);
      setSimulatedTotalRecords(null);
      setEstimatedServerMs(null);
      setEstimatedRowsPerSecond(null);
    } finally {
      setLoading(false);
    }
  }, [t]);

  useEffect(() => {
    void loadPage(0, DEFAULT_PAGE_SIZE);
  }, [loadPage]);

  const onPage = (event: DataTablePageEvent) => {
    const nextFirst = event.first ?? 0;
    const nextRows = event.rows ?? DEFAULT_PAGE_SIZE;
    void loadPage(nextFirst, nextRows);
  };

  const timingText = useMemo(() => {
    if (errorText) {
      return errorText;
    }
    if (lastLoadMs == null) {
      return t("common.loading", "Loading...");
    }
    const roundTrip = `Son yukleme: ${lastLoadMs.toFixed(1)} ms (round-trip)`;
    if (serverProcessingMs == null) {
      return roundTrip;
    }
    return `${roundTrip} | Sunucu: ${serverProcessingMs.toFixed(1)} ms`;
  }, [errorText, lastLoadMs, serverProcessingMs, t]);

  const estimatedText = useMemo(() => {
    if (errorText || simulatedTotalRecords == null || estimatedServerMs == null || estimatedRowsPerSecond == null) {
      return null;
    }
    const seconds = estimatedServerMs / 1000;
    return `1 milyar satir tahmini sunucu uretim suresi: ${seconds.toFixed(2)} s | ${estimatedRowsPerSecond.toLocaleString()} satir/s`;
  }, [errorText, simulatedTotalRecords, estimatedServerMs, estimatedRowsPerSecond]);

  const actualApiGenerationText = useMemo(() => {
    if (errorText || actualGeneratedRows == null || actualGenerationMs == null || actualNsPerRow == null) {
      return null;
    }
    return `API gercek uretim: ${actualGeneratedRows.toLocaleString()} satir / ${actualGenerationMs.toFixed(2)} ms | ${actualNsPerRow.toLocaleString()} ns/satir`;
  }, [errorText, actualGeneratedRows, actualGenerationMs, actualNsPerRow]);

  return (
    <Card title={t("menu.showcase", "Showcase")}>
      <Tag value="/showcase/rows" severity="info" />
      <DataTable
        value={rows}
        lazy
        paginator
        loading={loading}
        rows={pageSize}
        first={first}
        totalRecords={totalRecords}
        rowsPerPageOptions={[25, 50, 100, 200]}
        onPage={onPage}
        tableStyle={{ marginTop: "1rem" }}
        showGridlines
        size="small"
      >
        <Column field="id" header="id" sortable />
        <Column field="code" header="code" />
        <Column field="amount" header="amount" />
        <Column field="createdAt" header="createdAt" />
        <Column field="category" header="category" />
        <Column field="status" header="status" />
      </DataTable>
      <div style={{ marginTop: "0.75rem", display: "grid", gap: "0.45rem" }}>
        <div style={{ padding: "0.55rem 0.7rem", border: "1px solid var(--surface-border)", borderRadius: "8px" }}>
          <strong>KPI / Anlik:</strong> {timingText}
        </div>
        {actualApiGenerationText && (
          <div style={{ padding: "0.55rem 0.7rem", border: "1px solid var(--surface-border)", borderRadius: "8px" }}>
            <strong>KPI / API Gercek:</strong> {actualApiGenerationText}
          </div>
        )}
        {estimatedText && (
          <div style={{ padding: "0.55rem 0.7rem", border: "1px solid var(--surface-border)", borderRadius: "8px" }}>
            <strong>KPI / Tahmin:</strong> {estimatedText}
          </div>
        )}
      </div>
    </Card>
  );
}
