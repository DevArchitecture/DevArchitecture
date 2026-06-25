import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('React Admin - Login & Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
  })

  test('App renders without crash', async ({ page }) => {
    await expect(page.locator('body')).toBeVisible()
  })

  test('Login form is accessible', async ({ page }) => {
    const app = new AppFixtures(page)
    const isVisible = await app.login.isLoginFormVisible()
    expect(isVisible).toBe(true)
  })

  test('No console errors on initial load', async ({ page }) => {
    const app = new AppFixtures(page)
    const errors = await app.captureConsoleErrors()
    await app.checkNoConsoleErrors(errors)
  })
})
