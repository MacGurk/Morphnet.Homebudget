<template>
  <TitleBar>Edit transaction {{ transactionName }}</TitleBar>
  <v-container class="w-33 rounded-xl mx-auto">
    <v-sheet width="300" class="mx-auto pa-4">
      <h4 class="mb-4">Edit transaction information</h4>
      <form @submit.prevent="submit">
        <v-text-field v-model="date.value.value" label="Date" type="date">
        </v-text-field>
        <v-textarea
          v-model="description.value.value"
          label="Description"
          :error-messages="description.errorMessage.value as string"
        >
        </v-textarea>
        <v-text-field
          v-model="price.value.value"
          label="Price"
          hint="Round price to the nearest whole unit"
          type="number"
          min="1"
          :error-messages="price.errorMessage.value as string"
        ></v-text-field>
        <v-select
          v-model="userId.value.value"
          label="User"
          :items="users"
          item-title="name"
          item-value="id"
        >
          <template #loader>
            <v-progress-linear
              :active="loadingUsers"
              indeterminate
              absolute
            ></v-progress-linear>
          </template>
        </v-select>
        <v-checkbox
          v-model="isSettled.value.value"
          label="Is settled"
        ></v-checkbox>
        <div class="d-flex flex-column">
          <v-btn color="success" type="submit">Save changes</v-btn>
        </div>
      </form>
    </v-sheet>
  </v-container>
</template>

<script setup lang="ts">
import TitleBar from '@/components/common/TitleBar.vue';
import { useRoute } from 'vue-router';
import { onMounted, ref } from 'vue';
import TransactionApi from '@/api/TransactionApi';
import { useField, useForm } from 'vee-validate';
import User from '@/entities/User';
import { getDateString } from '@/utils/utils';
import UserApi from '@/api/UserApi';
import Transaction from '@/entities/Transaction';
import router from '@/router';

const transactionName = ref<string>('');
const users = ref<User[]>([]);
const loadingUsers = ref<boolean>(false);

const { handleSubmit } = useForm({
  validationSchema: {
    description(value: string) {
      if (value?.length > 0) return true;

      return 'Enter a description';
    },
    price(value: number) {
      if (value > 0) return true;

      return 'Enter a valid price';
    },
  },
});

const submit = handleSubmit(async (values) => {
  const transaction = new Transaction(
    parseInt(route.params.id as string),
    values.date,
    users.value.find((user) => user.id === values.userId) as User,
    values.description,
    values.price,
    values.isSettled,
  );
  try {
    await transactionApi.update(transaction);
  } catch (e) {
    console.error(e);
  }
  router.push('/transactions');
});

const date = useField('date');
const description = useField('description');
const price = useField('price');
const isSettled = useField('isSettled');
const userId = useField('userId');

const transactionApi = new TransactionApi();
const userApi = new UserApi();
const route = useRoute();

onMounted(() => {
  fetchTransaction();
  fetchUsers();
});

const fetchUsers = async () => {
  try {
    loadingUsers.value = true;
    users.value = await userApi.getContributors();
  } catch (e) {
    console.error('Error:', e);
  } finally {
    loadingUsers.value = false;
  }
};
const fetchTransaction = async () => {
  const transactionId = parseInt(route.params.id as string);
  const transaction = await transactionApi.getById(transactionId);
  transactionName.value = transaction.id.toString();
  date.value.value = transaction.date;
  description.value.value = transaction.description;
  price.value.value = transaction.price;
  isSettled.value.value = transaction.isSettled;
  userId.value.value = transaction.user.id;
};
</script>

<style scoped></style>
