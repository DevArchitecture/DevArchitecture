import { normalizeUiLanguage } from "./ui-language-code";

/**
 * Regional indicator flag emoji from BCP 47 locale (e.g. tr-TR → 🇹🇷, en-US → 🇺🇸).
 */
export function flagEmojiForLocale(locale: string): string {
  const trimmed = locale.trim();
  if (!trimmed) {
    return "\uD83C\uDF10";
  }
  const parts = trimmed.split(/[-_]/);
  let region: string | null = null;
  if (parts.length >= 2 && parts[1].length === 2) {
    region = parts[1].toUpperCase();
  } else if (parts[0].length === 2) {
    region = parts[0].toUpperCase();
  } else {
    region = regionFromLanguageCode(parts[0]?.toLowerCase() ?? "");
  }
  if (region === "EN") {
    region = "US";
  }
  if (!region || !/^[A-Z]{2}$/.test(region)) {
    return "\uD83C\uDF10";
  }
  const codePoint = (letter: string) => 0x1f1e6 + letter.charCodeAt(0) - 0x41;
  return String.fromCodePoint(codePoint(region[0]), codePoint(region[1]));
}

function regionFromLanguageCode(lang: string): string | null {
  const map: Record<string, string> = {
    tr: "TR",
    en: "US",
    de: "DE",
    fr: "FR",
    ar: "SA",
    ru: "RU",
    es: "ES",
    it: "IT",
    nl: "NL",
    pl: "PL",
    pt: "PT",
    ja: "JP",
    ko: "KR",
    zh: "CN"
  };
  return map[lang] ?? null;
}

export function labelForLocale(code: string): string {
  const labels: Record<string, string> = {
    "tr-TR": "Türkçe",
    "en-US": "English (US)",
    "en-GB": "English (UK)",
    "de-DE": "Deutsch",
    "fr-FR": "Français"
  };
  return labels[code] ?? code;
}

export type LanguageOption = {
  code: string;
  label: string;
  flag: string;
};

export function toLanguageOptions(codes: string[]): LanguageOption[] {
  return codes.map((code) => {
    const normalized = normalizeUiLanguage(code);
    return {
      code: normalized,
      label: labelForLocale(normalized),
      flag: flagEmojiForLocale(normalized)
    };
  });
}
