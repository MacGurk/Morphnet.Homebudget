<template>
  <TitleBar :title="`User ${this.user.name}`" />
  <v-progress-circular v-if="loading" indeterminate></v-progress-circular>
  <UserDetail v-else :user="user"></UserDetail>
</template>

<script lang="ts">
import TitleBar from '@/components/common/TitleBar.vue';
import UserDetail from '@/components/users/UserDetail.vue';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import {defineComponent} from 'vue';

interface UserData {
  user: User;
  loading: boolean;
}

const userApi = new UserApi();

export default defineComponent({
  name: 'User',
  components: {UserDetail, TitleBar},
  data(): UserData {
    return {
      user: new User(),
      loading: true,
    }
  },
  created() {
    this.fetchUser();
  },
  methods: {
    async fetchUser() {
      this.user = new User;
      
      const id = Array.isArray(this.$route.params.id) ? this.$route.params.id[0] : this.$route.params.id;
      this.user = await userApi.getById(parseInt(id));
      
      this.loading = false;
    }
  }
});
</script>

<style scoped></style>
