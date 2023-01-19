<template>
  <div class="post">
    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a>
      for more details.
    </div>

    <div v-if="post" class="content">
      <v-data-table
      :headers="headers"
      :items="post"
      :items-per-page="2">
      </v-data-table>
      <v-btn elevation="2">Button</v-btn>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent} from 'vue';

type Users = {
  id: number,
  name: string,
  email: string
}[];

type Headers = {
  text: string,
  value: string
}[];

interface Data {
  loading: boolean,
  post: null | Users
  
  headers: Headers
}

export default defineComponent({
  data(): Data {
    return {
      loading: false,
      post: null,
      headers: [
        { text: 'Id', value: 'id'},
        { text: 'Name', value: 'name'},
        { text: 'EMail', value: 'email'},
      ]
    };
  },
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    this.fetchData();
  },
  watch: {
    // call again the method if the route changes
    '$route': 'fetchData'
  },
  methods: {
    fetchData(): void {
      this.post = null;
      this.loading = true;

      fetch('api/v1.0/users')
          .then(r => r.json())
          .then(json => {
            this.post = json as Users;
            this.loading = false;
            return;
          });
    }
  },
});
</script>