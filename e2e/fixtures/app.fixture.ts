import { Page } from '@playwright/test'
import { LoginPage } from '../pages/login.page'
import { DashboardPage } from '../pages/dashboard.page'

export class AppFixtures {
  readonly page: Page
  readonly login: LoginPage
  readonly dashboard: DashboardPage

  constructor(page: Page) {
    this.page = page
    this.login = new LoginPage(page)
    this.dashboard = new DashboardPage(page)
  }

  async captureConsoleErrors(): Promise<string[]> {
    const errors: string[] = []
    this.page.on('console', (msg) => {
      if (msg.type() === 'error') errors.push(msg.text())
    })
    return errors
  }

  async checkNoConsoleErrors(errors: string[]) {
    const filtered = errors.filter(e => 
      !e.includes('favicon') && 
      !e.includes('Third-party') && 
      !e.includes('net::ERR_')
    )
    if (filtered.length > 0) {
      console.warn('Console errors:', filtered.join('\n'))
    }
  }
}
