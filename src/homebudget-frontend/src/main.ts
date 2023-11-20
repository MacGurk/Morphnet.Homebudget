import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
// Vuetify
import 'vuetify/styles';
import {createVuetify, ThemeDefinition} from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { aliases, mdi } from 'vuetify/iconsets/mdi';
import '@mdi/font/css/materialdesignicons.css';
import {createPinia, Pinia} from 'pinia';
import { Form } from 'vee-validate';

const cattpucinLatte : ThemeDefinition = {
  dark: true,
  colors: {
    background: '#e6e9ef',
    surface: '#dce0e8',
    primary: '#209fb5',
    'primary-darken-1': '#0f4852',
    secondary: '#dc8a78',
    'secondary-darken-1': '#674139',
    error: '#d20f39',
    info: '#1e66f5',
    success: '#40a02b',
    warning: '#df8e1d',
  }
}

const cattpucinMocha : ThemeDefinition = {
  dark: false,
  colors: {
    background: '#181825',
    surface: '#11111b',
    primary: '#74c7ec',
    'primary-darken-1': '#3f6c80',
    secondary: '#f5e0dc',
    'secondary-darken-1': '#796e6c',
    error: '#f38ba8',
    info: '#89b4fa',
    success: '#a6e3a1',
    warning: '#f9e2af',
  }
}

const vuetify = createVuetify({
  components,
  icons: {
    defaultSet: 'mdi',
    aliases,
    sets: {
      mdi,
    },
  },
  directives,
  theme: {
    defaultTheme: 'dark',
    themes: {
      dark: cattpucinMocha,
      light: cattpucinLatte,
    },
  },
});

const pinia : Pinia = createPinia();

createApp(App)
  .use(vuetify)
  .use(router)
  .component('From', Form)
  .use(pinia)
  .mount('#app');
