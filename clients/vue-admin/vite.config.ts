import path from "node:path";
import { fileURLToPath } from "node:url";
import { createRequire } from "node:module";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const require = createRequire(import.meta.url);

/**
 * npm workspaces hoist dependencies to the repo root; Vite must resolve `primeicons`
 * to the real package folder or icon fonts fail to load in dev (empty buttons).
 */
function resolvePrimeIconsPackageRoot(): string {
  try {
    return path.dirname(require.resolve("primeicons/package.json"));
  } catch {
    return path.resolve(__dirname, "../../node_modules/primeicons");
  }
}

// https://vite.dev/config/
export default defineConfig({
  // Explicit root avoids wrong cwd when the dev command is started from the monorepo root.
  root: __dirname,
  plugins: [vue()],
  appType: "spa",
  resolve: {
    alias: {
      primeicons: resolvePrimeIconsPackageRoot()
    }
  },
  server: {
    // Match common local URL; if busy, Vite picks the next free port (strictPort: false).
    port: 5174,
    strictPort: false,
    host: true,
    fs: {
      // npm workspaces: allow reading hoisted packages under the repo root.
      allow: [path.resolve(__dirname, "../.."), path.resolve(__dirname)]
    }
  }
});
