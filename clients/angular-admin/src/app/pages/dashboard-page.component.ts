import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CardModule } from "primeng/card";

@Component({
  selector: "app-dashboard-page",
  standalone: true,
  imports: [CommonModule, CardModule],
  template: `
    <p-card header="Dashboard">
      <p>
        This starter keeps the same backend contract and route model as the existing Angular admin.
        Teams can extend each feature module independently.
      </p>
    </p-card>
  `
})
export class DashboardPageComponent {}
