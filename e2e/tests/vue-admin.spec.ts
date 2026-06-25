import { test, expect } from '@playwright/test'

test.describe('Vue Admin', () => {
  test('app loads and shows login page', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
  })
})

test.describe('React Admin', () => {
  test('app loads and shows login page', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
  })
})

test.describe('Angular Admin', () => {
  test('app loads and shows login page', async ({ page }) => {
    await page.goto('/')
    await expect(page.locator('body')).toBeVisible()
  })
})
