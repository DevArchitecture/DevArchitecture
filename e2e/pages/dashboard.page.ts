import { Page, Locator } from '@playwright/test'

export class DashboardPage {
  readonly page: Page
  readonly sidebar: Locator
  readonly header: Locator
  readonly menuItems: Locator

  constructor(page: Page) {
    this.page = page
    this.sidebar = page.locator('.layout-sidebar, .p-sidebar, nav').first()
    this.header = page.locator('.layout-header, .p-toolbar, header').first()
    this.menuItems = page.locator('.layout-menu a, .p-menu a, nav a')
  }

  async navigateTo(menuLabel: string) {
    await this.page.locator(`text=${menuLabel}`).first().click()
    await this.page.waitForLoadState('networkidle')
  }

  async getUserMenu(): Promise<Locator> {
    return this.page.locator('.user-menu, .p-menu, .layout-menu')
  }

  async logout() {
    const logoutBtn = this.page.locator('button:has-text("Çıkış"), button:has-text("Logout")').first()
    if (await logoutBtn.isVisible()) {
      await logoutBtn.click()
    }
  }
}
