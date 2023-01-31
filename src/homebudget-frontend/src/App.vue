<template>
  <v-app :theme="theme">
    <NavigationDrawer
        @theme="onClick"
    ></NavigationDrawer>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import HelloWorld from './components/HelloWorld.vue';
import NavigationDrawer from "@/components/common/NavigationDrawer.vue";


export default defineComponent({
  name: 'App',
  components: {
    NavigationDrawer,
    HelloWorld
  },
  data() {
    return {
      theme: 'dark',
    }
  },
  methods: {
    onClick(): void {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    }
  }
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
