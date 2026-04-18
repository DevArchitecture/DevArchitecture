import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { apiClient, authStore } from "../api/client";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "../core/ui-language-code";
import { toLanguageOptions, type LanguageOption } from "../core/language-display";

export function LoginPage() {
  const t = (key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback));
  const navigate = useNavigate();
  const [email, setEmail] = useState("admin@adminmail.com");
  const [password, setPassword] = useState("Q1w212*_*");
  const [lang, setLang] = useState("tr-TR");
  const [languageEntries, setLanguageEntries] = useState<LanguageOption[]>(() => toLanguageOptions(["tr-TR"]));
  const [loginError, setLoginError] = useState("");

  useEffect(() => {
    const load = async () => {
      try {
        const { data } = await apiClient.get("/languages/codes");
        if (Array.isArray(data)) {
          const codes = data
            .map((item: Record<string, unknown>) => {
              const raw = cultureCodeFromLanguageLookupRow(item);
              return raw ? normalizeUiLanguage(raw) : null;
            })
            .filter((c): c is string => c != null && c !== "");
          const distinct = [...new Set(codes)];
          const finalCodes = distinct.length > 0 ? distinct : ["tr-TR"];
          setLanguageEntries(toLanguageOptions(finalCodes));
          if (distinct.length > 0) {
            setLang(distinct[0]);
          }
        }
      } catch {
        setLanguageEntries(toLanguageOptions(["tr-TR"]));
      }
    };
    void load();
  }, []);

  const onSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setLoginError("");
    const culture = normalizeUiLanguage(lang);
    try {
      const { data } = await apiClient.post("/Auth/login", { email, password, lang: culture });
      const token = data?.data?.token ?? data?.token;
      const claims = data?.data?.claims ?? data?.claims ?? [];
      if (token) {
        authStore.saveToken(token, claims, culture);
        await authStore.loadTranslations(culture);
        navigate("/dashboard");
        return;
      }
      setLoginError(t("login.failed", "Login failed. Check email/password."));
    } catch {
      setLoginError(t("login.failed", "Login failed. Check email/password."));
    }
  };

  return (
    <div className="centered-page">
      <Card title={t("login.title", "DevArchitecture Login")} subTitle={t("login.subtitle", "PrimeReact admin client")}>
        <form className="login-form" onSubmit={onSubmit}>
          <InputText value={email} onChange={(e) => setEmail(e.target.value)} placeholder={t("login.email", "Email")} />
          <Password value={password} onChange={(e) => setPassword(e.target.value)} toggleMask feedback={false} />
          <div className="lang-field">
            <label className="lang-field-label" htmlFor="login-lang-select">
              {t("login.language", "Language")}
            </label>
            <select
              id="login-lang-select"
              className="lang-select lang-select--login"
              value={lang}
              onChange={(event) => setLang(event.target.value)}
              aria-label={t("login.language", "Language")}
            >
              {languageEntries.map((o) => (
                <option key={o.code} value={o.code}>
                  {o.flag} {o.label}
                </option>
              ))}
            </select>
          </div>
          <Button icon="pi pi-sign-in" type="submit" tooltip={authStore.t("action.login", "Login")} />
          {loginError && <small style={{ color: "#c62828" }}>{loginError}</small>}
        </form>
      </Card>
    </div>
  );
}
