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
    primary: '#dd7878',
    'primary-darken-1': '#673838',
    secondary: '#179299',
    'secondary-darken-1': '#0b464a',
    error: '#d20f39',
    info: '#1e66f5',
    success: '#40a02b',
    warning: '#df8e1d',
  }
}

const cattpucinMacciato  = {
  dark: false,
  colors: {
    background: '#1e2030',
    surface: '#181926',
    primary: '#b7bdf8',
    'primary-darken-1': '#616483',
    secondary: '#8bd5ca',
    'secondary-darken-1': '#61968f',
    error: '#ed8796',
    info: '#8aadf4',
    success: '#a6da95',
    warning: '#eed49f',
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
      dark: cattpucinMacciato,
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
