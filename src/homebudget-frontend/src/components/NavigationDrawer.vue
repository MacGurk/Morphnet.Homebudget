<template>
  <v-navigation-drawer
      v-model="drawer"
      :rail="rail"
      permanent
      @click="rail = false"
  >
    <v-list-item prepend-avatar="mdi-account" title="User"></v-list-item>
    <v-list-item>
      <template v-slot:append>
        <v-btn v-show="!rail" variant="text" icon="mdi-chevron-left" @click.stop="rail = !rail"></v-btn>
      </template>
    </v-list-item>
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
      rail: true
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