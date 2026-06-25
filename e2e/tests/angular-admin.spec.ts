import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('Angular Admin - Login & Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
  })

  test('App renders successfully', async ({ page }) => {
    await expect(page.locator('body')).toBeVisible()
  })

  test('Application shell renders with content', async ({ page }) => {
    const html = await page.content()
    expect(html.length).toBeGreaterThan(100)
  })

  test('No console errors', async ({ page }) => {
    const app = new AppFixtures(page)
    const errors = await app.captureConsoleErrors()
    await app.checkNoConsoleErrors(errors)
  })
})
