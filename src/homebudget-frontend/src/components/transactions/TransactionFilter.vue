<template>
  <div class="d-flex justify-center align-center">
    <v-icon size="large" @click="this.year--">
      mdi-chevron-double-left
    </v-icon>
    <div class="mx-3">
      <v-text-field
          ref="yearText"
          type="text"
          v-model="enteredYear"
          v-if="editYear"
          @blur="finishEditYear"
          @keydown:enter="finishEditYear"
      ></v-text-field>
      <div v-else class="text-h6" @click="startEditYear">
        {{ year }}
      </div>
    </div>
    <v-icon size="large" @click="this.year++">
      mdi-chevron-double-right
    </v-icon>
  </div>
  <div class="d-flex justify-space-evenly ma-3">
    <div
        class="month-box border border-secondary flex-column flex-grow-1 text-center py-2"
        :class="monthActive(index)"
        v-bind:key="index" v-for="(month, index) in allMonths"
        @click="changeMonth(index)"
    >
      {{ month }}
    </div>
  </div>
</template>

<script lang="ts">
import { monthNames } from '@/utils/utils';
import {defineComponent} from 'vue';

interface TransactionFilterData{
  month: number;
  allMonths: String[];
  year: number;
  editYear: boolean
  enteredYear: number;
}

export default defineComponent({
  name: 'TransactionFilter',
  data(): TransactionFilterData {
    return {
      month: new Date().getMonth(),
      allMonths: monthNames,
      year: new Date().getFullYear(),
      editYear: false,
      enteredYear: 0,
    }
  },
  methods: {
    startEditYear() {
      this.enteredYear = this.year;
      this.editYear = true;
      this.$nextTick(() => {
        this.$refs.yearText.focus();
      })
    },
    finishEditYear(e: Event) {
      console.log('Cancel')
      e.preventDefault();
      if (this.enteredYear <= new Date().getFullYear() && this.enteredYear > 1970) {
        this.year = this.enteredYear
      }
      
      this.editYear = false;
    },
    changeMonth(month: number) {
      this.month = month;
    },
    monthActive(index: number) {
      return this.month === index ? 'bg-primary' : '';
    }
  },
  emits: ['filterChanged'],
  watch: {
    month() {
      this.$emit('filterChanged', this.month, this.year);
    },
     year() {
       this.$emit('filterChanged', this.month, this.year);
     } 
  }
})
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
</style>