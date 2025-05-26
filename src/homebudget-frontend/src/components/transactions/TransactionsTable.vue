<template>
  <div :class="mobile ? 'ma-3' : 'ma-16'">
    <v-data-table
      :headers="headers"
      :items="$props.transactions"
      :items-per-page="50"
      :loading="props.loading"
    >
      <template #top>
        <v-toolbar flat>
          <v-toolbar-title>{{ $props.title }}</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <slot name="createForm"></slot>
        </v-toolbar>
      </template>
      <template #item.date="{ value }">
        {{ date.format(value, 'keyboardDate') }}
      </template>
      <template #item.isSettled="{ value }">
        <v-icon v-if="value" size="small" color="success">
          mdi-check-circle-outline
        </v-icon>
        <v-icon v-else size="small" color="error">
          mdi-close-circle-outline
        </v-icon>
      </template>
      <template #item.actions="{ item }">
        <CrudActions
          :item-id="item.id"
          @edit-item="editTransaction"
          @delete-item="deleteTransaction"
        ></CrudActions>
      </template>
    </v-data-table>
  </div>
</template>

<script setup lang="ts">
import { onBeforeMount } from 'vue';
import Transaction from '@/entities/Transaction';
import CrudActions from '@/components/common/CrudActions.vue';
import router from '@/router';
import { DataTableHeader } from 'vuetify/lib/components/VDataTable/types';
import { useDisplay, useDate } from 'vuetify';

const { mobile } = useDisplay();
const date = useDate();

const props = withDefaults(
  defineProps<{
    title: string;
    transactions: Transaction[];
    hideActions?: boolean;
    loading?: boolean;
  }>(),
  {
    hideActions: false,
    loading: false,
  },
);

const headers: DataTableHeader[] = [
  { title: 'Date', align: 'start', key: 'date', width: '5%' },
  { title: 'Description', align: 'start', key: 'description' },
  { title: 'User', align: 'start', key: 'user.name' },
  { title: 'Price', align: 'start', key: 'price' },
  { title: 'Settled', align: 'start', key: 'isSettled' },
];

const emit = defineEmits(['editTransaction', 'deleteTransaction']);

onBeforeMount(() => {
  if (!props.hideActions) {
    headers.push({
      title: 'Actions',
      align: 'start',
      key: 'actions',
      sortable: false,
      width: '10%',
    });
  }
});

const editTransaction = (id: number) => {
  router.push(`/transactions/${id}/edit`);
};

const deleteTransaction = (id: number) => {
  emit('deleteTransaction', id);
};

const formatDate = (date: string): string => {
  return new Date(date).toLocaleDateString();
};
</script>

<style scoped></style>
