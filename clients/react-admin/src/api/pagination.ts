import { crudApi } from "./client";

export const PAGE_SIZE = 100;

type Row = Record<string, unknown>;

const unwrapRows = (body: unknown): Row[] | null => {
  if (Array.isArray(body)) return body as Row[];
  if (body && typeof body === "object" && Array.isArray((body as { data?: unknown }).data)) {
    return (body as { data: Row[] }).data;
  }
  return null;
};

export const fetchAllRows = async (path: string): Promise<Row[]> => {
  const collected: Row[] = [];
  let pageNumber = 1;
  for (;;) {
    const { data } = await crudApi.list(path, { pageNumber, pageSize: PAGE_SIZE });
    const pageRows = unwrapRows(data);
    if (!pageRows) return collected;
    collected.push(...pageRows);
    const isWrapped = !Array.isArray(data);
    if (!isWrapped) break;
    const totalRecords = Number((data as { totalRecords?: unknown }).totalRecords);
    if (!Number.isFinite(totalRecords) || collected.length >= totalRecords || pageRows.length === 0) break;
    pageNumber += 1;
  }
  return collected;
};
