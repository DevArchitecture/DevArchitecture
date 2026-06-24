import axios from 'axios';
import type { AxiosInstance, AxiosError } from 'axios';
import { useAuthStore } from '@/stores/auth';
import { useNotificationStore } from '@/stores/notification';

const apiClient: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'https://localhost:5101/api/v1',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor
apiClient.interceptors.request.use(
  (config) => {
    const authStore = useAuthStore();
    if (authStore.token) {
      config.headers.Authorization = `Bearer ${authStore.token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor
apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const authStore = useAuthStore();
    const notificationStore = useNotificationStore();

    if (error.response?.status === 401) {
      authStore.logout();
      window.location.href = '/login';
    } else if (error.response?.status === 403) {
      notificationStore.showError('You do not have permission to perform this action');
    } else if (error.response?.status === 429) {
      notificationStore.showError('Too many requests. Please try again later.');
    } else if (error.response && error.response.status >= 500) {
      notificationStore.showError('Server error. Please try again later.');
    }

    return Promise.reject(error);
  }
);

export default apiClient;
