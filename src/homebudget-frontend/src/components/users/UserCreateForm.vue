<template>
  <v-dialog v-model="createDialog" max-width="512px">
    <template #activator="{ props }">
      <v-btn color="primary" class="mb-2" v-bind="props">Create User</v-btn>
    </template>
    <v-card>
      <v-card-title class="bg-primary">New User</v-card-title>
      <v-card-text>
        <v-container>
          <v-row>
            <v-col cols="12" sm="6" md="12">
              <v-text-field v-model="newUser.name" label="Name"> </v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" sm="6" md="12">
              <v-text-field v-model="newUser.email" label="E-Mail" type="email">
              </v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-checkbox v-model="newUser.isContributor">
                <template #label>
                  Contributor&nbsp;
                  <v-tooltip
                    text="User contributes to the budget and is involved in the settlement"
                  >
                    <template #activator="{ props }">
                      <v-icon v-bind="props">mdi-help-circle-outline</v-icon>
                    </template>
                  </v-tooltip>
                </template>
              </v-checkbox>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                v-model="newUser.password"
                label="Password"
                :type="showPassword ? 'text' : 'password'"
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                :rules="[validationRules.minLength]"
                clearable
                @click:append="showPassword = !showPassword"
              >
              </v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                v-model="newUser.passwordConfirm"
                label="Confirm password"
                :type="showPassword ? 'text' : 'password'"
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                :error-messages="passwordMatchError"
                clearable
                @click:append="showPassword = !showPassword"
              >
              </v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="secondary" variant="text" @click="closeDialog">
          Cancel
        </v-btn>
        <v-btn color="primary" variant="flat" @click="addUser"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import UserForCreation from '@/models/UserForCreation';

interface UserCreateForm {
  name: string;
  email: string;
  isContributor: boolean;
  password: string;
  passwordConfirm: string;
}

const createDialog = ref<boolean>(false);
const newUser = ref<UserCreateForm>({
  email: '',
  name: '',
  isContributor: false,
  password: '',
  passwordConfirm: '',
});
const showPassword = ref<boolean>(false);

const validationRules = ref({
  minLength: (v: string) => v.length >= 8 || 'Min 8 characters',
});

const emit = defineEmits<{
  (_e: 'addUser', _newUser: UserForCreation): void;
}>();

const passwordMatchError = computed(() =>
  newUser.value.password === newUser.value.passwordConfirm
    ? ''
    : 'Password must match',
);

const closeDialog = () => {
  createDialog.value = false;
  newUser.value.name = '';
  newUser.value.email = '';
  newUser.value.isContributor = false;
  newUser.value.password = '';
  newUser.value.passwordConfirm = '';
};

const addUser = () => {
  createDialog.value = false;
  const user = new UserForCreation();
  user.name = newUser.value.name;
  user.email = newUser.value.email;
  user.password = newUser.value.password;
  user.isContributor = newUser.value.isContributor;
  emit('addUser', user);
};
</script>

<style scoped></style>
