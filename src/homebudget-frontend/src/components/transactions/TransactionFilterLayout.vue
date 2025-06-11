<script setup lang="ts">
import {onMounted, provide, reactive, ref, watch} from "vue";
import {Filters, FilterSymbol} from "@/components/transactions/filters";
import Transaction from "@/entities/Transaction";
import TransactionApi from "@/api/TransactionApi";
import TransactionForCreation from "@/models/TransactionForCreation";

const currentDate = new Date();
const transactionApi = new TransactionApi();

const filters = ref<Filters>({
  month: currentDate.getMonth(),
  year: currentDate.getFullYear(),
});
const transactions = ref<Transaction[]>([]);
const loading = ref(false);
function setFilters(update: Partial<Filters> ) {
  filters.value = {...filters.value, ...update};
}

async function fetchTransactions(): Promise<void> {
  transactions.value = [];
  try {
    loading.value = true;
    transactions.value = await transactionApi.getFiltered(
        filters.value.month,
        filters.value.year,
    );
  } catch (e) {
    console.error('Error:', e);
  } finally {
    loading.value = false;
  }
}

async function addTransaction(newTransaction: TransactionForCreation): Promise<void> {
  try {
    const transaction = await transactionApi.add(newTransaction);
    transactions.value.push(transaction);
  } catch (e) {
    console.error(e);
  }
}

async function deleteTransaction(id: number): Promise<void> {
  try {
    await transactionApi.delete(id);
    transactions.value = transactions.value.filter((t) => t.id !== id);
  } catch (e) {
    console.error(e);
  }
}

onMounted(() => {
  fetchTransactions();
});

watch(filters, async () => {
  await fetchTransactions();
})

provide(FilterSymbol, {
  filters,
  setFilters,
  transactions,
  loading,
  addTransaction,
  deleteTransaction,
});
</script>

<template>
  <slot name="filters" />
  <slot name="content" />
</template>

<style scoped>

</style>