<script setup lang="ts">
import { onMounted, ref } from "vue";
import Card from "primevue/card";
import Tag from "primevue/tag";
import DataTable, { type DataTablePageEvent } from "primevue/datatable";
import Column from "primevue/column";
import { apiClient, authStore } from "../services/api";

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

const rows = ref<ShowcaseRow[]>([]);
const loading = ref(false);
const first = ref(0);
const pageSize = ref(50);
const totalRecords = ref(1_000_000);
const lastLoadMs = ref<number | null>(null);
const serverProcessingMs = ref<number | null>(null);
const actualGeneratedRows = ref<number | null>(null);
const actualGenerationMs = ref<number | null>(null);
const actualNsPerRow = ref<number | null>(null);
const simulatedTotalRecords = ref<number | null>(null);
const estimatedServerMs = ref<number | null>(null);
const estimatedRowsPerSecond = ref<number | null>(null);
const errorText = ref<string | null>(null);

const t = (key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback));

const timingText = () => {
  if (errorText.value) {
    return errorText.value;
  }
  if (lastLoadMs.value == null) {
    return t("common.loading", "Loading...");
  }
  const roundTrip = `Son yukleme: ${lastLoadMs.value.toFixed(1)} ms (round-trip)`;
  if (serverProcessingMs.value == null) {
    return roundTrip;
  }
  return `${roundTrip} | Sunucu: ${serverProcessingMs.value.toFixed(1)} ms`;
};

const estimatedText = () => {
  if (
    errorText.value ||
    simulatedTotalRecords.value == null ||
    estimatedServerMs.value == null ||
    estimatedRowsPerSecond.value == null
  ) {
    return "";
  }
  const seconds = estimatedServerMs.value / 1000;
  return `1 milyar satir tahmini sunucu uretim suresi: ${seconds.toFixed(2)} s | ${estimatedRowsPerSecond.value.toLocaleString()} satir/s`;
};

const actualApiGenerationText = () => {
  if (errorText.value || actualGeneratedRows.value == null || actualGenerationMs.value == null || actualNsPerRow.value == null) {
    return "";
  }
  return `API gercek uretim: ${actualGeneratedRows.value.toLocaleString()} satir / ${actualGenerationMs.value.toFixed(2)} ms | ${actualNsPerRow.value.toLocaleString()} ns/satir`;
};

const loadPage = async (nextFirst: number, nextPageSize: number) => {
  const page = Math.floor(nextFirst / nextPageSize) + 1;
  loading.value = true;
  errorText.value = null;
  const startedAt = performance.now();
  try {
    const { data } = await apiClient.get<ShowcaseResponse>("/showcase/rows", {
      params: {
        page,
        pageSize: nextPageSize
      }
    });

    rows.value = Array.isArray(data?.items) ? data.items : [];
    pageSize.value = Number(data?.pageSize ?? nextPageSize);
    totalRecords.value = Number.isFinite(data?.totalRecords) ? Number(data.totalRecords) : 1_000_000;
    first.value = (Math.max(1, Number(data?.pageNumber ?? page)) - 1) * pageSize.value;
    lastLoadMs.value = performance.now() - startedAt;
    serverProcessingMs.value = Number.isFinite(data?.serverProcessingMs as number) ? Number(data.serverProcessingMs) : null;
    actualGeneratedRows.value = Number.isFinite(data?.actualGeneratedRows as number) ? Number(data.actualGeneratedRows) : null;
    actualGenerationMs.value = Number.isFinite(data?.actualGenerationMs as number) ? Number(data.actualGenerationMs) : null;
    actualNsPerRow.value = Number.isFinite(data?.actualNsPerRow as number) ? Number(data.actualNsPerRow) : null;
    simulatedTotalRecords.value = Number.isFinite(data?.simulatedTotalRecords as number) ? Number(data.simulatedTotalRecords) : null;
    estimatedServerMs.value = Number.isFinite(data?.estimatedServerMsForSimulatedTotal as number)
      ? Number(data.estimatedServerMsForSimulatedTotal)
      : null;
    estimatedRowsPerSecond.value = Number.isFinite(data?.estimatedRowsPerSecond as number) ? Number(data.estimatedRowsPerSecond) : null;
  } catch {
    rows.value = [];
    errorText.value = t("notify.listLoadFailed", "List load failed");
    lastLoadMs.value = null;
    serverProcessingMs.value = null;
    actualGeneratedRows.value = null;
    actualGenerationMs.value = null;
    actualNsPerRow.value = null;
    simulatedTotalRecords.value = null;
    estimatedServerMs.value = null;
    estimatedRowsPerSecond.value = null;
  } finally {
    loading.value = false;
  }
};

const onPage = (event: DataTablePageEvent) => {
  void loadPage(event.first ?? 0, event.rows ?? 50);
};

onMounted(async () => {
  await loadPage(0, 50);
});
</script>

<template>
  <Card>
    <template #title>{{ t("menu.showcase", "Showcase") }}</template>
    <template #content>
      <Tag value="/showcase/rows" severity="info" />
      <DataTable
        :value="rows"
        :lazy="true"
        :paginator="true"
        :loading="loading"
        :rows="pageSize"
        :first="first"
        :totalRecords="totalRecords"
        :rowsPerPageOptions="[25, 50, 100, 200]"
        @page="onPage"
        :tableStyle="{ marginTop: '1rem' }"
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
      <div class="metric-box">
        <strong>KPI / Anlik:</strong> {{ timingText() }}
      </div>
      <div v-if="actualApiGenerationText()" class="metric-box">
        <strong>KPI / API Gercek:</strong> {{ actualApiGenerationText() }}
      </div>
      <div v-if="estimatedText()" class="metric-box">
        <strong>KPI / Tahmin:</strong> {{ estimatedText() }}
      </div>
    </template>
  </Card>
</template>

<style scoped>
.timing-row {
  display: block;
  margin-top: 0.75rem;
  opacity: 0.85;
}

.metric-box {
  margin-top: 0.75rem;
  padding: 0.55rem 0.7rem;
  border: 1px solid var(--p-content-border-color, #d7dce2);
  border-radius: 8px;
  opacity: 0.92;
}
</style>
