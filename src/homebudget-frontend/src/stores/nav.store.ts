import { defineStore } from 'pinia';

interface NavState {
  isNavOpen: boolean;
}
export const useNavStore = defineStore('nav', {
  state: (): NavState => ({
    isNavOpen: false,
  }),
  actions: {
    toggleNav() {
      this.isNavOpen = !this.isNavOpen;
    },
  },
});
