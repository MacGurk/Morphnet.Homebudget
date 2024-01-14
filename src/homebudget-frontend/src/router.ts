import NotFound from '@/components/common/NotFound.vue';
import Home from '@/components/Home/Home.vue';
import Transactions from '@/components/transactions/Transactions.vue';
import Settlement from '@/components/settle/Settlement.vue';
import Users from '@/components/users/Users.vue';
import User from '@/components/users/User.vue';
import Login from '@/components/common/Login.vue';
import { createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from '@/stores/auth.store';
import UserEdit from '@/components/users/UserEdit.vue';
import TransactionEdit from '@/components/transactions/TransactionEdit.vue';

const routes = [
  { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
  { path: '/', name: 'Home', component: Home },
  { path: '/transactions', name: 'Transactions', component: Transactions },
  {
    path: '/transactions/:id/edit',
    name: 'TransactionEdit',
    component: TransactionEdit,
  },
  { path: '/settlement', name: 'Settlement', component: Settlement },
  { path: '/users', name: 'Users', component: Users },
  { path: '/users/:id', name: 'User', component: User },
  { path: '/users/:id/edit', name: 'UserEdit', component: UserEdit },
  { path: '/login', name: 'Login', component: Login },
];

const publicRoutes = ['/login'];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

router.beforeEach(async (to) => {
  const authRequired = !publicRoutes.includes(to.path);
  const authStore = useAuthStore();
  
  if (authRequired && (!authStore.auth || !authStore.isTokenValid())) {
    authStore.returnUrl = to.fullPath;
    return '/login';
  }
});

export default router;
