<template>
  <v-sheet class="ma-4 pa-3" rounded="lg">
    <div class="d-flex justify-center align-center"></div>
    <v-container>
      <v-row justify="end">
        <v-col />
        <v-col>
          <div class="d-flex justify-center align-center">
            <v-icon size="large" @click="setFilters({year: filters.year - 1})">
              mdi-chevron-double-left
            </v-icon>
            <div class="mx-3 year-text text-center">
              <v-text-field
                v-if="editYear"
                ref="yearText"
                v-model="enteredYear"
                type="text"
                density="compact"
                variant="solo"
                single-line
                hide-details
                @blur="finishEditYear"
                @keydown:enter="finishEditYear"
              ></v-text-field>
              <div v-else class="text-h6" @click="startEditYear">
                {{ filters.year }}
              </div>
            </div>
            <v-icon size="large" @click="setFilters({year: filters.year + 1})">
              mdi-chevron-double-right
            </v-icon>
          </div>
        </v-col>
        <v-col>
          <div class="d-flex justify-end align-center">
            <v-btn
              color="info"
              icon="mdi-calendar-today"
              @click="currentMonth"
            ></v-btn>
          </div>
        </v-col>
      </v-row>
    </v-container>
    <div class="d-flex justify-space-evenly ma-3">
      <v-select
        v-if="mobile"
        v-model="filters.month"
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
  </v-sheet>
</template>

<script setup lang="ts">
import { monthNames } from '@/utils/utils';
import {ref, watch, nextTick, inject} from 'vue';
import { useDisplay } from 'vuetify';
import {FilterContext, FilterSymbol} from "@/components/transactions/filters";

const {filters, setFilters} = inject(FilterSymbol) as FilterContext;

const yearText = ref<HTMLInputElement | null>(null);

const editYear = ref<boolean>(false);
const enteredYear = ref<number>(0);

const allMonths = ref(monthNames);

const { mobile } = useDisplay();

const startEditYear = () => {
  enteredYear.value = filters.value.year;
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
    filters.value.year = enteredYear.value;
  }

  editYear.value = false;
};

const changeMonth = (clickedMonth: number) => {
  setFilters({month: clickedMonth});
  // selectedMonth.value = clickedMonth;
};

const monthActive = (index: number) => {
  return filters.value.month === index ? 'bg-primary' : 'monthbox-bg';
};

const currentMonth = () => {
  const now = new Date();
  filters.value.month = now.getMonth();
  filters.value.year = now.getFullYear();
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
