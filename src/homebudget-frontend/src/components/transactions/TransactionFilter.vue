<template>
  <div class="d-flex justify-center align-center">
    <v-icon size="large" @click="year--"> mdi-chevron-double-left </v-icon>
    <div class="mx-3 year-text text-center">
      <v-text-field
        v-if="editYear"
        ref="yearText"
        v-model="enteredYear"
        type="text"
        @blur="finishEditYear"
        variant="solo"
        single-line
        hide-details
        density="compact"
        @keydown:enter="finishEditYear"
      ></v-text-field>
      <div v-else class="text-h6" @click="startEditYear">
        {{ year }}
      </div>
    </div>
    <v-icon size="large" @click="year++"> mdi-chevron-double-right </v-icon>
  </div>
  <div class="d-flex justify-space-evenly ma-3">
    <v-select
      v-if="mobile"
      v-model="selectedMonth"
      :items="allMonths"
      item-value="index"
      item-title="name"
    ></v-select>
    <div
      v-for="month in allMonths"
      v-else
      :key="month.index"
      class="month-box border v-table__wrapper flex-column flex-grow-1 text-center py-2"
      :class="monthActive(month.index)"
      @click="changeMonth(month.index)"
    >
      {{ month.name }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { monthNames } from '@/utils/utils';
import { ref, watch, nextTick } from 'vue';
import { useDisplay } from 'vuetify';

const yearText = ref<HTMLInputElement | null>(null);

const selectedMonth = ref<number>(new Date().getMonth());
const year = ref<number>(new Date().getFullYear());
const editYear = ref<boolean>(false);
const enteredYear = ref<number>(0);

const allMonths = ref(monthNames);

const { mobile } = useDisplay();

const emits = defineEmits<{
  (_e: 'filterChanged', _month: number, _year: number): true;
}>();

watch(selectedMonth, () => {
  emits('filterChanged', selectedMonth.value, year.value);
});
watch(year, () => {
  emits('filterChanged', selectedMonth.value, year.value);
});

const startEditYear = () => {
  enteredYear.value = year.value;
  editYear.value = true;
  nextTick(() => {
    yearText.value!!.focus();
  });
};

const finishEditYear = (e: Event) => {
  console.log('Cancel');
  e.preventDefault();
  if (
    enteredYear.value <= new Date().getFullYear() &&
    enteredYear.value > 1970
  ) {
    year.value = enteredYear.value;
  }

  editYear.value = false;
};

const changeMonth = (clickedMonth: number) => {
  selectedMonth.value = clickedMonth;
};

const monthActive = (index: number) => {
  return selectedMonth.value === index ? 'bg-primary' : 'monthbox-bg';
};
</script>

<style scoped>
.month-box {
  cursor: pointer;
}
.month-box:first-of-type {
  border-radius: 15px 0 0 15px;
}
.month-box:last-of-type {
  border-radius: 0 15px 15px 0;
}

.monthbox-bg {
  background-color: rgb(var(--v-theme-surface));
}

.year-text {
  width: 75px;
}
</style>
