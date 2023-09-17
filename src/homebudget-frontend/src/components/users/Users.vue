<template>
  <TitleBar>Users</TitleBar>
  <UserTable
    :users="users"
    @add-user="addUser"
    @delete-user="deleteUser"
    @edit-user="editUser"
  ></UserTable>
  <ConfirmDialog ref="confirm" />
</template>

<script setup lang="ts">
import { onMounted, Ref, ref } from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import UserForCreation from '@/models/UserForCreation';
import ConfirmDialog from '@/components/common/ConfirmDialog.vue';
import UserTable from '@/components/users/UserTable.vue';

const users = ref<User[]>([]) as Ref<User[]>;
const loading = ref<boolean>(false);

const confirm = ref<InstanceType<typeof ConfirmDialog> | null>(null);

onMounted(() => {
  fetchUsers();
});

const userApi = new UserApi();

const fetchUsers = async () => {
  users.value = [];

  users.value = await userApi.get();
};

const addUser = async (newUser: UserForCreation) => {
  loading.value = true;
  try {
    const user = await userApi.add(newUser);
    users.value.push(user);
  } catch (e) {
    console.error(e);
  } finally {
    loading.value = false;
  }
};

const editUser = (id: number) => {
  // PUT API call
  console.log(`Edit user: ${id}`);
};

const deleteUser = async (id: number) => {
  const deleteUser = users.value.find((u) => u.id === id);
  if (!deleteUser) {
    return;
  }
  if (
    await confirm.value?.open(
      `Delete user ${deleteUser.name}`,
      `Do you really want to delete the user '${deleteUser.name}'?`,
      'Delete',
      'error',
    )
  ) {
    try {
      await userApi.delete(id);
      users.value = users.value.filter((user) => user.id !== id);
    } catch (e) {
      console.error(e);
    }
  }
};
</script>

<style scoped></style>
