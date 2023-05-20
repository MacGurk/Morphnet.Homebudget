<template>
  <v-container>
    <v-row>
      <v-col>
        Id:
      </v-col>
      <v-col>
        {{ user.id }}
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        Name:
      </v-col>
      <v-col>
        {{ user.name }}
      </v-col>
    </v-row>
  </v-container>
  <TransactionsTable
      :title="`Transactions of user ${user.id}`"
      :transactions="transactions"
      hide-actions
  ></TransactionsTable>
</template>

<script lang="ts">
import {defineComponent, PropType, reactive, toRefs, watch} from 'vue';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import User from '@/entities/User';
import TransactionApi from '@/api/TransactionApi';
import Transaction from '@/entities/Transaction';

interface UserDetailState {
  transactions: Transaction[];
  userLoaded: boolean;
}

const transactionApi = new TransactionApi();
export default defineComponent({
  name: 'UserDetail',
  components: {TransactionsTable},
  props: {
    user: {type: Object as PropType<User>, required: true}
  },
  setup(props) {
    const state: UserDetailState = reactive({
      userLoaded: false,
      transactions: []
    });
    
    watch(() => props.user, () => {
      state.userLoaded = false;
      state.transactions = [];
      
      if (props.user.id) {
        fetchTransactionsOfUser(props.user.id);
      }
    }, {immediate: true});
    
    async function fetchTransactionsOfUser(userId: number) {
      state.transactions = await transactionApi.getByUser(userId);
      state.userLoaded = true;
    }
    
    return {
      ...toRefs(state)
    }
  }
});
</script>

<style scoped>

</style>