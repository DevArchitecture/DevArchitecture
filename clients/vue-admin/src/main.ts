import { createApp } from "vue";
import PrimeVue from "primevue/config";
import Aura from "@primeuix/themes/aura";
import ToastService from "primevue/toastservice";
import ConfirmationService from "primevue/confirmationservice";
import App from "./App.vue";
import router from "./router";
import "primeicons/primeicons.css";
import "./style.css";

if (localStorage.getItem("devarch.darkMode") === "true") {
  document.documentElement.classList.add("app-dark");
}

createApp(App)
  .use(router)
  .use(ToastService)
  .use(ConfirmationService)
  .use(PrimeVue, {
    ripple: true,
    theme: {
      preset: Aura,
      options: {
        darkModeSelector: ".app-dark"
      }
    }
  })
  .mount("#app");
