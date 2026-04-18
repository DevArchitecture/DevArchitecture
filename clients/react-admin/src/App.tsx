import { useEffect, useState } from "react";
import { Navigate, Route, Routes, useNavigate } from "react-router-dom";
import { Button } from "primereact/button";
import { apiClient, authStore } from "./api/client";
import { CLIENT_MODULES } from "./config/modules";
import { LoginPage } from "./pages/LoginPage";
import { DashboardPage } from "./pages/DashboardPage";
import { ResourcePage } from "./pages/ResourcePage";
import { toLanguageOptions, type LanguageOption } from "./core/language-display";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "./core/ui-language-code";
import { applyPrimeReactThemeLink } from "./theme/primereact-theme";
import "./App.css";

const DARK_MODE_KEY = "devarch.darkMode";

function ProtectedRoutes() {
  if (!authStore.hasToken()) {
    return <Navigate to="/login" replace />;
  }

  return (
    <Routes>
      <Route path="/dashboard" element={<DashboardPage />} />
      <Route path="/:moduleKey" element={<ResourcePage />} />
      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  );
}

function AppShell() {
  const navigate = useNavigate();
  const [version, setVersion] = useState(0);
  const [sidebarOpen, setSidebarOpen] = useState(true);
  const [darkMode, setDarkMode] = useState(() => localStorage.getItem(DARK_MODE_KEY) === "true");
  const [languageEntries, setLanguageEntries] = useState<LanguageOption[]>(() => toLanguageOptions(["tr-TR", "en-US"]));
  const [selectedLang, setSelectedLang] = useState(() => normalizeUiLanguage(authStore.getLang()));

  useEffect(() => {
    const unsubscribe = authStore.subscribe(() => setVersion((prev) => prev + 1));
    if (authStore.hasToken()) {
      void authStore.loadTranslations();
    }
    return unsubscribe;
  }, []);

  useEffect(() => {
    setSelectedLang(normalizeUiLanguage(authStore.getLang()));
  }, [version]);

  useEffect(() => {
    const loadTopbar = async () => {
      try {
        const { data } = await apiClient.get("/languages/codes");
        if (!Array.isArray(data) || data.length === 0) {
          return;
        }
        const codes = [
          ...new Set(
            data
              .map((item: Record<string, unknown>) => {
                const raw = cultureCodeFromLanguageLookupRow(item);
                return raw ? normalizeUiLanguage(raw) : null;
              })
              .filter((c): c is string => c != null && c !== "")
          )
        ];
        if (codes.length > 0) {
          setLanguageEntries(toLanguageOptions(codes));
        }
      } catch {
        /* keep defaults */
      }
    };
    void loadTopbar();
  }, []);

  const t = (key: string, fallback: string) => authStore.t(key, authStore.t(fallback, fallback));
  const isActive = (route: string) => window.location.pathname.startsWith(route);

  const toggleDark = () => {
    setDarkMode((prev) => {
      const next = !prev;
      localStorage.setItem(DARK_MODE_KEY, String(next));
      document.documentElement.classList.toggle("app-dark", next);
      applyPrimeReactThemeLink(next);
      return next;
    });
  };

  return (
    <div className={`layout-shell ${authStore.hasToken() ? "auth-layout" : ""} ${sidebarOpen ? "" : "sidebar-collapsed"}`}>
      {authStore.hasToken() && (
        <>
          <header className="topbar">
            <div className="topbar-left">
              <Button icon="pi pi-bars" severity="secondary" rounded tooltip={t("action.menu", "Menu")} onClick={() => setSidebarOpen((prev) => !prev)} />
              <h1>DevArchitecture</h1>
            </div>
            <div className="topbar-right">
              <label className="lang-select-wrap">
                <span className="visually-hidden">{t("action.language", "Language")}</span>
                <select
                  className="lang-select"
                  value={selectedLang}
                  aria-label={t("action.language", "Language")}
                  onChange={(event) => {
                    const lang = normalizeUiLanguage(event.target.value);
                    setSelectedLang(lang);
                    void authStore.loadTranslations(lang);
                  }}
                >
                  {languageEntries.map((o) => (
                    <option key={o.code} value={o.code}>
                      {o.flag} {o.label}
                    </option>
                  ))}
                </select>
              </label>
              <Button
                icon={darkMode ? "pi pi-sun" : "pi pi-moon"}
                rounded
                severity="secondary"
                tooltip={t("action.toggleTheme", "Toggle theme")}
                onClick={toggleDark}
              />
              <Button
                icon="pi pi-sign-out"
                rounded
                severity="secondary"
                tooltip={t("action.logout", "Logout")}
                onClick={() => {
                  authStore.clearToken();
                  navigate("/login");
                }}
              />
            </div>
          </header>

          <aside className={`side-nav ${sidebarOpen ? "" : "collapsed"}`}>
            <div className="brand">Sakai</div>
            {CLIENT_MODULES.map((item) => (
              <button
                key={item.key}
                onClick={() => navigate(item.route)}
                className={`nav-link ${isActive(item.route) ? "active" : ""}`}
                title={t(`menu.${item.key}`, item.label)}
              >
                {t(`menu.${item.key}`, item.label)}
              </button>
            ))}
          </aside>
        </>
      )}
      <main className="content-area">
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/*" element={<ProtectedRoutes />} />
        </Routes>
      </main>
    </div>
  );
}

export default AppShell;
