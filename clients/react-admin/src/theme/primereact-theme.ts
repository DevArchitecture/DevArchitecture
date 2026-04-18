import laraDarkUrl from "primereact/resources/themes/lara-dark-green/theme.css?url";
import laraLightUrl from "primereact/resources/themes/lara-light-green/theme.css?url";

const LINK_ID = "primereact-theme-stylesheet";

/**
 * Swaps Lara theme CSS (light/dark) to stay close to Sakai / Aura emerald palette.
 */
export function applyPrimeReactThemeLink(useDark: boolean): void {
  let link = document.getElementById(LINK_ID) as HTMLLinkElement | null;
  if (!link) {
    link = document.createElement("link");
    link.id = LINK_ID;
    link.rel = "stylesheet";
    document.head.appendChild(link);
  }
  link.href = useDark ? laraDarkUrl : laraLightUrl;
}
