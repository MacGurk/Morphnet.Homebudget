<template>
  <TitleBar>Transactions</TitleBar>
  <TransactionFilterLayout>
    <template #filters>
      <TransactionFilter />
    </template>
    <template #content>
      <TransactionsTable
          title="Transactions Overview"
          :loading="loading"
      >
      </TransactionsTable>
    </template>
  </TransactionFilterLayout>
</template>

<script setup lang="ts">
import {ref} from 'vue';
import TitleBar from "@/components/common/TitleBar.vue";
import TransactionFilterLayout from "@/components/transactions/TransactionFilterLayout.vue";
import TransactionFilter from "@/components/transactions/TransactionFilter.vue";
import TransactionsTable from "@/components/transactions/TransactionsTable.vue";


const loading = ref(false);


</script>

<style scoped></style>
