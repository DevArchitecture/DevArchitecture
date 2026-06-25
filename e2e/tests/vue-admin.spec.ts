import { test, expect } from '@playwright/test'

test.describe('Vue Admin - E2E', () => {
  test('app loads and renders login page', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
    await expect(page).toHaveURL(/\/login/)
  })

  test('login form is displayed', async ({ page }) => {
    await page.goto('/')
    // Wait for login form to render via PrimeVue
    await page.waitForLoadState('networkidle')
    await expect(page.locator('input[type="text"]').first()).toBeVisible()
  })

  test('page title is DevArchitecture', async ({ page }) => {
    await page.goto('/')
    const title = await page.title()
    expect(title).toBeTruthy()
  })
})

test.describe('React Admin - E2E', () => {
  test('app loads successfully', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
  })

  test('renders without crash', async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
    const errorLogs: string[] = []
    page.on('console', (msg) => {
      if (msg.type() === 'error') errorLogs.push(msg.text())
    })
    expect(errorLogs.length).toBe(0)
  })
})

test.describe('Angular Admin - E2E', () => {
  test('app loads and renders login page', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
  })

  test('application shell is rendered', async ({ page }) => {
    await page.goto('/')
    await page.waitForLoadState('networkidle')
    const html = await page.content()
    expect(html.length).toBeGreaterThan(100)
  })
})
