import { defineStore } from 'pinia';
import router from '@/router';
import AuthApi from '@/api/AuthApi';
import AuthData from '@/models/AuthData';
import User from "@/entities/User";

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
    isTokenValid(): boolean {
      if (this.auth == null) {
        return false;
      }
      const token = JSON.parse(atob(this.auth.token.split('.')[1]));
      const expire = new Date(token.exp * 1000);
      
      if (expire < new Date()) {
        this.auth = null;
        localStorage.removeItem('auth');
        return false;
      }
      
      return true;
    },
    isAuthenticated(): boolean {
      return !!this.auth;
    },
    getAuthenticatedUser(): User | null {
      if (this.auth == null) {
        return null;
      }
      const user = new User();
      user.id = this.auth.user.id;
      user.name = this.auth.user.name;
      user.email = this.auth.user.email;
      user.isContributor = this.auth.user.isContributor;
      return user;
    }
  },
});
