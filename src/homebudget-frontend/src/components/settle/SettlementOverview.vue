<template>
  <div class="d-flex flex-wrap">
    <SettlementCard v-for="settlement in this.settlements" v-bind:key="settlement.user.id" :settlement="settlement" @click="handleSettlementChange(settlement.user.id)"></SettlementCard>
    
  </div>
  <TransactionsTable :title="`Transactions of ${selectedSettlement.user.name}`" :transactions="this.selectedSettlement.transactions" hide-actions></TransactionsTable>
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import User from '@/entities/User';
import TransactionApi from '@/api/TransactionApi';
import Settlement from '@/entities/Settlement';
import SettlementCard from '@/components/settle/SettlementCard.vue';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import Transaction from '@/entities/Transaction';

interface SettlementOverviewData {
  settlements: Settlement[];
  selectedSettlement: Settlement;
  transactions: Transaction[];
}

const transactionApi = new TransactionApi();

export default defineComponent({
  name: 'SettlementOverview',
  components: {TransactionsTable, SettlementCard},
  data(): SettlementOverviewData {
    return {
      selectedSettlement: new Settlement(),
      settlements: [],
      transactions: []
    }
  },
  created() {
    this.fetchSettlements();
  },
  methods: {
    async fetchSettlements() {
      this.settlements = await transactionApi.getSettlements();
      this.selectedSettlement = this.settlements[0];
    },
    handleSettlementChange(userId: number) {
      this.selectedSettlement = this.settlements.find(x => x.user.id === userId) as Settlement;
    }
  }
})
</script>

<style scoped>

</style>