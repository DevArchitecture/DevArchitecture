import { describe, it, expect } from 'vitest'
import { useFormValidation } from '../useFormValidation'

describe('useFormValidation', () => {
  it('validates email correctly', () => {
    const { validateEmail, errors } = useFormValidation()
    expect(validateEmail('test@example.com')).toBe(true)
    expect(errors.value.email).toBeUndefined()
  })

  it('rejects invalid email', () => {
    const { validateEmail, errors } = useFormValidation()
    expect(validateEmail('invalid')).toBe(false)
    expect(errors.value.email).toBeDefined()
  })

  it('validates required fields', () => {
    const { validateRequired, errors } = useFormValidation()
    expect(validateRequired('', 'Name')).toBe(false)
    expect(errors.value.Name).toBeDefined()
  })

  it('validates min length', () => {
    const { validateMinLength, errors } = useFormValidation()
    expect(validateMinLength('ab', 3, 'Password')).toBe(false)
    expect(errors.value.Password).toBeDefined()
  })

  it('tracks validation state', () => {
    const { validateRequired, isValid } = useFormValidation()
    expect(isValid.value).toBe(true)
    validateRequired('', 'Name')
    expect(isValid.value).toBe(false)
  })

  it('resets errors', () => {
    const { errors, resetErrors } = useFormValidation()
    errors.value['test'] = 'error'
    resetErrors()
    expect(Object.keys(errors.value).length).toBe(0)
  })
})
