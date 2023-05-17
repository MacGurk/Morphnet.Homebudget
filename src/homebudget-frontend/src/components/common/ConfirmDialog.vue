<template> 
  <v-dialog v-model="dialog" max-width="512px" @keydown:esc="cancel">
    <v-card>
      <v-card-title :class="cardTitleColor">{{ title }}</v-card-title>
      <v-card-text>{{ text }}</v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn variant="outlined" @click="cancel">Cancel</v-btn>
        <v-btn
            class="font-weight-bold"
            :color="color"
            variant="flat"
            @click="confirm"
        >
          {{ confirmText }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import {defineComponent} from 'vue';

interface DialogData {
  dialog: boolean;
  color: string;
  title: string;
  text: string;
  confirmText: string;
  resolve: Function;
  reject: Function;
}
export default defineComponent({
  name: 'ConfirmDialog',
  data(): DialogData {
    return {
      dialog: false,
      color: 'primary',
      title: '',
      text: '',
      confirmText: '',
      resolve: Function,
      reject: Function,
    }
  },
  methods: {
    open(title: string, text: string, confirmText = 'Ok', color = 'primary') {
      this.dialog = true;
      this.title = title;
      this.text = text;
      this.confirmText = confirmText;
      this.color = color;
      return new Promise((resolve, reject) => {
        this.resolve = resolve;
        this.reject = reject;
      })
    },
    confirm() {
      this.resolve(true);
      this.dialog = false;
    },
    cancel() {
      this.resolve(false);
      this.dialog = false;
    }
  },
  computed: {
    cardTitleColor(): string {
      return `bg-${this.color}`;
    }
  }
})
</script>

<style scoped>
</style>