<template>
  <div class="text-center">
    <v-dialog v-model="dialog" width="400" >
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="lighten-2" dark v-bind="attrs" v-on="on"> Add New </v-btn>
      </template>

      <v-card>
        <v-card-title class="lighten-2">
          <v-spacer></v-spacer>
          <span style="font-weight: lighter"
                >ADD <small>NEW</small> BLOG</span
              >
          <v-spacer></v-spacer>
          
        </v-card-title>
        <v-divider></v-divider>
        <v-form
          ref="form"
          v-model="valid"
          @submit.prevent="submit"
          lazy-validation
        >
          <v-card-text>
            <div class="text-center">
              <img
                :src="src_image"
                height="120px"
                width="140px"
              />
              <br />
              <input
                type="file"
                id="img"
                @change="imagePreview"
                ref="i_image"
                style="display: none"
              />
              <v-btn @click="$refs.i_image.click()"
                ><v-icon>mdi-upload</v-icon>upload</v-btn
              >
            </div>
            <v-text-field
              v-model="title"
              :rules="[(v) => !!v || 'Title is required']"
              label="Title"
              required
            ></v-text-field>
            <v-textarea
              auto-grow
              v-model="content"
              :rules="[(v) => !!v || 'Content is required']"
              label="Content"
              rows="1"
              required
            ></v-textarea>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" text type="submit">Add</v-btn>
            <v-btn text @click="dialog = !dialog">Close</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { getAPI } from "../../axios-api";

export default {
  data() {
    return {
      title: "",
      content: "",
      src_image: require('@/assets/default_blog.png'),
      image: "",
      valid: true,
      dialog: false,
    };
  },
  methods: {
    imagePreview(e) {
      if (e.target.files[0]) {
        const reader = new FileReader();
        reader.onload = x => {
          
          this.src_image = x.target.result;
        }
        reader.readAsDataURL(e.target.files[0])
      }
    },
    submit() {
      if (this.$refs.form.validate()) {
        console.log("go" + this.title + " " + this.content + " ");
        const bodyFormData = new FormData();
        bodyFormData.append("Title", this.title);
        bodyFormData.append("Content", this.content);
        bodyFormData.append("Image", document.getElementById("img").files[0]);
        getAPI
          .post("/blogs/", bodyFormData, {headers:{Authorization: `Bearer ${localStorage.getItem("jwt")}`}})
          .then((response) => {
            console.log(response.data);
            this.dialog= false;
            this.$emit("get-blogs");
            this.clean();
          })
          .catch((err) => console.log(err));
      }
    },
    clean(){
      this.title = null;
      this.content= null;
      this.src_image= require('@/assets/default_blog.png');
      document.getElementById("img").value = null;
    }
  },
};
</script>