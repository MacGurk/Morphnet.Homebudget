<template>
  <TitleBar>Settlement</TitleBar>
  <SettlementOverview></SettlementOverview>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TitleBar from '@/components/common/TitleBar.vue';
import SettlementOverview from '@/components/settle/SettlementOverview.vue';

export default defineComponent({
  name: 'Settlement',
  components: { SettlementOverview, TitleBar },
});
</script>

<style scoped></style>
