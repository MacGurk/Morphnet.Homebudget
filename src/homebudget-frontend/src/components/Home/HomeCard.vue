<script setup lang="ts">

const props = defineProps<{
  title: string;
}>();
</script>

<template>
  <v-card height="30em">
    <v-card-title>
      <h2>{{ title }}</h2>
    </v-card-title>
    <v-card-text class="asdf">
      <slot></slot>
    </v-card-text>
    
  </v-card>
</template>

<style scoped>
.asdf {
  height: 80%;
}
</style>