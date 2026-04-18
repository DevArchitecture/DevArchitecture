import { Injectable, signal } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map, Observable, tap } from "rxjs";
import { environment } from "../../environments/environment";
import { normalizeUiLanguage } from "./ui-language-code";

type LoginRequest = {
  email: string;
  password: string;
  lang?: string;
};

type LoginResponse = {
  token?: string;
  refreshToken?: string;
  claims?: string[];
  success?: boolean;
  message?: string;
  data?: {
    token: string;
    refreshToken?: string;
    claims?: string[];
  };
};

@Injectable({ providedIn: "root" })
export class AuthService {
  private readonly tokenKey = "devarch.token";
  private readonly claimsKey = "devarch.claims";
  private readonly langKey = "devarch.lang";
  readonly isAuthenticated = signal<boolean>(this.hasToken());

  constructor(private readonly http: HttpClient) {}

  login(payload: LoginRequest): Observable<void> {
    return this.http
      .post<LoginResponse>(`${environment.apiBaseUrl}/Auth/login`, payload, {
        headers: { "x-dev-arch-version": environment.apiVersion }
      })
      .pipe(
        tap((result) => {
          const token = result.data?.token ?? result.token ?? "";
          const claims = result.data?.claims ?? result.claims ?? [];
          if (token) {
            if (payload.lang) {
              localStorage.setItem(this.langKey, payload.lang);
            }
            this.setToken(token, claims);
          }
        }),
        map(() => void 0)
      );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.claimsKey);
    this.isAuthenticated.set(false);
  }
  hasClaim(claim: string): boolean {
    if (!claim) {
      return true;
    }
    const claims = this.getClaims();
    return claims.includes(claim);
  }

  getClaims(): string[] {
    const raw = localStorage.getItem(this.claimsKey);
    if (!raw) return [];
    try {
      const parsed = JSON.parse(raw);
      return Array.isArray(parsed) ? parsed : [];
    } catch {
      return [];
    }
  }


  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getLang(): string {
    const raw = localStorage.getItem(this.langKey);
    const lang = normalizeUiLanguage(raw);
    if (raw != null && raw.trim() !== lang) {
      localStorage.setItem(this.langKey, lang);
    }
    return lang;
  }

  private setToken(token: string, claims: string[]): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.claimsKey, JSON.stringify(claims ?? []));
    this.isAuthenticated.set(true);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }
}
