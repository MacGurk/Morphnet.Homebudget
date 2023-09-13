<template>
  <TitleBar>{{ `User ${user.name}` }}</TitleBar>
  <UserDetail :user="user" :loading="loading"></UserDetail>
</template>

<script setup lang="ts">
import TitleBar from '@/components/common/TitleBar.vue';
import UserDetail from '@/components/users/UserDetail.vue';
import User from '@/entities/User';
import UserApi from '@/api/UserApi';
import { onMounted, Ref, ref } from 'vue';
import { useRoute } from 'vue-router';

const userApi = new UserApi();
const route = useRoute();

const user = ref<User>(new User()) as Ref<User>;
const loading = ref<boolean>(true);

const fetchUser = async () => {
  user.value = new User();

  const id = Array.isArray(route.params.id)
    ? route.params.id[0]
    : route.params.id;
  user.value = await userApi.getById(parseInt(id));

  loading.value = false;
};

onMounted(() => {
  fetchUser();
});
</script>

<style scoped></style>
