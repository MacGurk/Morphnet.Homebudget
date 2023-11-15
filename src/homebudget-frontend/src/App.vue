<template>
  <v-app :theme="themeStore.theme">
    <AppBar />
    <NavigationDrawer v-if="!!authStore.auth"></NavigationDrawer>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import NavigationDrawer from '@/components/common/NavigationDrawer.vue';
import { useThemeStore } from '@/stores/theme.store';
import { useAuthStore } from '@/stores/auth.store';
import AppBar from '@/components/common/AppBar.vue';

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
