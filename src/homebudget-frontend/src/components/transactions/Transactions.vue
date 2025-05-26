<template>
  <TitleBar>Transactions</TitleBar>
  <TransactionFilter @filter-changed="handleFilterChange" />
  <TransactionsTable
    title="Transactions Overview"
    :transactions="transactions"
    :loading="loading"
    @delete-transaction="deleteTransaction"
  >
    <template #createForm>
      <TransactionCreateForm @add-transaction="addTransaction" />
    </template>
  </TransactionsTable>
  <ConfirmDialog ref="confirm" />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import Transaction from '@/entities/Transaction';
import TransactionApi from '@/api/TransactionApi';
import TransactionForCreation from '@/models/TransactionForCreation';
import { getDateString } from '@/utils/utils';
import ConfirmDialog from '@/components/common/ConfirmDialog.vue';
import TransactionFilter from '@/components/transactions/TransactionFilter.vue';
import { YearMonthFilter } from '@/types/YearMonthFilter';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import TransactionCreateForm from '@/components/transactions/TransactionCreateForm.vue';

interface TransactionsData {
  transactions: Transaction[];
  filter: YearMonthFilter;
  loading: boolean;
}

const transactionApi = new TransactionApi();

export default defineComponent({
  name: 'Transactions',
  components: {
    TransactionCreateForm,
    TransactionsTable,
    TransactionFilter,
    ConfirmDialog,
    TitleBar,
  },
  data(): TransactionsData {
    return {
      transactions: [],
      filter: {
        month: new Date().getMonth(),
        year: new Date().getFullYear(),
      },
      loading: false,
    };
  },
  created() {
    this.fetchTransactions();
  },
  methods: {
    getDateString,
    async fetchTransactions(): Promise<void> {
      this.transactions = [];
      try {
        this.loading = true;
        this.transactions = await transactionApi.getFiltered(
          this.filter.month,
          this.filter.year,
        );
      } catch (e) {
        console.error('Error:', e);
      } finally {
        this.loading = false;
      }
    },
    async addTransaction(
      newTransaction: TransactionForCreation,
    ): Promise<void> {
      try {
        const transaction = await transactionApi.add(newTransaction);
        this.transactions.push(transaction);
      } catch (e) {
        console.error(e);
      }
    },
    async deleteTransaction(id: number): Promise<void> {
      if (
        await this.$refs.confirm.open(
          `Delete user ${id}`,
          `Do you really want to delete the transaction '${id}'?`,
          'Delete',
          'error',
        )
      ) {
        try {
          await transactionApi.delete(id);
          this.transactions = this.transactions.filter((t) => t.id !== id);
        } catch (e) {
          console.error(e);
        }
      }
    },
    handleFilterChange(month: number, year: number) {
      this.filter.year = year;
      this.filter.month = month;
      this.fetchTransactions();
    },
  },
});
</script>

<style scoped></style>
