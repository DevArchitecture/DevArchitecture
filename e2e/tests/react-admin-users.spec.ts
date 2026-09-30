import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('Users list', () => {
  test('login and users list shows rows and total records', async ({ page }) => {
    const app = new AppFixtures(page)
    await app.login.goto()
    await app.login.login('admin@adminmail.com', 'Q1w212*_*')
    await page.waitForFunction(() => Boolean(localStorage.getItem('devarch.token')), null, { timeout: 15000 })

    await page.goto('/user')
    await page.waitForLoadState('networkidle')

    await expect(page.getByText('admin@adminmail.com').first()).toBeVisible({ timeout: 15000 })
    await expect(page.locator('.p-paginator').first()).toContainText(/\d+-\d+ \/ \d+/)
  })
})
