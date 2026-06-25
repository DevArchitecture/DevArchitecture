/**
 * E2E Test Template
 *
 * Kullanım:
 * 1. Bu dosyayı kopyalayın: cp template.e2e.ts tests/my-feature.e2e.ts
 * 2. Test adını ve URL'i güncelleyin
 * 3. Test senaryolarını ekleyin
 *
 * Playwright'ı çalıştırma:
 *   cd e2e && npx playwright test --project=vue-admin    # Sadece Vue
 *   cd e2e && npx playwright test --project=react-admin  # Sadece React
 *   cd e2e && npx playwright test                         # Tümü
 */

import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('Template Feature - E2E', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
  })

  test('Feature requirements - test case 1', async ({ page }) => {
    const app = new AppFixtures(page)

    // Arrange: sayfa yüklendi
    await expect(page.locator('body')).toBeVisible()

    // Act: kullanıcı işlemi
    await page.locator('button').first().click()
    await page.waitForLoadState('networkidle')

    // Assert: beklenen sonuç
    await expect(page.locator('.result')).toBeVisible()
  })

  test('Handles error states gracefully', async ({ page }) => {
    const app = new AppFixtures(page)
    const errors = await app.captureConsoleErrors()

    // Do something that might error
    await page.goto('/invalid-route')
    await page.waitForLoadState('networkidle')

    // Should show error page, not crash
    await expect(page.locator('body')).toBeVisible()
    await app.checkNoConsoleErrors(errors)
  })

  test('API response handling', async ({ page }) => {
    // API mock örneği:
    await page.route('**/api/v1/**', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify({ data: [] })
      })
    })

    await page.goto('/')
    await page.waitForLoadState('networkidle')
    await expect(page.locator('body')).toBeVisible()
  })
})
