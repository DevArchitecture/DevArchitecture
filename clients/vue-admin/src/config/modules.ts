export type ClientModule = {
  key: string;
  label: string;
  route: string;
  resourcePath?: string;
};

export const API_BASE_URL = "https://localhost:5101/api/v1";
export const API_VERSION = "1.0";

export const CLIENT_MODULES: ClientModule[] = [
  { key: "dashboard", label: "Dashboard", route: "/dashboard" },
  { key: "user", label: "Users", route: "/user", resourcePath: "/users" },
  { key: "group", label: "Groups", route: "/group", resourcePath: "/groups" },
  { key: "language", label: "Languages", route: "/language", resourcePath: "/languages" },
  { key: "translate", label: "Translates", route: "/translate", resourcePath: "/translates" },
  { key: "operationclaim", label: "Operation Claims", route: "/operationclaim", resourcePath: "/operation-claims" },
  { key: "log", label: "Logs", route: "/log", resourcePath: "/logs" }
];
