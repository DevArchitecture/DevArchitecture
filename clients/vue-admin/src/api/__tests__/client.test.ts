import { describe, it, expect, vi, beforeEach } from 'vitest'

vi.mock('@/stores/auth', () => ({
  useAuthStore: () => ({
    token: 'test-token',
    logout: vi.fn()
  })
}))

vi.mock('@/stores/notification', () => ({
  useNotificationStore: () => ({
    showError: vi.fn()
  })
}))

describe('API Client', () => {
  beforeEach(() => {
    localStorage.clear()
  })

  it('creates axios instance with correct base URL', async () => {
    const { default: apiClient } = await import('../client')
    expect(apiClient.defaults.baseURL).toContain('localhost:5101')
    expect(apiClient.defaults.timeout).toBe(10000)
  })

  it('has request interceptor', async () => {
    const { default: apiClient } = await import('../client')
    const interceptors = apiClient.interceptors as any
    expect(interceptors.request).toBeDefined()
  })

  it('has response interceptor', async () => {
    const { default: apiClient } = await import('../client')
    const interceptors = apiClient.interceptors as any
    expect(interceptors.response).toBeDefined()
  })
})
