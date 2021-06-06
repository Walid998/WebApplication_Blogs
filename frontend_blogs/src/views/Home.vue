<template>
  <div class="home">
    <nav-bar />
    <div style="margin-top: 50px"></div>
    <v-container class="lighten-5">
      <div style="display: block; margin: 10px; text-align: center">
        <h2 style="font-weight: lighter; margin-bottom: 20px">All Blogs</h2>
        <add-blog v-on:get-blogs="getBlogs" />
      </div>
      <br />
      <h2
        v-if="isBlogsEmpty"
        class="text-center"
        style="margin-top: 50px; font-weight: 400"
      >
        <small>no blogs posted yet ....</small>
      </h2>
      <v-row no-gutters style="margin-top: 20px">
        <v-col v-for="blog in blogsData" :key="blog.id" cols="12" sm="4">
          <blog-card :blog="blog" v-on:get-blogs="getBlogs" />
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
// @ is an alias to /src
import NavBar from "@/components/NavBar";

import { getAPI } from "../axios-api";
import AddBlog from "../components/Blog/AddBlog.vue";
import BlogCard from "../components/Blog/BlogCard.vue";
export default {
  name: "Home",
  components: {
    NavBar,
    AddBlog,
    BlogCard,
  },
  data: () => {
    return {
      blogsData: "",
      isBlogsEmpty: false,
    };
  },
  created() {
    this.getBlogs();
  },

  methods: {
    getBlogs() {
      getAPI
        .get("/blogs/", {
          headers: { Authorization: `Bearer ${localStorage.getItem("jwt")}` },
        })
        .then((response) => {
          this.blogsData = response.data;
          this.isBlogsEmpty = this.blogsData.length == 0 ? true : false;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>
