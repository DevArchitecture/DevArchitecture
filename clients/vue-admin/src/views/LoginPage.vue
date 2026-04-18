<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import Card from "primevue/card";
import InputText from "primevue/inputtext";
import Password from "primevue/password";
import Button from "primevue/button";
import { apiClient, authStore } from "../services/api";
import { cultureCodeFromLanguageLookupRow, normalizeUiLanguage } from "../core/ui-language-code";
import { toLanguageOptions, type LanguageOption } from "../core/language-display";

const router = useRouter();
const email = ref("admin@adminmail.com");
const password = ref("Q1w212*_*");
const lang = ref("tr-TR");
const languageEntries = ref<LanguageOption[]>(toLanguageOptions(["tr-TR"]));
const loginError = ref("");

const loadLanguages = async () => {
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
      languageEntries.value = toLanguageOptions(finalCodes);
      if (distinct.length > 0) {
        lang.value = distinct[0];
      }
    }
  } catch {
    languageEntries.value = toLanguageOptions(["tr-TR"]);
  }
};

void loadLanguages();

const submit = async () => {
  loginError.value = "";
  const culture = normalizeUiLanguage(lang.value);
  try {
    const { data } = await apiClient.post("/Auth/login", {
      email: email.value,
      password: password.value,
      lang: culture
    });
    const token = data?.data?.token ?? data?.token;
    const claims = data?.data?.claims ?? data?.claims ?? [];
    if (token) {
      authStore.saveToken(token, claims, culture);
      await authStore.loadTranslations(culture);
      await router.push("/dashboard");
      return;
    }
    loginError.value = authStore.t("login.failed", "Login failed. Check email/password.");
  } catch {
    loginError.value = authStore.t("login.failed", "Login failed. Check email/password.");
  }
};
</script>

<template>
  <div class="centered-page">
    <Card>
      <template #title>{{ authStore.t("login.title", "DevArchitecture Login") }}</template>
      <template #subtitle>{{ authStore.t("login.subtitle", "PrimeVue admin client") }}</template>
      <template #content>
        <form class="login-form" @submit.prevent="submit">
          <InputText v-model="email" :placeholder="authStore.t('login.email', 'Email')" />
          <Password v-model="password" :feedback="false" toggleMask />
          <div class="lang-field">
            <label class="lang-field-label" for="login-lang-select">{{ authStore.t("login.language", "Language") }}</label>
            <select
              id="login-lang-select"
              v-model="lang"
              class="lang-select lang-select--login"
              :aria-label="authStore.t('login.language', 'Language')"
            >
              <option v-for="o in languageEntries" :key="o.code" :value="o.code">{{ o.flag }} {{ o.label }}</option>
            </select>
          </div>
          <Button icon="pi pi-sign-in" :title="authStore.t('action.login', 'Login')" type="submit" />
          <small v-if="loginError" style="color:#c62828">{{ loginError }}</small>
        </form>
      </template>
    </Card>
  </div>
</template>
