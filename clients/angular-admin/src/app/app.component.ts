import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Router, RouterLink, RouterOutlet } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { ToastModule } from "primeng/toast";
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { AuthService } from "./core/auth.service";
import { ApiService, CLIENT_MODULES } from "./core/api.service";
import { TranslationService } from "./core/translation.service";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "./core/ui-language-code";
import { toLanguageOptions, type LanguageOption } from "./core/language-display";

@Component({
  selector: "app-root",
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink, ButtonModule, ToastModule, ConfirmDialogModule],
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.scss"
})
export class AppComponent implements OnInit {
  readonly modules = CLIENT_MODULES;
  languageEntries: LanguageOption[] = toLanguageOptions(["tr-TR", "en-US"]);
  sidebarOpen = true;
  selectedLang = "tr-TR";
  darkMode = false;

  constructor(
    readonly authService: AuthService,
    readonly translationService: TranslationService,
    private readonly apiService: ApiService,
    private readonly router: Router
  ) {
    this.selectedLang = normalizeUiLanguage(this.authService.getLang());
    this.apiService.getByPath("/languages/codes").subscribe({
      next: (result) => {
        const list = Array.isArray(result) ? result : [];
        const codes = [
          ...new Set(
            list
              .map((item) => {
                const raw = cultureCodeFromLanguageLookupRow(item as Record<string, unknown>);
                return raw ? normalizeUiLanguage(raw) : null;
              })
              .filter((c): c is string => c != null && c !== "")
          )
        ];
        if (codes.length > 0) {
          this.languageEntries = toLanguageOptions(codes);
        }
      },
      error: () => {
        /* keep defaults */
      }
    });
    if (this.authService.isAuthenticated()) {
      this.translationService.load(this.authService.getLang()).subscribe();
    }
  }

  ngOnInit(): void {
    this.darkMode = localStorage.getItem("devarch.darkMode") === "true";
    document.documentElement.classList.toggle("app-dark", this.darkMode);
  }

  toggleDark(): void {
    this.darkMode = !this.darkMode;
    localStorage.setItem("devarch.darkMode", String(this.darkMode));
    document.documentElement.classList.toggle("app-dark", this.darkMode);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigateByUrl("/login");
  }

  translate(key: string, fallback: string): string {
    return this.translationService.t(key, fallback);
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  isActive(route: string): boolean {
    return this.router.url.startsWith(route);
  }

  navigate(route: string): void {
    this.router.navigateByUrl(route);
  }

  onLangChange(): void {
    this.selectedLang = normalizeUiLanguage(this.selectedLang);
    this.translationService.load(this.selectedLang).subscribe();
  }
}
