<template>
  <div class="ma-16">
    <div class="d-flex flex-wrap">
      <SettlementCard
        v-for="settlement in settlements"
        :key="settlement.user.id"
        :settlement="settlement"
        @click="handleSettlementChange(settlement.user.id)"
      ></SettlementCard>
    </div>
    <v-btn color="primary" @click="settleTransactions">Settle all</v-btn>
  </div>
  <TransactionsTable
    v-if="selectedSettlement !== undefined"
    :title="`Transactions of ${(selectedSettlement as Settlement).user.name}`"
    :transactions="selectedSettlement.transactions"
    hide-actions
  ></TransactionsTable>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TransactionApi from '@/api/TransactionApi';
import Settlement from '@/entities/Settlement';
import SettlementCard from '@/components/settle/SettlementCard.vue';
import TransactionsTable from '@/components/transactions/TransactionsTable.vue';
import Transaction from '@/entities/Transaction';
import router from '@/router';

interface SettlementOverviewData {
  settlements: Settlement[];
  selectedSettlement: Settlement | undefined;
  transactions: Transaction[];
}

const transactionApi = new TransactionApi();

export default defineComponent({
  name: 'SettlementOverview',
  components: { TransactionsTable, SettlementCard },
  data(): SettlementOverviewData {
    return {
      selectedSettlement: undefined,
      settlements: [],
      transactions: [],
    };
  },
  computed: {
    Settlement() {
      return Settlement;
    },
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
      this.selectedSettlement = this.settlements.find(
        (x) => x.user.id === userId,
      ) as Settlement;
    },
    settleTransactions() {
      let transactionIds: number[] = [];
      this.settlements.forEach((settlement) => {
        settlement.transactions.forEach((transaction) =>
          transactionIds.push(transaction.id),
        );
      });
      transactionApi.settleTransactions(transactionIds);
      router.push('/');
    },
  },
});
</script>

<style scoped></style>
