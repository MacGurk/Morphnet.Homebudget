<template>
  <v-navigation-drawer
    v-model="navStore.isNavOpen"
    :temporary="mobile"
    :permanent="!mobile"
  >
    <v-list-item :title="username"> </v-list-item>
    <v-divider></v-divider>
    <v-list nav>
      <v-list-item
        prepend-icon="mdi-home"
        title="Home"
        value="home"
        to="/"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-cash"
        title="Transactions"
        value="transactions"
        to="/transactions"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-handshake"
        title="Settlement"
        value="settlement"
        to="/settlement"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-account"
        title="User"
        value="user"
        to="/users"
      ></v-list-item>
    </v-list>
    <v-divider></v-divider>

    <template #append>
      <v-list-item
        prepend-icon="mdi-logout"
        title="Logout"
        value="logout"
        @click.stop="onLogout"
      ></v-list-item>
      <v-list-item
        :prepend-icon="
          themeStore.theme === 'light'
            ? 'mdi-weather-sunny'
            : 'mdi-weather-night'
        "
        title="Toggle Theme"
        @click.stop="onClick"
      ></v-list-item>
      <v-list-item></v-list-item>
    </template>
  </v-navigation-drawer>
</template>

<script setup lang="ts">
import { useThemeStore } from '@/stores/theme.store';
import { useAuthStore } from '@/stores/auth.store';
import { ref } from 'vue';
import { useNavStore } from '@/stores/nav.store';
import { useDisplay } from 'vuetify';

const { mobile } = useDisplay();

const themeStore = useThemeStore();
const authStore = useAuthStore();
const navStore = useNavStore();

const username = ref<string>('');
if (authStore.auth) {
  username.value = authStore.auth.user.name;
}

const onClick = () => {
  themeStore.toggleTheme();
};
const onLogout = () => {
  authStore.logout();
};
</script>

<style scoped></style>
