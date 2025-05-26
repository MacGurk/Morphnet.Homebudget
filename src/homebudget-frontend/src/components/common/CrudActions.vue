<template>
  <v-icon
    size="large"
    color="info"
    class="mr-4"
    @click="editItem($props.itemId)"
  >
    mdi-pencil
  </v-icon>
  <v-icon size="large" @click="deleteItem($props.itemId)"> mdi-delete </v-icon>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
  name: 'CrudActions',
  props: {
    itemId: { type: Number, required: true },
  },
  emits: ['editItem', 'deleteItem'],
  methods: {
    editItem(id: number) {
      this.$emit('editItem', id);
    },
    deleteItem(id: number) {
      this.$emit('deleteItem', id);
    },
  },
});
</script>

<style scoped></style>
