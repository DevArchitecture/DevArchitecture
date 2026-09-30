import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('Group Users relation dialog', () => {
  test('group users button lists users in lookup', async ({ page }) => {
    const app = new AppFixtures(page)
    await app.login.goto()
    await app.login.login('admin@adminmail.com', 'Q1w212*_*')
    await page.waitForFunction(() => Boolean(localStorage.getItem('devarch.token')), null, { timeout: 15000 })

    await page.goto('/group')
    await page.waitForLoadState('networkidle')

    await page.locator('.p-datatable-tbody tr').first().click()
    await page.locator('div.toolbar button:has(.pi.pi-users)').click()

    const multiselect = page.locator('.p-dialog .p-multiselect').first()
    await expect(multiselect).toBeVisible({ timeout: 10000 })
    await multiselect.click()

    await expect(
      page.getByRole('option', { name: /System Admin/ }).first()
    ).toBeVisible({ timeout: 10000 })
  })
})
