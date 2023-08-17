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
                :rules="[rules.minLength]"
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

<script lang="ts">
import { defineComponent } from 'vue';
import UserForCreation from '@/models/UserForCreation';

interface UserCreateFormData {
  createDialog: boolean;
  newUser: {
    name: string;
    email: string;
    isContributor: boolean;
    password: string;
    passwordConfirm: string;
  };
  showPassword: boolean;
  rules: Object;
}

export default defineComponent({
  name: 'UserCreateForm',
  emits: {
    addUser: (_newUser: UserForCreation) => true,
  },
  data(): UserCreateFormData {
    return {
      createDialog: false,
      newUser: {
        email: '',
        name: '',
        isContributor: false,
        password: '',
        passwordConfirm: '',
      },
      showPassword: false,
      rules: {
        minLength: (v: string) => v.length >= 8 || 'Min 8 characters',
      },
    };
  },
  computed: {
    passwordMatchError() {
      return this.newUser.password === this.newUser.passwordConfirm
        ? ''
        : 'Password must match';
    },
  },
  methods: {
    closeDialog() {
      this.createDialog = false;
      this.newUser.name = '';
      this.newUser.email = '';
      this.newUser.isContributor = false;
      this.newUser.password = '';
    },
    addUser() {
      this.createDialog = false;
      const user = new UserForCreation();
      user.name = this.newUser.name;
      user.email = this.newUser.email;
      user.password = this.newUser.password;
      user.isContributor = this.newUser.isContributor;
      this.$emit('addUser', user);
    },
  },
});
</script>

<style scoped></style>
