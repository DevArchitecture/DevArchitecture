<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import Button from "primevue/button";
import Toast from "primevue/toast";
import ConfirmDialog from "primevue/confirmdialog";
import { CLIENT_MODULES } from "./config/modules";
import { apiClient, authStore } from "./services/api";
import { toLanguageOptions, type LanguageOption } from "./core/language-display";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "./core/ui-language-code";

const router = useRouter();
const isAuthenticated = computed(() => authStore.hasToken());
const sidebarOpen = ref(true);
const darkMode = ref(localStorage.getItem("devarch.darkMode") === "true");
const languageEntries = ref<LanguageOption[]>(toLanguageOptions(["tr-TR", "en-US"]));
const selectedLang = ref(normalizeUiLanguage(authStore.currentLang.value));

const menuLabel = (key: string, fallback: string) => {
  return authStore.t(`menu.${key}`, authStore.t(fallback, fallback));
};

const loadTopbarLanguages = async () => {
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
      languageEntries.value = toLanguageOptions(codes);
    }
  } catch {
    /* keep defaults */
  }
};

const onLangChange = async () => {
  selectedLang.value = normalizeUiLanguage(selectedLang.value);
  await authStore.loadTranslations(selectedLang.value);
};

onMounted(async () => {
  await loadTopbarLanguages();
  selectedLang.value = normalizeUiLanguage(authStore.currentLang.value);
  if (authStore.hasToken()) {
    await authStore.loadTranslations();
  }
});

const logout = async () => {
  authStore.clearToken();
  await router.push("/login");
};

const toggleDark = () => {
  darkMode.value = !darkMode.value;
  localStorage.setItem("devarch.darkMode", String(darkMode.value));
  document.documentElement.classList.toggle("app-dark", darkMode.value);
};

const isActive = (route: string) => window.location.pathname.startsWith(route);
</script>

<template>
  <div class="layout-shell" :class="{ 'auth-layout': isAuthenticated, 'sidebar-collapsed': isAuthenticated && !sidebarOpen }">
    <Toast />
    <ConfirmDialog />
    <header v-if="isAuthenticated" class="topbar">
      <div class="topbar-left">
        <Button icon="pi pi-bars" severity="secondary" rounded :title="authStore.t('action.menu', 'Menu')" @click="sidebarOpen = !sidebarOpen" />
        <h1>DevArchitecture</h1>
      </div>
      <div class="topbar-right">
        <label class="lang-select-wrap">
          <span class="visually-hidden">{{ authStore.t("action.language", "Language") }}</span>
          <select
            v-model="selectedLang"
            class="lang-select"
            :aria-label="authStore.t('action.language', 'Language')"
            @change="onLangChange"
          >
            <option v-for="o in languageEntries" :key="o.code" :value="o.code">{{ o.flag }} {{ o.label }}</option>
          </select>
        </label>
        <Button
          :icon="darkMode ? 'pi pi-sun' : 'pi pi-moon'"
          severity="secondary"
          rounded
          :title="authStore.t('action.toggleTheme', 'Toggle theme')"
          @click="toggleDark"
        />
        <Button icon="pi pi-sign-out" severity="secondary" :title="authStore.t('action.logout', 'Logout')" @click="logout" />
      </div>
    </header>

    <aside v-if="isAuthenticated" class="side-nav" :class="{ collapsed: !sidebarOpen }">
      <div class="brand">Sakai</div>
      <button
        v-for="item in CLIENT_MODULES"
        :key="item.key"
        class="nav-link"
        :class="{ active: isActive(item.route) }"
        :title="menuLabel(item.key, item.label)"
        @click="router.push(item.route)"
      >
        {{ menuLabel(item.key, item.label) }}
      </button>
    </aside>

    <main class="content-area">
      <RouterView />
    </main>
  </div>
</template>
