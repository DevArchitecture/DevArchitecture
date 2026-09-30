import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, of, switchMap } from "rxjs";
import { environment } from "../../environments/environment";

export const PAGE_SIZE = 100;

export type ClientModule = {
  key: string;
  label: string;
  route: string;
  resourcePath?: string;
};

export const CLIENT_MODULES: ClientModule[] = [
  { key: "dashboard", label: "Dashboard", route: "/dashboard" },
  { key: "user", label: "Users", route: "/user", resourcePath: "/users" },
  { key: "group", label: "Groups", route: "/group", resourcePath: "/groups" },
  { key: "language", label: "Languages", route: "/language", resourcePath: "/languages" },
  { key: "translate", label: "Translates", route: "/translate", resourcePath: "/translates" },
  { key: "operationclaim", label: "Operation Claims", route: "/operationclaim", resourcePath: "/operation-claims" },
  { key: "log", label: "Logs", route: "/log", resourcePath: "/logs" },
  { key: "showcase", label: "Showcase", route: "/showcase", resourcePath: "/showcase/rows" }
];

@Injectable({ providedIn: "root" })
export class ApiService {
  constructor(private readonly http: HttpClient) {}
  private readonly noCacheHeaders = new HttpHeaders({
    "Cache-Control": "no-cache, no-store, must-revalidate",
    Pragma: "no-cache",
    Expires: "0"
  });

  getList(resourcePath: string): Observable<unknown[]> {
    return this.http.get<unknown[]>(`${environment.apiBaseUrl}${resourcePath}`, {
      headers: this.noCacheHeaders,
      params: new HttpParams().set("_ts", Date.now().toString())
    });
  }

  private getPage(resourcePath: string, pageNumber: number): Observable<unknown> {
    return this.http.get<unknown>(`${environment.apiBaseUrl}${resourcePath}`, {
      headers: this.noCacheHeaders,
      params: new HttpParams()
        .set("_ts", Date.now().toString())
        .set("pageNumber", pageNumber.toString())
        .set("pageSize", PAGE_SIZE.toString())
    });
  }

  private unwrapRows(body: unknown): unknown[] | null {
    if (Array.isArray(body)) return body;
    if (body && typeof body === "object" && Array.isArray((body as { data?: unknown }).data)) {
      return (body as { data: unknown[] }).data;
    }
    return null;
  }

  getAllRows(resourcePath: string): Observable<unknown[]> {
    const collect = (pageNumber: number, acc: unknown[]): Observable<unknown[]> =>
      this.getPage(resourcePath, pageNumber).pipe(
        switchMap((body) => {
          const pageRows = this.unwrapRows(body);
          if (!pageRows) return of(acc);
          const all = [...acc, ...pageRows];
          if (Array.isArray(body)) return of(all);
          const totalRecords = Number((body as { totalRecords?: unknown }).totalRecords);
          if (!Number.isFinite(totalRecords) || all.length >= totalRecords || pageRows.length === 0) {
            return of(all);
          }
          return collect(pageNumber + 1, all);
        })
      );
    return collect(1, []);
  }

  create(resourcePath: string, payload: unknown): Observable<unknown> {
    return this.http.post(`${environment.apiBaseUrl}${resourcePath}`, payload, {
      responseType: "text" as "json"
    });
  }

  update(resourcePath: string, payload: unknown): Observable<unknown> {
    return this.http.put(`${environment.apiBaseUrl}${resourcePath}`, payload, {
      responseType: "text" as "json"
    });
  }

  delete(resourcePath: string, id: string): Observable<unknown> {
    return this.http.delete(`${environment.apiBaseUrl}${resourcePath}/${id}`, {
      responseType: "text" as "json"
    });
  }

  getByPath(path: string, queryParams?: Record<string, string | number>): Observable<unknown> {
    let params = new HttpParams().set("_ts", Date.now().toString());
    if (queryParams) {
      for (const [key, value] of Object.entries(queryParams)) {
        params = params.set(key, String(value));
      }
    }
    return this.http.get(`${environment.apiBaseUrl}${path}`, {
      headers: this.noCacheHeaders,
      params
    });
  }

  putByPath(path: string, payload: unknown): Observable<unknown> {
    return this.http.put(`${environment.apiBaseUrl}${path}`, payload, {
      responseType: "text" as "json"
    });
  }
}
