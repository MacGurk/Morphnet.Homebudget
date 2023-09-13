<template>
  <v-container class="w-25 bg-grey-lighten-1 rounded-xl mx-16 my-8">
    <v-progress-circular v-if="loading" indeterminate></v-progress-circular>
    <div v-else>
      <v-row class>
        <v-col class="v-col-auto"> Id: </v-col>
        <v-col class="v-col-auto">
          {{ props.user.id }}
        </v-col>
      </v-row>
      <v-row>
        <v-col class="v-col-auto"> Name: </v-col>
        <v-col class="v-col-auto">
          {{ props.user.name }}
        </v-col>
      </v-row>
      <v-row>
        <v-col class="v-col-auto"> E-Mail: </v-col>
        <v-col class="v-col-auto">
          {{ props.user.email }}
        </v-col>
      </v-row>
    </div>
  </v-container>
  <TransactionsTable
    :title="`Transactions of user ${props.user.id}`"
    :transactions="transactions"
    :loading="loadingTransactions"
    hide-actions
  ></TransactionsTable>
</template>

<script setup lang="ts">
import { Ref, ref, watch } from 'vue';
import User from '@/entities/User';
import TransactionApi from '@/api/TransactionApi';
import Transaction from '@/entities/Transaction';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';

const props = defineProps<{
  user: User;
  loading: boolean;
}>();

const loadingTransactions = ref<boolean>(true);
const transactions = ref<Transaction[]>([]) as Ref<Transaction[]>;

watch(
  () => props.user,
  (newUser: User) => {
    loadingTransactions.value = true;
    transactions.value = [];

    if (newUser.id) {
      fetchTransactionsOfUser(newUser.id);
    }
  },
);

const transactionApi = new TransactionApi();

const fetchTransactionsOfUser = async (userId: number) => {
  transactions.value = await transactionApi.getByUser(userId);
  loadingTransactions.value = false;
};
</script>

<style scoped></style>
