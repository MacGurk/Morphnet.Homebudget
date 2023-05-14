import { createApp } from 'vue';
import App from './App.vue';
// Vuetify
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { VDataTable } from 'vuetify/labs/VDataTable';
import { aliases, mdi } from 'vuetify/iconsets/mdi';
import '@mdi/font/css/materialdesignicons.css';

import { createRouter, createWebHistory } from 'vue-router';
import Transactions from '@/components/transactions/Transactions.vue';
import Users from '@/components/users/Users.vue';
import User from '@/components/users/User.vue';
import Home from '@/components/Home/Home.vue';
import NotFound from '@/components/common/NotFound.vue';
import Login from '@/components/common/Login.vue';

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

const routes = [
  { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
  { path: '/', name: 'Home', component: Home },
  { path: '/transactions', name: 'Transactions', component: Transactions },
  { path: '/users', name: 'Users', component: Users },
  { path: '/users/:id', name: 'User', component: User },
  { path: '/login', name: 'Login', component: Login },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

createApp(App).use(vuetify).use(router).mount('#app');
