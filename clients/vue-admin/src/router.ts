import { createRouter, createWebHistory } from "vue-router";
import { authStore } from "./services/api";
import LoginPage from "./views/LoginPage.vue";
import DashboardPage from "./views/DashboardPage.vue";
import ResourcePage from "./views/ResourcePage.vue";
import ShowcasePage from "./views/ShowcasePage.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/login", component: LoginPage },
    { path: "/dashboard", component: DashboardPage, meta: { requiresAuth: true } },
    { path: "/showcase", component: ShowcasePage, meta: { requiresAuth: true } },
    { path: "/:moduleKey(user|group|language|translate|operationclaim|log)", component: ResourcePage, meta: { requiresAuth: true } },
    { path: "/", redirect: "/dashboard" }
  ]
});

router.beforeEach((to) => {
  if (to.meta.requiresAuth && !authStore.hasToken()) {
    return "/login";
  }
  return true;
});

export default router;
