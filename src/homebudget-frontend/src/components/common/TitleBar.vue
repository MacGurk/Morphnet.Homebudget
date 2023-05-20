<template>
  <div class="py-3">
    <h2 class="title"><slot></slot></h2>
  </div>
  <v-divider></v-divider>
</template>

<script lang="ts">
import {defineComponent} from 'vue';

export default defineComponent({
  name: 'TitleBar',
  props: {
    title: String,
  },
});
</script>

<style scoped>
.title {
  text-align: center;
}
</style>
