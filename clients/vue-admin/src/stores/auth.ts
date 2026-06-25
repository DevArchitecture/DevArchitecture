import { ref } from "vue";

const TOKEN_KEY = "devarch.token";
const CLAIMS_KEY = "devarch.claims";

const token = ref(localStorage.getItem(TOKEN_KEY) ?? "");
const claims = ref<string[]>(JSON.parse(localStorage.getItem(CLAIMS_KEY) ?? "[]"));

export function useAuthStore() {
  const logout = () => {
    token.value = "";
    claims.value = [];
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(CLAIMS_KEY);
  };

  return {
    token,
    claims,
    logout,
  };
}
