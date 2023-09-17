<template>
  <v-sheet width="300" class="mt-6 mx-auto pa-4">
    <h4 class="mb-4">Change password</h4>
    <form @submit.prevent="submit">
      <v-text-field
        v-model="password.value.value"
        label="New Password"
        :error-messages="password.errorMessage.value as string"
      ></v-text-field>
      <v-text-field
        v-model="passwordConfirmation.value.value"
        label="New Password confirmation"
        :error-messages="passwordConfirmation.errorMessage.value as string"
      ></v-text-field>
      <div class="d-flex flex-column">
        <v-btn color="warning" type="submit">Set new password</v-btn>
      </div>
    </form>
  </v-sheet>
</template>

<script setup lang="ts">
import { useField, useForm } from 'vee-validate';
import UserForUpdatePassword from '@/models/UserForUpdatePassword';
import UserApi from '@/api/UserApi';
import { useRoute } from 'vue-router';
import router from '@/router';

const { handleSubmit } = useForm({
  validationSchema: {
    password(value: string) {
      if (value?.length > 8) return true;

      return 'Min 8 character';
    },
    passwordConfirmation(value: string) {
      if (value === password.value.value) return true;

      return 'Passwords must match';
    },
  },
});

const password = useField('password');
const passwordConfirmation = useField('passwordConfirmation');

const route = useRoute();
const userApi = new UserApi();

const submit = handleSubmit(async (values) => {
  const userPasswordUpdate = new UserForUpdatePassword(
    parseInt(route.params.id as string),
    values.password,
  );
  await userApi.updatePassword(userPasswordUpdate);
  await router.push('/users');
});
</script>

<style scoped></style>
