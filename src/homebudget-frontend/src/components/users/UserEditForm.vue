<template>
  <v-sheet width="300" class="mx-auto pa-4">
    <h4 class="mb-4">Edit user information</h4>
    <form @submit.prevent="submitUser">
      <v-text-field
        v-model="name.value.value"
        label="Name"
        :error-messages="name.errorMessage.value as string"
        required
      ></v-text-field>
      <v-text-field
        v-model="email.value.value"
        label="E-Mail"
        :error-messages="email.errorMessage.value as string"
        required
      >
      </v-text-field>
      <v-checkbox
        v-model="isContributor.value.value"
        label="Contributor"
      ></v-checkbox>
      <div class="d-flex flex-column">
        <v-btn color="success" type="submit">Save changes</v-btn>
      </div>
    </form>
  </v-sheet>
</template>
<script setup lang="ts">
import { useField, useForm } from 'vee-validate';
import { onMounted } from 'vue';
import UserApi from '@/api/UserApi';
import { useRoute } from 'vue-router';
import User from '@/entities/User';
import router from '@/router';

const { handleSubmit } = useForm({
  validationSchema: {
    name(value: string) {
      if (value?.length > 0) return true;

      return 'Enter a name';
    },
    email(value: string) {
      if (value?.length > 0) return true;

      return 'Enter an e-mail address';
    },
  },
});

const emit = defineEmits({
  updateDisplayUserName: (_userName: string) => true,
});

const name = useField('name');
const email = useField('email');
const isContributor = useField('isContributor');

onMounted(() => {
  fetchUser();
});

const userApi = new UserApi();
const route = useRoute();

const fetchUser = async () => {
  const userId = parseInt(route.params.id as string);
  const user = await userApi.getById(userId);
  name.value.value = user.name;
  email.value.value = user.email;
  isContributor.value.value = user.isContributor;

  emit('updateDisplayUserName', user.name);
};

const submitUser = handleSubmit(async (values) => {
  const updatedUser: User = new User(
    parseInt(route.params.id as string),
    values.name,
    values.email,
    values.isContributor,
  );
  await userApi.update(updatedUser);
  await router.push('/users');
});
</script>

<style scoped></style>
