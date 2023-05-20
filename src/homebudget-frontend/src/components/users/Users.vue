<template>
  <TitleBar title="Users"/>
  <UserTable 
      :users="users"
      @delete-user="deleteUser"
      @edit-user="editUser"
  ></UserTable>
  <ConfirmDialog ref="confirm" />
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import UserForCreation from '@/models/UserForCreation';
import ConfirmDialog from '@/components/common/ConfirmDialog.vue';
import UserTable from '@/components/users/UserTable.vue';

interface UsersData {
  users: User[];
  loading: boolean;
}

const userApi = new UserApi();

export default defineComponent({
  name: 'Users',
  data(): UsersData {
    return {
      users: [],
      loading: false
    };
  },
  components: {
    UserTable,
    ConfirmDialog,
    TitleBar
  },
  created() {
    this.fetchUsers();
  },
  methods: {
    async fetchUsers(): Promise<void> {
      this.users = [];

      this.users = await userApi.get();
    },
    async addUser(newUser: UserForCreation): Promise<void> {
      this.loading = true;
      try {
        const user = await userApi.add(newUser);
        this.users.push(user);
      } catch (e) {
        console.error(e);
      } finally {
        this.loading = false;
      }
    },
    editUser(id: number): void {
      // PUT API call
      console.log(`Edit user: ${id}`);
    },
    async deleteUser(id: number): Promise<void> {
      const deleteUser = this.users.find(u => u.id === id);
      if (!deleteUser) {
        return;
      }
      if (await this.$refs.confirm.open(
          `Delete user ${deleteUser.name}`,
          `Do you really want to delete the user '${deleteUser.name}'?`,
          'Delete',
          'error'
      )) {
        if (await userApi.delete(id)) {
          this.users = this.users.filter(user => user.id !== id);
        }
      }
    }
  },
});
</script>

<style scoped></style>
