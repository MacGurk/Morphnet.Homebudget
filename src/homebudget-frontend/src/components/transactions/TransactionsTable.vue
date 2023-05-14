<template>
  <div class="pa-16">
    <v-data-table :headers="headers" :items="$props.transactions" :items-per-page="10">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Transactions Overview</v-toolbar-title>
          <v-divider
              class="mx-4"
              inset
              vertical
          ></v-divider>
          <v-spacer></v-spacer>
          <TransactionCreateForm  />
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon size="small" class="mr-2" @click="editUser(item.raw.id)"
        >mdi-pencil
        </v-icon
        >
        <v-icon size="small" @click="deleteUser(item.raw.id)"
        >mdi-delete
        </v-icon
        >
      </template>
    </v-data-table>
  </div>
  <CRUDTable :items="$props.transactions" :headers="headers">
    <template v-slot:toolbar>
      <v-toolbar flat>
        <v-toolbar-title>Transactions Overview</v-toolbar-title>
        <v-divider
            class="mx-4"
            inset
            vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <TransactionCreateForm  />
      </v-toolbar>
    </template>
  </CRUDTable>
</template>

<script lang="ts">
import {defineComponent, PropType} from 'vue';
import Transaction from '@/entities/Transaction';
import TransactionCreateForm from '@/components/transactions/TransactionCreateForm.vue';
import CRUDTable from '@/components/common/CRUDTable.vue';

interface TransactionsTableData {
  loading: boolean;
  headers: {
    title: string,
    align?: string,
    key: string,
    sortable?: boolean,
    width?: string,
  }[];
}

export default defineComponent({
  name: 'TransactionsTable',
  components: {CRUDTable, TransactionCreateForm},
  props: {
    transactions: {type: Object as PropType<Transaction[]>, required: true},
  },
  data(): TransactionsTableData {
    return {
      loading: false,
      headers: [
        {title: 'Id', align: 'start', key: 'id', width: '5%'},
        {title: 'Date', align: 'start', key: 'date'},
        {title: 'Description', align: 'start', key: 'description'},
        {title: 'User', align: 'start', key: 'user.name'},
        {title: 'Price', align: 'start', key: 'price'},
        {title: 'Actions', align: 'start', key: 'actions', sortable: false, width: '10%'},
      ]
    }
  },
  emits: {
    editTransaction: null,
    deleteTransaction: null,
    addTransaction: null,
  },
  methods: {
    editTransaction(id: number) {
      this.$emit('editTransaction', id);
    },
    deleteTransaction(id: number) {
      this.$emit('deleteTransaction', id);
    },
    addTransaction(transaction: Transaction) {
      this.$emit('addTransaction', transaction);
    }
  }
})
</script>

<style scoped>

</style>