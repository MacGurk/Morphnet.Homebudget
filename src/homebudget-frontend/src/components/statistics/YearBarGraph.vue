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

const colors = ['#ed8796', '#8aadf4', '#f5a97f', '#a6da95', '#eed49f', '#c6a0f6', '#91d7e3', '#f4dbd6']

const data = shallowRef<ChartData | null>(null);
const loaded = ref(false);
const options = {
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    x: {
      stacked: true,
      title: {
        display: true,
        text: "Month"
      },
      border: {
        display: false,
      },
      ticks: {
        color: '#718096'
      },
      grid: {
        display: false
      }
    },
    y: {
      stacked: true,
      title: {
        display: true,
        text: "in CHF"
      },
      border: {
        display: false,
      },
      ticks: {
        color: '#718096',
      },
      grid: {
        color: '#EDF2F7'
      }
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
    datasets: statistics.map((statistic, index) => {
      return {
        label: statistic.user.name,
        data: statistic.monthlyTotal,
        backgroundColor: colors[index % colors.length]
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
    <div class="text-h5 mx-3">
        {{ year }}
    </div>
    <v-icon size="large" @click="year++"> mdi-chevron-double-right </v-icon>
  </div>
  <div class="chart-container">
    <Bar v-if="loaded" :data="data" :options="options" />
    <Bar v-else :data="dummyData" :options="options" />
  </div>
</template>

<style scoped>
.chart-container {
  flex-grow: 1;
  min-height: 0;
  
  height: 100%;
}
</style>