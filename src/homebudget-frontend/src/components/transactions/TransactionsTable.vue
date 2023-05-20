<template>
  <div class="pa-16">
    <v-data-table
        :headers="headers"
        :items="$props.transactions"
        :items-per-page="10">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>{{ this.$props.title }}</v-toolbar-title>
          <v-divider
              class="mx-4"
              inset
              vertical
          ></v-divider>
          <v-spacer></v-spacer>
          <slot name="createForm"></slot>
        </v-toolbar>
      </template>
      <template v-slot:item.date="{ item }">
        {{ formatDate(item.raw.date) }}
      </template>
      <template v-slot:item.isSettled="{ item }">
        <v-icon v-if="item.raw.isSettled" size="small" color="success">
          mdi-check-circle-outline
        </v-icon>
        <v-icon v-else size="small" color="error">
          mdi-close-circle-outline
        </v-icon>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <CrudActions
            :itemId="item.raw.id"
            @edit-item="editTransaction"
            @delete-item="deleteTransaction"
        ></CrudActions>
      </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import {defineComponent, PropType} from 'vue';
import Transaction from '@/entities/Transaction';
import CrudActions from '@/components/common/CrudActions.vue';

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
  components: {CrudActions},
  props: {
    title: {type: String, required: true},
    transactions: {type: Object as PropType<Transaction[]>, required: true},
    hideActions: {type: Boolean, default: false}
  },
  created() {
    if (!this.$props.hideActions) {
      this.headers.push({title: 'Actions', align: 'start', key: 'actions', sortable: false, width: '10%'})
    }
  },
  data(): TransactionsTableData {
    return {
      loading: false,
      headers: [
        {title: 'Id', align: 'start', key: 'id', width: '5%'},
        {title: 'Date', align: 'start', key: 'date', width: '5%'},
        {title: 'Description', align: 'start', key: 'description'},
        {title: 'User', align: 'start', key: 'user.name'},
        {title: 'Price', align: 'start', key: 'price'},
        {title: 'Settled', align:'start', key: 'isSettled'},
      ],
    }
  },
  emits: ['editTransaction', 'deleteTransaction'],
  methods: {
    editTransaction(id: number) {
      this.$emit('editTransaction', id);
    },
    deleteTransaction(id: number) {
      this.$emit('deleteTransaction', id);
    },
    formatDate(date: string): string {
      return new Date(date).toLocaleDateString();
    }
  }
})
</script>

<style scoped>

</style>