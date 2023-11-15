import { defineStore } from 'pinia';

interface ThemeState {
  theme: string;
}
export const useThemeStore = defineStore('theme', {
  state: (): ThemeState => ({
    theme: 'dark',
  }),
  actions: {
    toggleTheme() {
      this.theme = this.theme === 'dark' ? 'light' : 'dark';
    },
  },
});
