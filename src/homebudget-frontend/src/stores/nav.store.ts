import { defineStore } from 'pinia';

interface NavState {
  isNavOpen: boolean;
}


export const useNavStore = defineStore('nav', {
  state: (): NavState => ({
    isNavOpen: window.innerWidth > 600,
  }),
  actions: {
    toggleNav() {
      this.isNavOpen = !this.isNavOpen;
    },
  },
});
