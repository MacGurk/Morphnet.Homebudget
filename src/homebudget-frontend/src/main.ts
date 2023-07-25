import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
// Vuetify
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { VDataTable } from 'vuetify/labs/VDataTable';
import { aliases, mdi } from 'vuetify/iconsets/mdi';
import '@mdi/font/css/materialdesignicons.css';
import { createPinia } from 'pinia';
import { Form } from 'vee-validate';

const vuetify = createVuetify({
  components: {
    ...components,
    VDataTable,
  },
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
  },
});

const pinia = createPinia();

createApp(App)
  .use(vuetify)
  .use(router)
  .component('From', Form)
  .use(pinia)
  .mount('#app');
