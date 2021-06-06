<template>
  <div>
    <v-row no-gutters>
      <v-col class="text-center col">
        <h1
          style="
            font-family: 'Brush Script MT';
            font-size: 100px;
            margin-top: 15px;
          "
        >
          Blogs
        </h1>
        <h2 style="padding: 0px 60px">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo est saepe
          quis quisquam fuga omnis animi enim hic cumque nam.
        </h2>
      </v-col>
      <v-col class="text-center col">
        <v-sheet elevation="2" class="mx-auto mt-5" width="350">
          <v-card height="300">
            <v-card-title class="lighten-4">
              <v-spacer></v-spacer>
              <span style="font-weight: 400"
                >LOGIN <small>TO</small> BLOGS</span
              >
              <v-spacer></v-spacer>
            </v-card-title>
            <v-form
              ref="form"
              v-model="valid"
              @submit.prevent="submit"
              lazy-validation
            >
              <v-card-text>
                <p v-if="IncorrectAuth" style="color: red">
                  Incorrect username or password !
                </p>
                <v-text-field
                  v-model="username"
                  :rules="[(v) => !!v || 'Username is required']"
                  label="Username"
                  required
                ></v-text-field>
                <v-text-field
                  v-model="password"
                  autocomplete="on"
                  :rules="[(v) => !!v || 'Password is required']"
                  label="Password"
                  :append-icon="show_password ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="show_password ? 'text' : 'password'"
                  @click:append="show_password = !show_password"
                  required
                ></v-text-field>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="black" type="submit" class="white--text" style="font-weight: 300">
                  login
                </v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-form>
          </v-card>
        </v-sheet>
      </v-col>
      
    </v-row>
  </div>
</template>

<style scoped>
.col {
  margin: 9% auto;
}
</style>

<script>
import { getAPI } from "../axios-api";
import jwt_decode from "jwt-decode";
export default {
  data: () => {
    return {
      username: "",
      password: "",
      show_password: false,
      valid: true,
      IncorrectAuth: false,
    };
  },
  methods: {
    submit() {
      if (this.$refs.form.validate()) {
        getAPI
          .post("/Auth/Login", {
            username: this.username,
            password: this.password,
          })
          .then((response) => {
            let decoded = jwt_decode(response.data.token);
            let userrole =
              decoded[
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
              ];
              console.log(decoded);
            let usrnm = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
            localStorage.setItem("jwt", response.data.token);
            localStorage.setItem("role", userrole);
            localStorage.setItem("name", usrnm);

            this.$router.push({ name: "Home" });
          })
          .catch((err) => {
            console.log(err);
            this.IncorrectAuth = true;
          });
      }
    },
  },
};
</script>