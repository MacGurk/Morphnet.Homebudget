<script setup lang="ts">
import {inject, onBeforeMount, ref} from 'vue';
import CrudActions from '@/components/common/CrudActions.vue';
import router from '@/router';
import {FilterSymbol} from "@/components/transactions/filters";
import ConfirmDialog from "@/components/common/ConfirmDialog.vue";
import TransactionCreateForm from "@/components/transactions/TransactionCreateForm.vue";
import TransactionsTable from "@/components/transactions/TransactionsTable.vue";

const context = inject(FilterSymbol);
if (!context) {
  throw new Error('Not child of a FilterContext');
}

const { transactions, loading, deleteTransaction, addTransaction } = context;

const confirm = ref<InstanceType<typeof ConfirmDialog> | null>(null);

const editTransaction = (id: number) => {
  router.push(`/transactions/${id}/edit`);
};

const deleteAction = async (id: number) => {
  if (
      await confirm.value?.open(
          `Delete user ${id}`,
          `Do you really want to delete the transaction '${id}'?`,
          'Delete',
          'error',
      )
  ) {
    await deleteTransaction(id);
  }
};

</script>

<template>
  <TransactionsTable title="Transactions Overview" :transactions="transactions" :loading="loading">
    <template #create>
      <TransactionCreateForm @add-transaction="addTransaction" />
    </template>
    <template #actions="{ item }">
      <CrudActions
          :item-id="item.id"
          @edit-item="editTransaction"
          @delete-item="deleteAction"
      ></CrudActions>
    </template>
  </TransactionsTable>
  <ConfirmDialog ref="confirm" />
</template>

<style scoped>

</style>