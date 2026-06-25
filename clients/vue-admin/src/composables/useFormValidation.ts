import { ref, computed } from 'vue';

export function useFormValidation() {
  const errors = ref<Record<string, string>>({});

  const validateEmail = (email: string): boolean => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!re.test(email)) {
      errors.value.email = 'Invalid email address';
      return false;
    }
    delete errors.value.email;
    return true;
  };

  const validateRequired = (value: any, fieldName: string): boolean => {
    if (!value || (typeof value === 'string' && !value.trim())) {
      errors.value[fieldName] = `${fieldName} is required`;
      return false;
    }
    delete errors.value[fieldName];
    return true;
  };

  const validateMinLength = (value: string, minLength: number, fieldName: string): boolean => {
    if (value.length < minLength) {
      errors.value[fieldName] = `${fieldName} must be at least ${minLength} characters`;
      return false;
    }
    delete errors.value[fieldName];
    return true;
  };

  const isValid = computed(() => Object.keys(errors.value).length === 0);

  const resetErrors = () => {
    errors.value = {};
  };

  return {
    errors,
    isValid,
    validateEmail,
    validateRequired,
    validateMinLength,
    resetErrors
  };
}
