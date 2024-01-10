<script setup lang="ts">

const props = defineProps<{
  title: string;
}>();
</script>

<template>
  <v-card>
    <v-card-title>
      <h2>{{ title }}</h2>
    </v-card-title>
    <v-card-text>
      <slot></slot>
    </v-card-text>
    
  </v-card>
</template>

<style scoped>

</style>