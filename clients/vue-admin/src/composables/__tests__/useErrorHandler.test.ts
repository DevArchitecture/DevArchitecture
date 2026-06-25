import { describe, it, expect, vi } from 'vitest'
import { useErrorHandler } from '../useErrorHandler'

vi.mock('@/stores/notification', () => ({
  useNotificationStore: () => ({
    showError: vi.fn()
  })
}))

describe('useErrorHandler', () => {
  it('returns handleError function', () => {
    const { handleError } = useErrorHandler()
    expect(handleError).toBeDefined()
    expect(typeof handleError).toBe('function')
  })

  it('extracts message from response data', () => {
    const { handleError } = useErrorHandler()
    const error = { response: { data: { message: 'Custom error' } } }
    handleError(error, 'test')
  })

  it('handles error without response', () => {
    const { handleError } = useErrorHandler()
    const error = new Error('Network error')
    handleError(error, 'network')
  })
})
