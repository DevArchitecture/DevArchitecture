import { Routes } from "@angular/router";
import { LoginPageComponent } from "./pages/login-page.component";
import { DashboardPageComponent } from "./pages/dashboard-page.component";
import { ResourcePageComponent } from "./pages/resource-page.component";
import { authGuard } from "./core/auth.guard";

export const routes: Routes = [
  { path: "login", component: LoginPageComponent },
  { path: "dashboard", component: DashboardPageComponent, canActivate: [authGuard] },
  { path: "user", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "user" } },
  { path: "group", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "group" } },
  { path: "language", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "language" } },
  { path: "translate", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "translate" } },
  { path: "operationclaim", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "operationclaim" } },
  { path: "log", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "log" } },
  { path: "", pathMatch: "full", redirectTo: "dashboard" },
  { path: "**", redirectTo: "dashboard" }
];
