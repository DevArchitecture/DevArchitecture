import { defineConfig } from '@playwright/test'

export default defineConfig({
  testDir: './tests',
  testMatch: ['*.spec.ts', '*.e2e.ts'],
  timeout: 60000,
  retries: 0,
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
  ],
  projects: [
    {
      name: 'vue-admin',
      use: { baseURL: 'http://localhost:5174' },
      testMatch: ['vue-admin*.spec.ts', 'vue-admin*.e2e.ts'],
      webServer: {
        command: 'cd ../clients/vue-admin && npx vite --port 5174',
        url: 'http://localhost:5174',
        reuseExistingServer: true,
        timeout: 30000,
      },
    },
    {
      name: 'react-admin',
      use: { baseURL: 'http://localhost:5173' },
      testMatch: ['react-admin*.spec.ts', 'react-admin*.e2e.ts'],
      webServer: {
        command: 'cd ../clients/react-admin && npx vite --port 5173',
        url: 'http://localhost:5173',
        reuseExistingServer: true,
        timeout: 30000,
      },
    },
    {
      name: 'angular-admin',
      use: { baseURL: 'http://localhost:4200' },
      testMatch: ['angular-admin*.spec.ts', 'angular-admin*.e2e.ts'],
      webServer: {
        command: 'cd ../clients/angular-admin && npx ng serve --port 4200',
        url: 'http://localhost:4200',
        reuseExistingServer: true,
        timeout: 60000,
      },
    },
  ],
})
