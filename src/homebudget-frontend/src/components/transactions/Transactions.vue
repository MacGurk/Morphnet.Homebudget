<template>
  <TitleBar>Transactions</TitleBar>
  <TransactionFilterLayout>
    <template #filters>
      <TransactionFilter />
    </template>
    <template #content>
      <TransactionsCrudTable />
    </template>
  </TransactionFilterLayout>
</template>

<script setup lang="ts">
import TitleBar from "@/components/common/TitleBar.vue";
import TransactionFilterLayout from "@/components/transactions/TransactionFilterLayout.vue";
import TransactionFilter from "@/components/transactions/TransactionFilter.vue";
import TransactionsCrudTable from "@/components/transactions/TransactionsCrudTable.vue";
</script>

<style scoped></style>
