<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="4">
        <v-card class="mx-auto px-6 pb-8">
          <v-card-title>Login</v-card-title>
          <v-card-text>
            <form :validation-schema="schema" @submit.prevent="onSubmit">
              <v-text-field
                v-model="username.value.value"
                :error-messages="username.errorMessage.value as string"
                label="Username"
                required
              ></v-text-field>
              <v-text-field
                v-model="password.value.value"
                label="Password"
                :type="passwordVisible ? 'text' : 'password'"
                :error-messages="password.errorMessage.value as string"
                :append-inner-icon="passwordVisible ? 'mdi-eye-off' : 'mdi-eye'"
                required
                @click:append-inner="switchVisibility"
              ></v-text-field>
              <v-btn type="submit" size="large" color="primary" block>
                Sign In
              </v-btn>
              <v-alert
                v-if="errors.apiError"
                class="mt-6"
                :text="errors.apiError"
                type="error"
                variant="outlined"
              ></v-alert>
            </form>
            <div></div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { useField, useForm } from 'vee-validate';
import { ref } from 'vue';
import { object, string } from 'yup';
import { useAuthStore } from '@/stores/auth.store';
import { getErrorMessage } from '@/utils/utils';

const schema = object({
  username: string().required('Username is required'),
  password: string().required('Password is required'),
});

const { handleSubmit, errors, setErrors } = useForm({
  validationSchema: {
    username(value: string) {
      if (value?.length > 0) return true;

      return 'Enter a username';
    },
    password(value: string) {
      if (value?.length > 0) return true;

      return 'Enter a password';
    },
  },
});

const username = useField<string>('username');
const password = useField<string>('password');

const passwordVisible = ref(false);

const onSubmit = handleSubmit(async (values) => {
  const authStore = useAuthStore();
  try {
    await authStore.login(values.username, values.password);
  } catch (error) {
    setErrors({ apiError: getErrorMessage(error) });
  }
});

const switchVisibility = (): void => {
  passwordVisible.value = !passwordVisible.value;
};
</script>

<style scoped></style>
