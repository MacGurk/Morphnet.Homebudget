<template>
  <div :class="mobile ? 'ma-3' : 'ma-16'">
    <v-data-table
      :headers="headers"
      :items="props.transactions"
      :items-per-page="50"
      :loading="props.loading"
    >
      <template #top>
        <v-toolbar flat>
          <v-toolbar-title>{{ $props.title }}</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <slot name="create" />
            
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
      <template #item.actions="slotProps">
        <slot name="actions" v-bind="slotProps" />
      </template>
    </v-data-table>
  </div>
  <ConfirmDialog ref="confirm" />
</template>

<script setup lang="ts">
import { onBeforeMount, ref } from 'vue';
import { DataTableHeader } from 'vuetify/lib/components/VDataTable/types';
import { useDisplay, useDate } from 'vuetify';
import ConfirmDialog from "@/components/common/ConfirmDialog.vue";
import Transaction from "@/entities/Transaction";

const { mobile } = useDisplay();
const date = useDate();

const confirm = ref<InstanceType<typeof ConfirmDialog> | null>(null);

const props = withDefaults(
  defineProps<{
    title: string;
    hideActions?: boolean;
    loading?: boolean;
    transactions: Transaction[];
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
</script>

<style scoped></style>
