<template>
  <v-navigation-drawer
      v-model="drawer"
      expand-on-hover
      rail
  >
    <v-list nav>
      <router-link to="/">
        <v-list-item prepend-icon="mdi-home" title="Home" value="home"></v-list-item>
      </router-link>
      <router-link to="/transactions">
        <v-list-item prepend-icon="mdi-cash" title="Transactions" value="transactions"></v-list-item>
      </router-link>
      <router-link to="/users">
        <v-list-item prepend-icon="mdi-account" title="User"></v-list-item>
      </router-link>
    </v-list>
    <v-divider></v-divider>
    
    <template v-slot:append>
      <v-list-item
          :prepend-icon="theme === 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"
          title="Toggle Theme"
          @click.stop="onClick"
      ></v-list-item>
      <v-list-item>
      </v-list-item>
    </template>

  </v-navigation-drawer>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
  name: "NavigationDrawer",
  data() {
    return {
      theme: 'dark',
      drawer: true,
    }
  },
  methods: {
    onClick():void {
       this.theme = this.theme === 'light' ? 'dark' : 'light';
       this.$emit('theme');
    }
  }
});
</script>

<style scoped>

</style>