<template>
  <TitleBar>Home</TitleBar>
  <div class="ma-4 text-center">
    <v-img
        class="ml-2"
        src="img/homebudget-logo.svg"
        max-height="200"
        contain
    />
    <h2>Welcome to HomeBudget</h2>
    <p>The home screen is currently under development.</p>
    <p>Maybe some statistics will be visible here in the future.</p>
    <TransactionYearLineGraph />
  </div>
</template>

<script>
import TitleBar from '@/components/common/TitleBar.vue';

export default {
  name: 'Home',
  components: { TitleBar },
};
</script>

<style scoped></style>
