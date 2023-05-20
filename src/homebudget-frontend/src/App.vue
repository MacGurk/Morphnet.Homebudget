<template>
  <v-app :theme="theme">
    <v-app-bar>
      <v-app-bar-title>HomeBudget</v-app-bar-title>
    </v-app-bar>
    <NavigationDrawer @theme="onClick"></NavigationDrawer>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import NavigationDrawer from '@/components/common/NavigationDrawer.vue';

export default defineComponent({
  name: 'App',
  components: {
    NavigationDrawer,
  },
  data() {
    return {
      theme: 'dark',
      pageName: 'asd',
    };
  },
  methods: {
    onClick(): void {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    },
  },
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

</style>
