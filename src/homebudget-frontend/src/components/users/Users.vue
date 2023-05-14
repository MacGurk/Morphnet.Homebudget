<template>
  <TitleBar title="Users"/>
  <CRUDTable title="User Overview" :items="users" :headers="headers" @delete-item="deleteUser" @edit-item="editUser">
    <template v-slot:createForm>
      <UserCreateForm @add-user="addUser" />
    </template>
  </CRUDTable>
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import CRUDTable, {CrudTableHeader} from '@/components/common/CRUDTable.vue';
import UserCreateForm from '@/components/users/UserCreateForm.vue';
import UserForCreation from '@/models/UserForCreation';

interface UsersData {
  users: User[];
  loading: boolean;
  headers: CrudTableHeader;
}

const userApi = new UserApi();

export default defineComponent({
  name: 'Users',
  data(): UsersData {
    return {
      users: [],
      loading: false,
      headers: [
        {title: 'Id', align: 'start', key: 'id', width: '5%'},
        {title: 'Name', align: 'start', key: 'name'},
        {title: 'E-Mail', align: 'start', key: 'email'},
        {title: 'Actions', align: 'start', key: 'actions', sortable: false, width: '10%'},
      ],
    };
  },
  components: {
    UserCreateForm,
    CRUDTable,
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
    editUser(id: number): void {
      // PUT API call
      console.log(`Parent edit: ${id}`);
    },
    async deleteUser(id: number): Promise<void> {
      if (await userApi.delete(id)) {
        this.users = this.users.filter(user => user.id !== id);
      }
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


    }
  },
});
</script>

<style scoped></style>
