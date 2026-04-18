export type ApiInventoryModule = {
  name: string;
  angularRoute: string;
  endpoints: string[];
};

export const apiInventory: ApiInventoryModule[] = [
  {
    name: "Auth",
    angularRoute: "/login",
    endpoints: [
      "POST /api/v1/Auth/login",
      "POST /api/v1/Auth/refresh-token",
      "POST /api/v1/Auth/register",
      "PUT /api/v1/Auth/forgot-password",
      "PUT /api/v1/Auth/user-password"
    ]
  },
  {
    name: "Users",
    angularRoute: "/user",
    endpoints: [
      "GET /api/v1/users",
      "GET /api/v1/users/lookups",
      "GET /api/v1/users/{id}",
      "POST /api/v1/users",
      "PUT /api/v1/users",
      "DELETE /api/v1/users/{id}",
      "GET /api/v1/user-claims/users/{id}",
      "PUT /api/v1/user-claims",
      "GET /api/v1/user-groups/users/{id}/groups",
      "PUT /api/v1/user-groups"
    ]
  },
  {
    name: "Groups",
    angularRoute: "/group",
    endpoints: [
      "GET /api/v1/groups",
      "GET /api/v1/groups/lookups",
      "GET /api/v1/groups/{id}",
      "POST /api/v1/groups",
      "PUT /api/v1/groups",
      "DELETE /api/v1/groups/{id}",
      "GET /api/v1/group-claims/groups/{id}",
      "PUT /api/v1/group-claims",
      "GET /api/v1/user-groups/groups/{id}/users",
      "PUT /api/v1/user-groups/groups"
    ]
  },
  {
    name: "Languages",
    angularRoute: "/language",
    endpoints: [
      "GET /api/v1/languages",
      "GET /api/v1/languages/codes",
      "GET /api/v1/languages/lookups",
      "GET /api/v1/languages/{id}",
      "POST /api/v1/languages",
      "PUT /api/v1/languages",
      "DELETE /api/v1/languages/{id}"
    ]
  },
  {
    name: "Translates",
    angularRoute: "/translate",
    endpoints: [
      "GET /api/v1/translates",
      "GET /api/v1/translates/languages/{lang}",
      "GET /api/v1/translates/dtos",
      "GET /api/v1/translates/{id}",
      "POST /api/v1/translates",
      "PUT /api/v1/translates",
      "DELETE /api/v1/translates/{id}"
    ]
  },
  {
    name: "OperationClaims",
    angularRoute: "/operationclaim",
    endpoints: [
      "GET /api/v1/operation-claims",
      "GET /api/v1/operation-claims/lookups",
      "GET /api/v1/operation-claims/{id}",
      "PUT /api/v1/operation-claims",
      "GET /api/v1/operation-claims/cache"
    ]
  },
  {
    name: "Logs",
    angularRoute: "/log",
    endpoints: [
      "GET /api/v1/logs"
    ]
  }
];
