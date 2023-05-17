<template>
  <div class="pa-16">
    <v-data-table :headers="$props.headers" :items="$props.items" :items-per-page="10">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>{{ $props.title }}</v-toolbar-title>
          <v-divider
              class="mx-4"
              inset
              vertical
          ></v-divider>
          <v-spacer></v-spacer>
          <slot name="createForm"></slot>
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon size="large" color="info" class="mr-4" @click="editItem(item.raw.id)">
          mdi-pencil
        </v-icon>
        <v-icon size="large" @click="deleteItem(item.raw.id)">
          mdi-delete
        </v-icon>
      </template>
      <template v-slot:item.date="{ item }">
        {{ formatDate(item.raw.date) }}
      </template>
      <template v-slot:item.isSettled="{ item }">
        <v-icon v-if="item.raw.isSettled" size="small" color="success">
          mdi-check-circle-outline
        </v-icon>
        <v-icon v-else size="small" color="error">
          mdi-close-circle-outline
        </v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import {defineComponent, PropType} from 'vue';
import {getDateString} from '../../utils/utils';

export type CrudTableHeader = [
    ...Array<{title: string; align?: string; key: string; sortable?: boolean; width?: string;}>,
    {title: 'Actions'; align?: string; key: 'actions'; sortable?: boolean; width?: string;}
]

type TableItem = Record<string, any> & {
  id: number;
}

export default defineComponent({
  name: 'CRUDTable',
  props: {
    headers: {type: Object as () => PropType<CrudTableHeader>, required: true},
    items: {type: Array as () => TableItem[], required: true},
    title: {type: String, required: true}
  },
  emits: {
    deleteItem: (_id: number) => true,
    editItem: (_id: number) => true
  },
  methods: {
    getDateString,
    async deleteItem(id: number) {
      this.$emit('deleteItem', id);
    },
    editItem(id: number) {
      this.$emit('editItem', id);
    },
    formatDate(date: string): string {
      return new Date(date).toLocaleDateString();
    }
  }
})
</script>

<style scoped>

</style>