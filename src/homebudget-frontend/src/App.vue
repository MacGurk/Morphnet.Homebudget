<template>
  <v-app :theme="themeStore.theme">
    <v-app-bar>
      <v-app-bar-title>HomeBudget</v-app-bar-title>
    </v-app-bar>
    <NavigationDrawer v-if="!!authStore.auth"></NavigationDrawer>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import NavigationDrawer from '@/components/common/NavigationDrawer.vue';
import { useThemeStore } from '@/stores/theme.stora';
import { useAuthStore } from '@/stores/auth.store';

const themeStore = useThemeStore();
const authStore = useAuthStore();
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
