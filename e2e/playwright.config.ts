import { defineConfig } from '@playwright/test'

export default defineConfig({
  testDir: './tests',
  timeout: 30000,
  retries: 1,
  use: {
    headless: true,
  },
  projects: [
    { name: 'vue-admin', use: { baseURL: 'http://localhost:5174' } },
    { name: 'react-admin', use: { baseURL: 'http://localhost:5173' } },
    { name: 'angular-admin', use: { baseURL: 'http://localhost:4200' } },
  ],
})
