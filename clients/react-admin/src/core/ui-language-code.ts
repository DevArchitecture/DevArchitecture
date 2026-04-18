/**
 * UI and API use culture codes (e.g. tr-TR). /languages/codes returns SelectionItem: id = Language.Code, label = Language.Name.
 * Legacy localStorage may contain display names like "Türkçe".
 */

const cultureLike = /^[a-zA-Z]{2,3}(-[a-zA-Z0-9]{2,12})+$/;

function canonicalizeCultureCode(t: string): string {
  const segments = t.split(/[-_]/);
  if (segments.length >= 2 && segments[0].toLowerCase() === "en" && segments[1].toLowerCase() === "en") {
    return "en-US";
  }
  return t;
}

export function normalizeUiLanguage(raw: string | null | undefined): string {
  if (raw == null || String(raw).trim() === "") {
    return "tr-TR";
  }
  const t = String(raw).trim();
  if (cultureLike.test(t)) {
    return canonicalizeCultureCode(t);
  }
  switch (t) {
    case "Türkçe":
      return "tr-TR";
    case "Turkish":
      return "tr-TR";
    case "English":
      return "en-US";
    case "İngilizce":
    case "Ingilizce":
      return "en-US";
    default:
      return "tr-TR";
  }
}

/** Map API language lookup row to culture code; never use label (display name). */
export function cultureCodeFromLanguageLookupRow(item: Record<string, unknown>): string | null {
  const tryKeys = ["code", "Code", "id", "Id"];
  for (const key of tryKeys) {
    const v = item[key];
    if (v == null) {
      continue;
    }
    const s = String(v).trim();
    if (s !== "") {
      return s;
    }
  }
  return null;
}
