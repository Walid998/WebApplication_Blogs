<template>
  <v-card max-width="250" height="330" class="mx-auto my-5">
    <v-img
      v-if="blog.image != null"
      :src="path+blog.image"
      aspect-ratio="1"
      height="180px"
    >
    </v-img>
    <v-img
      v-else
      :src="require('@/assets/default_blog.png')"
      aspect-ratio="1"
      height="180px"
    >
    </v-img>

    <v-card-title>
      {{ blog.title }}
      <v-spacer></v-spacer>

      <v-menu offset-y v-if="getUserRole('admin')">
        <template v-slot:activator="{ on, attrs }">
          <v-btn v-bind="attrs" v-on="on" icon text color="#000">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item link>
            <v-list-item-title @click="activeEditBlog = true">
              <v-icon text>mdi-pencil-outline</v-icon>
              Edit
            </v-list-item-title>
          </v-list-item>
          <v-list-item link>
            <v-list-item-title @click="activeDeleteBlog = true">
              <v-icon class="red--text" text>mdi-trash-can-outline</v-icon>
              Delete
            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-card-title>

    <v-card-text>
      <div>
        {{ blog.content }}
      </div>
    </v-card-text>

    <edit-blog
      :dialog="activeEditBlog"
      :blog="blog"
      v-on:get-blogs="$emit('get-blogs')"
      v-on:close="
        () => {
          this.activeEditBlog = false;
        }
      "
    />
    <delete-blog
      :dialog="activeDeleteBlog"
      :blog="blog"
      v-on:get-blogs="$emit('get-blogs')"
      v-on:close="
        () => {
          this.activeDeleteBlog = false;
        }
      "
    />
  </v-card>
</template>
<script>
import EditBlog from "./EditBlog.vue";
import DeleteBlog from "./DeleteBlog.vue";
export default {
  components: {
    EditBlog,
    DeleteBlog
  },
  data() {
    return {
      path: "https://localhost:44317/images/",
      activeEditBlog: false,
      activeDeleteBlog: false,
    };
  },
  props: ["blog"],
  computed: {
    blogImage() {
      return {
        ...this.$props["blog"],
        img:
          this.$props["blog"].image &&
          require("C:\\Users\\Toshiba\\source\\repos\\WebApplication_Blogs\\WebApplication_Blogs\\wwwroot\\images\\" +
            this.$props["blog"].image),
      };
    },
  },
  methods: {
    getUserRole(role) {
      if (localStorage.getItem("role") == role) {
        return true;
      }
      return false;
    },
  },
  // data() {
  //   return {
  //     b_img: require("C:\\Users\\Toshiba\\source\\repos\\WebApplication_Blogs\\WebApplication_Blogs\\wwwroot\\images\\"+this.$props['blog'].image),
  //   };
  // },
};
</script>
