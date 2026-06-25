import { Page, Locator } from '@playwright/test'

export class LoginPage {
  readonly page: Page
  readonly usernameInput: Locator
  readonly passwordInput: Locator
  readonly submitButton: Locator
  readonly errorMessage: Locator

  constructor(page: Page) {
    this.page = page
    this.usernameInput = page.locator('input[type="text"]').first()
    this.passwordInput = page.locator('input[type="password"]').first()
    this.submitButton = page.locator('button[type="submit"], button:has-text("Giriş"), button:has-text("Login")').first()
    this.errorMessage = page.locator('.p-message, .error, [role="alert"]').first()
  }

  async goto() {
    await this.page.goto('/')
    await this.page.waitForLoadState('networkidle')
  }

  async login(username: string, password: string) {
    await this.usernameInput.fill(username)
    await this.passwordInput.fill(password)
    await this.submitButton.click()
    await this.page.waitForLoadState('networkidle')
  }

  async isLoginFormVisible(): Promise<boolean> {
    return this.usernameInput.isVisible()
  }
}
