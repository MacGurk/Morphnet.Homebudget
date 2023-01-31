<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="4">
        <v-card class="mx-auto px-6 pb-8">
          <v-card-title>Login</v-card-title>
          <v-card-text>
            <v-form>
              <v-text-field
                  v-model="username"
                  label="Username"
                  required
              ></v-text-field>
              <v-text-field
                  v-model="password"
                  label="Password"
                  :type="passwordVisible ? 'text' : 'password'"
                  :append-inner-icon="passwordVisible ? 'mdi-eye-off' : 'mdi-eye'"
                  @click:append-inner="switchVisibility"
                  required
              ></v-text-field>
              <v-btn type="submit" size="large" color="primary" block>Sign In</v-btn>
            </v-form>
          </v-card-text>
          
        </v-card>
        
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import {defineComponent} from "vue";

export default defineComponent({
  name: "Login",
  data() {
    return {
      username: '',
      password: '',
      passwordVisible: false,
    }
    
  },
  methods: {
    switchVisibility():void {
      this.passwordVisible = !this.passwordVisible
    }
  }
})
</script>

<style scoped>

</style>