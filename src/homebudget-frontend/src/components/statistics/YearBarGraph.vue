<script setup lang="ts">
import {onMounted, ref, shallowRef, nextTick, watch } from 'vue';
import {Bar} from 'vue-chartjs';
import {BarElement, CategoryScale, Chart as ChartJS, ChartData, Legend, LinearScale, Title, Tooltip, Colors} from 'chart.js'
import TransactionApi from "@/api/TransactionApi";

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, Colors);

const dummyData = {
  labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
  datasets: [
    {
      label: 'Loading',
      data: [65, 59, 80, 81, 56, 55, 40, 20, 30, 40, 50, 60],
      backgroundColor: '#4d4c4c'
    }
  ]
};

const data = shallowRef<ChartData | null>(null);
const loaded = ref(false);
const options = {
  responsive: true,
  maintainAspectRatio: true,
  scales: {
    x: {
      stacked: true,
    },
    y: {
      stacked: true,
    }
  }
}

const year = ref(new Date().getFullYear());

const transactionApi = new TransactionApi();
const getStatistics = async () => {
  loaded.value = false;
  const statistics = await transactionApi.getStatisticsByYear(year.value);
  data.value = {
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    datasets: statistics.map((statistic) => {
      return {
        label: statistic.user.name,
        data: statistic.monthlyTotal,
      }
    })
  };
  loaded.value = true;
}

watch(year, () => {
  getStatistics();
});

onMounted(async () => {
  await getStatistics();
});

</script>

<template>
  <div class="d-flex justify-center align-center">
    <v-icon size="large" @click="year--"> mdi-chevron-double-left </v-icon>
    <div class="mx-3 year-text text-center">
    <div class="text-h6">
        {{ year }}
      </div>
    </div>
    <v-icon size="large" @click="year++"> mdi-chevron-double-right </v-icon>
  </div>
  <div class="container ma-16">
    <Bar v-if="loaded" :data="data" :options="options" />
    <Bar v-else :data="dummyData" :options="options" />
  </div>
</template>

<style scoped>

</style>