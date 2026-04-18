import axios from "axios";
import { ref } from "vue";
import { API_BASE_URL, API_VERSION } from "../config/modules";

const TOKEN_KEY = "devarch.token";
const CLAIMS_KEY = "devarch.claims";
const LANG_KEY = "devarch.lang";

export const apiClient = axios.create({
  baseURL: API_BASE_URL
});

apiClient.interceptors.request.use((config) => {
  config.headers["x-dev-arch-version"] = API_VERSION;
  config.headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
  config.headers.Pragma = "no-cache";
  config.headers.Expires = "0";
  const token = localStorage.getItem(TOKEN_KEY);
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error?.response?.status;
    const requestUrl = String(error?.config?.url ?? "").toLowerCase();
    if (status === 401 && !requestUrl.includes("/auth/login")) {
      authStore.clearToken();
      if (typeof window !== "undefined" && !window.location.pathname.startsWith("/login")) {
        window.location.href = "/login";
      }
    }
    return Promise.reject(error);
  }
);

const tokenState = ref(localStorage.getItem(TOKEN_KEY) ?? "");
const claimsState = ref<string[]>([]);
const langState = ref(localStorage.getItem(LANG_KEY) ?? "tr-TR");
const dictionaryState = ref<Record<string, string>>({});

const normalizeKey = (value: string) => value.trim().toLowerCase();
const getCandidates = (value: string): string[] => {
  const normalized = normalizeKey(value);
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
};

const entriesFromFlatStringMap = (map: Record<string, unknown>): any[] => {
  const keys = Object.keys(map);
  if (keys.length === 0) {
    return [];
  }
  const out: any[] = [];
  for (const k of keys) {
    const v = map[k];
    if (typeof v !== "string") {
      return [];
    }
    out.push({ code: k, value: v });
  }
  return out;
};

const parseTranslationList = (payload: unknown): any[] => {
  if (Array.isArray(payload)) return payload;
  if (payload && typeof payload === "object") {
    const container = payload as Record<string, unknown>;
    if (Array.isArray(container.data)) return container.data as any[];
    if (Array.isArray(container.items)) return container.items as any[];
    if (Array.isArray(container.result)) return container.result as any[];
    const nestedData = container.data;
    if (nestedData && typeof nestedData === "object" && !Array.isArray(nestedData)) {
      const fromNested = entriesFromFlatStringMap(nestedData as Record<string, unknown>);
      if (fromNested.length > 0) {
        return fromNested;
      }
    }
    const fromFlat = entriesFromFlatStringMap(container);
    if (fromFlat.length > 0) {
      return fromFlat;
    }
  }
  return [];
};

const parseClaims = (): string[] => {
  const raw = localStorage.getItem(CLAIMS_KEY);
  if (!raw) return [];
  try {
    const parsed = JSON.parse(raw);
    return Array.isArray(parsed) ? parsed : [];
  } catch {
    return [];
  }
};

claimsState.value = parseClaims();

export const authStore = {
  token: tokenState,
  claims: claimsState,
  currentLang: langState,
  dictionary: dictionaryState,
  hasToken(): boolean {
    return !!tokenState.value;
  },
  saveToken(token: string, claims: string[] = [], lang?: string): void {
    tokenState.value = token;
    claimsState.value = claims;
    localStorage.setItem(TOKEN_KEY, token);
    localStorage.setItem(CLAIMS_KEY, JSON.stringify(claims));
    if (lang) {
      langState.value = lang;
      localStorage.setItem(LANG_KEY, lang);
    }
  },
  clearToken(): void {
    tokenState.value = "";
    claimsState.value = [];
    dictionaryState.value = {};
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(CLAIMS_KEY);
  },
  setLang(lang: string): void {
    langState.value = lang;
    localStorage.setItem(LANG_KEY, lang);
  },
  getClaims(): string[] {
    return claimsState.value;
  },
  hasClaim(claim: string): boolean {
    if (!claim) return true;
    return claimsState.value.includes(claim);
  },
  async loadTranslations(lang?: string): Promise<void> {
    const targetLang = lang ?? langState.value ?? "tr-TR";
    this.setLang(targetLang);
    try {
      const { data } = await apiClient.get(`/translates/languages/${targetLang}`, {
        params: { _ts: Date.now() }
      });
      const list = parseTranslationList(data);
      const next: Record<string, string> = {};
      for (const item of list) {
        const code = String(item?.code ?? item?.key ?? item?.name ?? "");
        const value = String(item?.value ?? item?.text ?? item?.translation ?? "");
        if (code && value) {
          for (const candidate of getCandidates(code)) {
            next[candidate] = value;
          }
        }
      }
      dictionaryState.value = next;
    } catch {
      dictionaryState.value = {};
    }
  },
  t(key: string, fallback?: string): string {
    for (const candidate of getCandidates(key)) {
      if (dictionaryState.value[candidate]) {
        return dictionaryState.value[candidate];
      }
    }
    return fallback ?? key;
  }
};

export const crudApi = {
  list(resourcePath: string) {
    return apiClient.get(resourcePath, {
      params: { _ts: Date.now() }
    });
  },
  create(resourcePath: string, payload: unknown) {
    return apiClient.post(resourcePath, payload, {
      responseType: "text"
    });
  },
  update(resourcePath: string, payload: unknown) {
    return apiClient.put(resourcePath, payload, {
      responseType: "text"
    });
  },
  remove(resourcePath: string, id: string) {
    return apiClient.delete(`${resourcePath}/${id}`, {
      responseType: "text"
    });
  }
};
