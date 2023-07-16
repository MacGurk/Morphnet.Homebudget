<template>
  <v-card width="200" class="mr-4 mb-4">
    <v-card-title>{{ this.$props.settlement.user.name }}</v-card-title>
    <v-card-text>
      <div v-if="this.$props.settlement.receives">
        <v-icon color="success">mdi-hand-extended</v-icon>
        <v-icon color="success">mdi-arrow-left-thick</v-icon>
        <v-icon color="success">mdi-cash</v-icon>
        {{ this.$props.settlement.amount }} CHF
      </div>
      <div v-else>
        <v-icon color="error">mdi-hand-extended</v-icon>
        <v-icon color="error">mdi-arrow-right-thick</v-icon>
        <v-icon color="error">mdi-cash</v-icon>
        {{ this.$props.settlement.amount }} CHF
      </div>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import Settlement from '@/entities/Settlement';

export default defineComponent({
  name: 'SettlementCard',
  props: {
    settlement: { type: Object as PropType<Settlement>, required: true },
  },
});
</script>

<style scoped></style>
