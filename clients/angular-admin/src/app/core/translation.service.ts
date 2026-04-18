import { Injectable, signal } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError, map, Observable, of, tap } from "rxjs";
import { environment } from "../../environments/environment";
import { normalizeUiLanguage } from "./ui-language-code";

type TranslationItem = {
  code?: string;
  key?: string;
  name?: string;
  value?: string;
  text?: string;
  translation?: string;
};

@Injectable({ providedIn: "root" })
export class TranslationService {
  private readonly langKey = "devarch.lang";
  readonly currentLang = signal<string>(normalizeUiLanguage(localStorage.getItem(this.langKey)));
  readonly dictionary = signal<Record<string, string>>({});

  constructor(private readonly http: HttpClient) {
    const raw = localStorage.getItem(this.langKey);
    const n = normalizeUiLanguage(raw);
    if (raw != null && raw.trim() !== n) {
      localStorage.setItem(this.langKey, n);
    }
  }

  load(lang?: string): Observable<void> {
    const targetLang = normalizeUiLanguage(lang ?? this.currentLang());
    this.setLang(targetLang);
    // Drop previous-language entries immediately so toasts and labels do not keep showing
    // stale API strings until the new request completes (otherwise English UI still shows Turkish notifications).
    this.dictionary.set({});
    return this.http.get<unknown>(`${environment.apiBaseUrl}/translates/languages/${targetLang}`, {
      headers: { "x-dev-arch-version": environment.apiVersion }
    }).pipe(
      map((response) => this.extractList(response)),
      tap((list) => this.dictionary.set(this.toDictionary(list))),
      map(() => void 0),
      catchError(() => {
        this.dictionary.set({});
        return of(void 0);
      })
    );
  }

  t(key: string, fallback?: string): string {
    const dict = this.dictionary();
    for (const candidate of this.getCandidates(key)) {
      if (dict[candidate]) {
        return dict[candidate];
      }
    }
    return fallback ?? key;
  }

  setLang(lang: string): void {
    const n = normalizeUiLanguage(lang);
    this.currentLang.set(n);
    localStorage.setItem(this.langKey, n);
  }

  private extractList(payload: unknown): TranslationItem[] {
    if (Array.isArray(payload)) {
      return payload as TranslationItem[];
    }
    if (payload && typeof payload === "object") {
      const container = payload as Record<string, unknown>;
      if (Array.isArray(container["data"])) return container["data"] as TranslationItem[];
      if (Array.isArray(container["items"])) return container["items"] as TranslationItem[];
      if (Array.isArray(container["result"])) return container["result"] as TranslationItem[];
      const nestedData = container["data"];
      if (nestedData && typeof nestedData === "object" && !Array.isArray(nestedData)) {
        const fromNested = this.entriesFromFlatStringMap(nestedData as Record<string, unknown>);
        if (fromNested.length > 0) {
          return fromNested;
        }
      }
      const fromFlat = this.entriesFromFlatStringMap(container);
      if (fromFlat.length > 0) {
        return fromFlat;
      }
    }
    return [];
  }

  private entriesFromFlatStringMap(map: Record<string, unknown>): TranslationItem[] {
    const keys = Object.keys(map);
    if (keys.length === 0) {
      return [];
    }
    const out: TranslationItem[] = [];
    for (const k of keys) {
      const v = map[k];
      if (typeof v !== "string") {
        return [];
      }
      out.push({ code: k, value: v });
    }
    return out;
  }

  private toDictionary(list: TranslationItem[]): Record<string, string> {
    const next: Record<string, string> = {};
    for (const item of list) {
      const code = String(item.code ?? item.key ?? item.name ?? "").trim();
      const value = String(item.value ?? item.text ?? item.translation ?? "").trim();
      if (code && value) {
        for (const candidate of this.getCandidates(code)) {
          next[candidate] = value;
        }
      }
    }
    return next;
  }

  private getCandidates(value: string): string[] {
    const normalized = this.normalize(value);
    const set = new Set<string>([
      normalized,
      normalized.replace(/\./g, "_"),
      normalized.replace(/_/g, "."),
      normalized.replace(/\s+/g, "")
    ]);
    if (normalized.startsWith("menu.")) {
      set.add(normalized.slice(5));
    }
    if (normalized.startsWith("action.")) {
      set.add(normalized.slice(7));
    }
    return [...set];
  }

  private normalize(value: string): string {
    return value.trim().toLowerCase();
  }
}
