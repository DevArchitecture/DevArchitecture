import { useNotificationStore } from '@/stores/notification';

export function useErrorHandler() {
  const notificationStore = useNotificationStore();

  const handleError = (error: any, context?: string) => {
    const message = getErrorMessage(error);
    const title = context || 'Error';
    
    notificationStore.showError(message, title);
    
    if (import.meta.env.DEV) {
      console.error(`[${context}]`, error);
    }
  };

  const getErrorMessage = (error: any): string => {
    if (error.response?.data?.message) {
      return error.response.data.message;
    }
    
    if (error.message) {
      return error.message;
    }
    
    return 'An unexpected error occurred';
  };

  return { handleError };
}
