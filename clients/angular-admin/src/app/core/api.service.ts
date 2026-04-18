import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

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
  { key: "log", label: "Logs", route: "/log", resourcePath: "/logs" }
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

  getByPath(path: string): Observable<unknown> {
    return this.http.get(`${environment.apiBaseUrl}${path}`, {
      headers: this.noCacheHeaders,
      params: new HttpParams().set("_ts", Date.now().toString())
    });
  }

  putByPath(path: string, payload: unknown): Observable<unknown> {
    return this.http.put(`${environment.apiBaseUrl}${path}`, payload, {
      responseType: "text" as "json"
    });
  }
}
