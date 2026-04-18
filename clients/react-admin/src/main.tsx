import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { PrimeReactProvider } from "primereact/api";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import "./index.css";
import App from "./App.tsx";
import { applyPrimeReactThemeLink } from "./theme/primereact-theme";

const dark = localStorage.getItem("devarch.darkMode") === "true";
document.documentElement.classList.toggle("app-dark", dark);
applyPrimeReactThemeLink(dark);

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <PrimeReactProvider value={{ ripple: true }}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </PrimeReactProvider>
  </StrictMode>
);
