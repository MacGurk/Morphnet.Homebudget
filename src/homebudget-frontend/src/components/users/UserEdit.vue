<template>
  <TitleBar>Edit User {{ userName }}</TitleBar>
  <v-container class="w-33 rounded-xl mx-auto">
    <UserEditForm
      @update-display-user-name="updateDisplayUserName"
    ></UserEditForm>
    <PasswordEditForm></PasswordEditForm>
  </v-container>
</template>

<script setup lang="ts">
import TitleBar from '@/components/common/TitleBar.vue';
import { ref } from 'vue';
import UserEditForm from '@/components/users/UserEditForm.vue';
import PasswordEditForm from '@/components/users/PasswordEditForm.vue';

const userName = ref<string>('');

const updateDisplayUserName = (newUserName: string) => {
  userName.value = newUserName;
};
</script>

<style scoped></style>
