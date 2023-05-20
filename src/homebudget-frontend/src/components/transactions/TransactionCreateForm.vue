<template>
  <v-dialog v-model="createDialog" max-width="512px" @before-enter="openDialog">
    <template v-slot:activator="{ props }">
      <v-btn color="primary" class="mb-2" v-bind="props" @click="openDialog">Create Transaction</v-btn>
    </template>
    <v-card>
      <form>
        <v-card-title class="bg-primary">New Transaction</v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col
                  cols="12"
                  sm="6"
                  md="12"
              >
                <v-text-field
                    v-model="newTransaction.date"
                    label="Date"
                    type="date"
                >
                </v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col
                  cols="12"
                  sm="6"
                  md="12"
              >
                <v-textarea
                    v-model="newTransaction.description"
                    label="Description"
                    clearable
                >
                </v-textarea>
              </v-col>
            </v-row>
            <v-row>
              <v-col
                  cols="12"
                  sm="6"
                  md="12"
              >
                <v-text-field
                    v-model="newTransaction.price"
                    label="Price"
                    hint="Round price to the nearest whole unit"
                    type="number"
                    min="1"
                >
                </v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col
                  cols="12"
                  sm="6"
                  md="12"
              >
                <v-select
                    v-model="newTransaction.userId"
                    label="User"
                    :items="$props.users"
                    item-title="name"
                    item-value="id"
                >
                  <template v-slot:loader>
                    <v-progress-linear
                        :active="loadingUsers"
                        indeterminate
                        absolute
                    ></v-progress-linear>
                  </template>
                </v-select>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
              color="secondary"
              variant="text"
              @click="closeDialog"
          >
            Cancel
          </v-btn>
          <v-btn
              color="primary"
              variant="flat"
              @click="addTransaction"
          >
            Save
          </v-btn>
        </v-card-actions>
      </form>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { getDateString } from '@/utils/utils';
import {defineComponent} from 'vue';
import User from '@/entities/User';
import TransactionForCreation from '@/models/TransactionForCreation';

interface TransactionCreateFormData {
  createDialog: boolean;
  loadingUsers: boolean;
  newTransaction: {
    date: string,
    userId: number | null,
    description: string,
    price: number
  };
}

export default defineComponent({
  name: 'TransactionCreateForm',
  props: {
    users: { type: Array as () => User[], required: true}
  },
  data(): TransactionCreateFormData {
    return {
      createDialog: false,
      loadingUsers: false,
      newTransaction: {
        date: getDateString(new Date()),
        userId: null,
        description: '',
        price: 0
      },
    };
  },
  emits: {
    addTransaction: (_newTransaction: TransactionForCreation) => true,
  },
  methods: {
    closeDialog() {
      this.createDialog = false;
      this.newTransaction.date = getDateString(new Date());
      this.newTransaction.description = '';
      this.newTransaction.price = 0;
      this.newTransaction.userId = null;
    },
    async openDialog() {
      try {
        this.loadingUsers = true;
      } catch (e) {
        console.error('Error:', e)
      } finally {
        this.loadingUsers = false;
      }
    },
    addTransaction() {
      if (this.newTransaction.userId === null) {
        return;
      }
      this.createDialog = false;
      const transaction = new TransactionForCreation();
      transaction.date = new Date(this.newTransaction.date);
      transaction.description = this.newTransaction.description;
      transaction.price = this.newTransaction.price;
      transaction.userId = this.newTransaction.userId;
      this.$emit('addTransaction', transaction);
    }
  }
})
</script>

<style scoped>

</style>