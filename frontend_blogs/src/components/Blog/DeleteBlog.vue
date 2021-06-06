<template>
  <v-dialog persistent v-model="dialog" width="500" height="400">
    <v-card
      v-if="dialog"
      class="transition-fast-in-fast-out"
      style="height: 100%; direction: rtl"
    >
      <v-card-title class="headline lighten-2">
        <v-spacer></v-spacer>
        <v-icon color="red" style="font-size: 42px">mdi-alert-octagon</v-icon>
        <v-spacer></v-spacer>
      </v-card-title>
      <v-card-text class="text-center">
        <h3>Are you sure to delete</h3>
        <br />
        <h2>{{ blog.title }}</h2>
        <br />
        <h2>?</h2>
      </v-card-text>
      <v-divider></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-form @submit.prevent="DeleteBlog">
          <v-btn type="submit" color="red" text>Delete</v-btn>
        </v-form>
        <v-btn text color="accent-4" @click="$emit('close')"> Close </v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { getAPI } from "../../axios-api";

export default {
  props: ["dialog", "blog"],
  methods:{
    DeleteBlog(){
      getAPI
          .delete("/blogs/"+this.$props['blog'].id, {headers:{Authorization: `Bearer ${localStorage.getItem("jwt")}`}})
          .then(() => {
            this.$emit("close");
            this.$emit("get-blogs");
          })
          .catch((err) => console.log(err));
    }
  }
};
</script>