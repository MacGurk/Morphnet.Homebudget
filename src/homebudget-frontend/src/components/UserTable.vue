<template>
  <v-data-table
      :headers="headers"
      :items="users"
      :items-per-page="2">
  </v-data-table>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
  name: "UserTable",
  data() {
    
  }
})
</script>

<style scoped>

</style>