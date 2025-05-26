<template>
  <TitleBar>Home</TitleBar>
  <div class="ma-4 text-center">
    <v-container>
      <v-row>
        <v-col cols="12" lg="6">
          <HomeCard title="Year statistic">
            <YearBarGraph />
          </HomeCard>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script lang="ts">
import TitleBar from '@/components/common/TitleBar.vue';
import HomeCard from '@/components/Home/HomeCard.vue';
import YearBarGraph from '@/components/statistics/YearBarGraph.vue';

export default {
  name: 'BarChart',
  components: { YearBarGraph, HomeCard, TitleBar },
};
</script>

<style scoped></style>
