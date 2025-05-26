<template>
  <v-dialog>
    <TransactionsTable
      title="Unsettled transactions of User"
      :transactions="$props.transactions"
    ></TransactionsTable>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import Transaction from '@/entities/Transaction';

export default defineComponent({
  name: 'SettlementTransactionsDialog',
  components: { TransactionsTable },
  props: {
    transactions: { type: Object as PropType<Transaction[]>, required: true },
  },
});
</script>

<style scoped></style>
