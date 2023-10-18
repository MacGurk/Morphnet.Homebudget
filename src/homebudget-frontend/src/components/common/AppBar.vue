<script setup lang="ts">
import { useNavStore } from '@/stores/nav.store';

const navStore = useNavStore();

const toggleNav = () => {
  navStore.toggleNav();
};
</script>

<template>
  <v-app-bar>
    <v-app-bar-nav-icon
      variant="text"
      @click.stop="toggleNav"
    ></v-app-bar-nav-icon>
    <v-app-bar-title>HomeBudget</v-app-bar-title>
  </v-app-bar>
</template>

<style scoped></style>
