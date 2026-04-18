import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { CardModule } from "primeng/card";
import { InputTextModule } from "primeng/inputtext";
import { PasswordModule } from "primeng/password";
import { ButtonModule } from "primeng/button";
import { AuthService } from "../core/auth.service";
import { ApiService } from "../core/api.service";
import { TranslationService } from "../core/translation.service";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "../core/ui-language-code";
import { toLanguageOptions, type LanguageOption } from "../core/language-display";

@Component({
  selector: "app-login-page",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, CardModule, InputTextModule, PasswordModule, ButtonModule],
  template: `
    <div class="centered-page">
      <p-card [header]="translate('login.title', 'DevArchitecture Login')" [subheader]="translate('login.subtitle', 'PrimeNG admin client')">
        <form [formGroup]="form" (ngSubmit)="submit()" class="login-form">
          <input pInputText type="email" [placeholder]="translate('login.email', 'Email')" formControlName="email" />
          <p-password [feedback]="false" [placeholder]="translate('login.password', 'Password')" formControlName="password"></p-password>
          <div class="lang-field">
            <label class="lang-field-label" for="login-lang-select">{{ translate("login.language", "Language") }}</label>
            <select
              id="login-lang-select"
              class="lang-select"
              formControlName="lang"
              [attr.aria-label]="translate('login.language', 'Language')"
            >
              <option *ngFor="let o of languageEntries" [value]="o.code">{{ o.flag }} {{ o.label }}</option>
            </select>
          </div>
          <button pButton type="submit" icon="pi pi-sign-in" [attr.title]="translate('action.login', 'Login')"></button>
          <small *ngIf="loginError" style="color:#c62828">{{ loginError }}</small>
        </form>
      </p-card>
    </div>
  `,
  styleUrl: "./login-page.component.scss"
})
export class LoginPageComponent {
  readonly form;
  languageEntries: LanguageOption[] = toLanguageOptions(["tr-TR"]);
  loginError = "";

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router,
    private readonly apiService: ApiService,
    private readonly translationService: TranslationService
  ) {
    this.form = this.formBuilder.group({
      email: ["admin@adminmail.com", [Validators.required, Validators.email]],
      password: ["Q1w212*_*", [Validators.required]],
      lang: ["tr-TR", [Validators.required]]
    });
    this.apiService.getByPath("/languages/codes").subscribe({
      next: (result) => {
        const list = Array.isArray(result) ? result : [];
        const codes = list
          .map((item: Record<string, unknown>) => {
            const raw = cultureCodeFromLanguageLookupRow(item);
            return raw ? normalizeUiLanguage(raw) : null;
          })
          .filter((c): c is string => c != null && c !== "");
        const distinct = [...new Set(codes)];
        const finalCodes = distinct.length > 0 ? distinct : ["tr-TR"];
        this.languageEntries = toLanguageOptions(finalCodes);
        if (distinct.length > 0) {
          this.form.patchValue({ lang: distinct[0] });
        }
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loginError = "";
    this.authService.login(this.form.getRawValue() as { email: string; password: string; lang: string }).subscribe({
      next: () => {
        const lang = normalizeUiLanguage(this.form.getRawValue().lang ?? "tr-TR");
        this.translationService.load(lang).subscribe({
          next: () => this.router.navigateByUrl("/dashboard"),
          error: () => this.router.navigateByUrl("/dashboard")
        });
      },
      error: () => {
        this.loginError = this.translate("login.failed", "Login failed. Check email/password.");
      }
    });
  }

  translate(key: string, fallback: string): string {
    return this.translationService.t(key, fallback);
  }
}
