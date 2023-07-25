import { defineStore } from 'pinia';
import router from '@/router';
import AuthApi from '@/api/AuthApi';
import AuthData from '@/models/AuthData';

interface AuthState {
  auth: AuthData | null;
  returnUrl: string | null;
}
export const useAuthStore = defineStore('auth', {
  state: (): AuthState => ({
    auth: JSON.parse(localStorage.getItem('auth') || 'null'),
    returnUrl: null,
  }),
  actions: {
    async login(username: string, password: string) {
      const authApi = new AuthApi();
      const result = await authApi.login(username, password);
      if (result.result == 'success') {
        this.auth = result.data;
        localStorage.setItem('auth', JSON.stringify(result.data));
        router.push(this.returnUrl || '/');
      } else {
        throw new Error(result.message);
      }
    },
    logout() {
      this.auth = null;
      localStorage.removeItem('auth');
      router.push('/login');
    },
    isAuthenticated(): boolean {
      return !!this.auth;
    },
  },
});
