import { useCallback, useEffect, useState } from 'react';

export function useErrorHandler() {
  const [error, setError] = useState<{ message: string } | null>(null);

  useEffect(() => {
    const handler = (event: Event) => {
      const detail = (event as CustomEvent).detail;
      setError({ message: detail?.message || 'An unexpected error occurred' });
      setTimeout(() => setError(null), 5000);
    };
    window.addEventListener('app-error', handler);
    return () => window.removeEventListener('app-error', handler);
  }, []);

  const handleError = useCallback((err: any, context?: string) => {
    const message = err?.response?.data?.message || err?.message || 'An unexpected error occurred';
    setError({ message });
    if (import.meta.env.DEV) {
      console.error(`[${context}]`, err);
    }
    setTimeout(() => setError(null), 5000);
  }, []);

  return { error, handleError, clearError: () => setError(null) };
}
