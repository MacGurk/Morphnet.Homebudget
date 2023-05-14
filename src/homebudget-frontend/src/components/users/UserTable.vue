<template>
  <div class="pa-16">
    <v-data-table :headers="headers" :items="$props.users" :items-per-page="10">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>User Overview</v-toolbar-title>
          <v-divider
              class="mx-4"
              inset
              vertical
          ></v-divider>
          <v-spacer></v-spacer>
          <v-dialog v-model="createDialog" max-width="512px">
            <template v-slot:activator="{ props }">
              <v-btn color="primary" class="mb-2" v-bind="props">Create User</v-btn>
            </template>
            <v-card>
              <v-card-title>New User</v-card-title>
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                          v-model="newUser.name"
                          label="Name"
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
                      <v-text-field
                          v-model="newUser.email"
                          label="E-Mail"
                          type="email"
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
                      <v-text-field
                          v-model="newUser.password"
                          label="Password"
                          :type="showPassword ? 'text' : 'password'"
                          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                          @click:append="showPassword = !showPassword"
                          :rules="[rules.minLength]"
                          clearable
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
                      <v-text-field
                          v-model="newUser.passwordConfirm"
                          label="Confirm password"
                          :type="showPassword ? 'text' : 'password'"
                          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                          @click:append="showPassword = !showPassword"
                          :error-messages="passwordMatchError"
                          clearable
                      >
                      </v-text-field>
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
                  variant="text"
                  @click="addUser"
                >
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon size="small" class="mr-2" @click="editUser(item.raw.id)"
        >mdi-pencil
        </v-icon
        >
        <v-icon size="small" @click="deleteUser(item.raw.id)"
        >mdi-delete
        </v-icon
        >
      </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import {defineComponent, PropType} from 'vue';
import User from '@/entities/User';

interface UserTableData {
  loading: boolean;
  createDialog: boolean;
  headers: {
    title: string,
    align?: string,
    key: string,
    sortable?: boolean,
    width?: string,
  }[];
  newUser: {
    name: string,
    email: string,
    password: string,
    passwordConfirm: string
  };
  showPassword: boolean;
  rules: Object
}

export default defineComponent({
  name: 'UserTable',
  props: {
    users: {type: Object as PropType<User[]>, required: true},
  },
  data(): UserTableData {
    return {
      loading: false,
      createDialog: false,
      headers: [
        {title: 'Id', align: 'start', key: 'id', width: '5%'},
        {title: 'Name', align: 'start', key: 'name'},
        {title: 'E-Mail', align: 'start', key: 'email'},
        {title: 'Actions', align: 'start', key: 'actions', sortable: false, width: '10%'},
      ],
      showPassword: false,
      newUser: {
        name: '',
        email: '',
        password: '',
        passwordConfirm: ''
      },
      rules: {
        minLength: (v: string) => v.length >= 8 || 'Min 8 characters'
      }
    };
  },
  emits: {
    editUser: null,
    deleteUser: null,
    addUser: null,
  },
  methods: {
    editUser(id: number) {
      this.$emit('editUser', id);
      console.log(`edit ${id}`);
    },
    deleteUser(id: number) {
      this.$emit('deleteUser', id);
      console.log(`delete ${id}`);
    },
    closeDialog() {
      this.createDialog = false;
      this.newUser.name = '';
      this.newUser.email = '';
      this.newUser.password = '';
    },
    addUser() {
      this.createDialog = false;
      this.$emit('addUser', this.newUser);
    }
  },
  computed: {
  passwordMatchError () {
    return (this.newUser.password === this.newUser.passwordConfirm) ? '' : 'Password must match';
  }
    
  }
});
</script>

<style scoped></style>
