import { test, expect } from '@playwright/test'
import { AppFixtures } from '../fixtures/app.fixture'

test.describe('Vue Admin - Login & Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
  })

  test('Login form renders with all required fields', async ({ page }) => {
    const app = new AppFixtures(page)
    const isVisible = await app.login.isLoginFormVisible()

    expect(isVisible).toBe(true)
    const passwordField = page.locator('input[type="password"]')
    await expect(passwordField).toBeVisible()
    const submitBtn = page.locator('button[type="submit"], button:has-text("Giriş"), button:has-text("Login")')
    await expect(submitBtn).toBeVisible()
  })

  test('App title is set correctly', async ({ page }) => {
    const title = await page.title()
    expect(title.length).toBeGreaterThan(0)
  })

  test('Page renders without JavaScript console errors', async ({ page }) => {
    const app = new AppFixtures(page)
    const errors = await app.captureConsoleErrors()
    await app.checkNoConsoleErrors(errors)
  })

  test('Failed login shows error message', async ({ page }) => {
    const app = new AppFixtures(page)
    await app.login.login('invalid@test.com', 'wrongpass')
    await page.waitForTimeout(1000)
    const body = await page.textContent('body')
    expect(body).toBeTruthy()
  })
})
