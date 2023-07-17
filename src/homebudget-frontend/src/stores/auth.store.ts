import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('user') || '{}'),
    returnUrl: null,
  }),
  // actions: {
  //   async login(username, password) {
  //     const user = await fet
  //     this.user = new User;
  //
  //   }
  // }
});
