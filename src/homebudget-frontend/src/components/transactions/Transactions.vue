<template>
  <TitleBar>Transactions</TitleBar>
  <TransactionFilter @filter-changed="handleFilterChange" />
  <TransactionsTable
      title="Transactions Overview"
      :transactions="transactions"
      @edit-transaction="editTransaction"
      @delete-transaction="deleteTransaction"
  >
    <template v-slot:createForm>
      <TransactionCreateForm :users="users" @add-transaction="addTransaction" />
    </template>
  </TransactionsTable>
  <ConfirmDialog ref="confirm" />
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import Transaction from '@/entities/Transaction';
import TransactionApi from '@/api/TransactionApi';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import TransactionForCreation from '@/models/TransactionForCreation';
import {getDateString} from '../../utils/utils';
import ConfirmDialog from '@/components/common/ConfirmDialog.vue';
import TransactionFilter from '@/components/transactions/TransactionFilter.vue';
import {YearMonthFilter} from '@/types/YearMonthFilter';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import TransactionCreateForm from '@/components/transactions/TransactionCreateForm.vue';

interface TransactionsData {
  transactions: Transaction[];
  users: User[],
  filter: YearMonthFilter;
}

const userApi = new UserApi();
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
      users: [],
      filter: {
        month: new Date().getMonth(),
        year: new Date().getFullYear(),
      }
    }
  },
  created() {
    this.fetchTransactions();
    this.fetchUsers();
  },
  methods: {
    getDateString,
    async fetchTransactions(): Promise<void> {
      this.transactions = [];
      try {
        this.transactions = await transactionApi.getFiltered(this.filter.month, this.filter.year);
      } catch (e) {
        console.error('Error:', e);
      }
    },
    async fetchUsers(): Promise<void> {
      this.users = [];
      try {
        this.users = await userApi.get();
      } catch (e) {
        console.error('Error:', e);
      }
    },
    async addTransaction(newTransaction: TransactionForCreation): Promise<void> {
      try {
        const transaction = await transactionApi.add(newTransaction);
        this.transactions.push(transaction);
      } catch (e) {
        console.error(e);
      }
    },
    editTransaction(id: number): void {
      console.log(`Edit transaction ${id}`)
    },
    async deleteTransaction(id: number): Promise<void> {
      if (await this.$refs.confirm.open(
          `Delete user ${id}`,
          `Do you really want to delete the transaction '${id}'?`,
          'Delete',
          'error'
      )) {
        if (await transactionApi.delete(id)) {
          this.transactions = this.transactions.filter(t => t.id !== id);
        }
      }
    },
    handleFilterChange(month: number, year: number) {
      this.filter.year = year;
      this.filter.month = month;
      this.fetchTransactions();
    }
  }
});
</script>

<style scoped></style>
