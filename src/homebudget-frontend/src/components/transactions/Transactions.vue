<template>
  <TitleBar title="Transactions" />
  <CRUDTable
      title="Transactions Overview"
      :items="transactions"
      :headers="headers"
  >
    <template v-slot:createForm>
      <TransactionCreateForm @add-transaction="addTransaction" />
    </template>
  </CRUDTable>
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import Transaction from '@/entities/Transaction';
import {fetchApiTransactions, postApiTransaction} from '@/api/TransactionApi';
import User from '@/entities/User';
import {fetchUsers} from '@/api/UserApi';
import CRUDTable, {CrudTableHeader} from '@/components/common/CRUDTable.vue';
import TransactionCreateForm from '@/components/transactions/TransactionCreateForm.vue';
import TransactionForCreation from '@/models/TransactionForCreation';
import {getDateString} from '../../utils/utils';

interface TransactionsData {
  transactions: Transaction[];
  users: User[],
  headers: CrudTableHeader;
}

export default defineComponent({
  name: 'Transactions',
  components: {
    CRUDTable,
    TransactionCreateForm,
    TitleBar,
  },
  data(): TransactionsData {
    return {
      transactions: [],
      users: [],
      headers: [
        {title: 'Id', align: 'start', key: 'id', width: '5%'},
        {title: 'Date', align: 'start', key: 'date', width: '5%'},
        {title: 'Description', align: 'start', key: 'description'},
        {title: 'User', align: 'start', key: 'user.name'},
        {title: 'Price', align: 'start', key: 'price'},
        {title: 'Actions', align: 'start', key: 'actions', sortable: false, width: '10%'},
      ]
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
        this.transactions = await fetchApiTransactions();
      } catch (e) {
        console.error('Error:', e);
      }
    },
    async fetchUsers(): Promise<void> {
      this.users = [];
      try {
        this.users = await fetchUsers();
      } catch (e) {
        console.error('Error:', e);
      }
    },
    async addTransaction(newTransaction: TransactionForCreation): Promise<void> {
      try {
        const transaction = await postApiTransaction(newTransaction);
        this.transactions.push(transaction);
      } catch (e) {
        console.error(e);
      }
    }
  }
});
</script>

<style scoped></style>
