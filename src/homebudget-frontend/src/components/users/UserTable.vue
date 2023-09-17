<template>
  <div class="pa-16">
    <v-data-table
      :headers="headers"
      :items="$props.users"
      :items-per-page="10"
      :loading="props.loading"
    >
      <template #top>
        <v-toolbar flat>
          <v-toolbar-title>User Overview</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <UserCreateForm @add-user="addUser"></UserCreateForm>
        </v-toolbar>
      </template>
      <template #[`item.name`]="{ item }">
        <v-list-item :to="`/users/${item.raw.id}`">{{
          item.raw.name
        }}</v-list-item>
      </template>
      <template #item.isContributor="{ item }">
        <v-icon v-if="item.raw.isContributor" size="small" color="success">
          mdi-check-circle-outline
        </v-icon>
        <v-icon v-else size="small" color="error">
          mdi-close-circle-outline
        </v-icon>
      </template>
      <template #[`item.actions`]="{ item }">
        <CrudActions
          :item-id="item.raw.id"
          @edit-item="editUser"
          @delete-item="deleteUser"
        ></CrudActions>
      </template>
    </v-data-table>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import User from '@/entities/User';
import UserCreateForm from '@/components/users/UserCreateForm.vue';
import UserForCreation from '@/models/UserForCreation';
import CrudActions from '@/components/common/CrudActions.vue';
import router from '@/router';

interface UserTableHeader {
  title: string;
  align?: string;
  key: string;
  sortable?: boolean;
  width?: string;
}

const props = withDefaults(
  defineProps<{
    users: User[];
    loading: boolean;
  }>(),
  {
    loading: false,
  },
);

const emits = defineEmits<{
  (_e: 'deleteUser', _id: number): true;
  (_e: 'addUser', _userToCreate: UserForCreation): true;
}>();

const headers = ref<UserTableHeader[]>([
  { title: 'Id', align: 'start', key: 'id', width: '5%' },
  { title: 'Name', align: 'start', key: 'name' },
  { title: 'E-Mail', align: 'start', key: 'email' },
  { title: 'Contributor', align: 'start', key: 'isContributor' },
  {
    title: 'Actions',
    align: 'start',
    key: 'actions',
    sortable: false,
    width: '10%',
  },
]);

const editUser = (id: number) => {
  router.push(`/users/${id}/edit`);
};

const deleteUser = (id: number) => {
  emits('deleteUser', id);
};

const addUser = (newUser: UserForCreation) => {
  emits('addUser', newUser);
};
</script>

<style scoped></style>
