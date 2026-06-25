import { defineConfig } from '@playwright/test'

export default defineConfig({
  testDir: './tests',
  testMatch: ['*.spec.ts', '*.e2e.ts'],
  timeout: 60000,
  retries: 1,
  workers: 1,
  use: {
    headless: true,
    screenshot: 'only-on-failure',
    video: 'retain-on-failure',
    trace: 'retain-on-failure',
  },
  reporter: [
    ['list'],
    ['html', { outputFolder: 'playwright-report' }],
    ['json', { outputFile: 'playwright-results.json' }],
  ],
  projects: [
    {
      name: 'vue-admin',
      use: { baseURL: 'http://localhost:5174' },
      testMatch: ['vue-admin*.spec.ts', 'vue-admin*.e2e.ts'],
    },
    {
      name: 'react-admin',
      use: { baseURL: 'http://localhost:5173' },
      testMatch: ['react-admin*.spec.ts', 'react-admin*.e2e.ts'],
    },
    {
      name: 'angular-admin',
      use: { baseURL: 'http://localhost:4200' },
      testMatch: ['angular-admin*.spec.ts', 'angular-admin*.e2e.ts'],
    },
    {
      name: 'api',
      use: { baseURL: 'https://localhost:5101' },
      testMatch: ['api*.spec.ts', 'api*.e2e.ts'],
    },
  ],
})
