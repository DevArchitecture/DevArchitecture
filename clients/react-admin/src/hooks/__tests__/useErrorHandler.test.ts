import { describe, it, expect } from 'vitest'

describe('useErrorHandler', () => {
  it('exports useErrorHandler function', async () => {
    const { useErrorHandler } = await import('../useErrorHandler')
    expect(useErrorHandler).toBeDefined()
    expect(typeof useErrorHandler).toBe('function')
  })

  it('exports index properly', async () => {
    const mod = await import('../index')
    expect(mod.useErrorHandler).toBeDefined()
  })
})
