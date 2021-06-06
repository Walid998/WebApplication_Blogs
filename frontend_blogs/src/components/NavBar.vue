<template>
  
    <v-app-bar
      color="#008088 accent-4"
      dense
      dark
      flat
      app
    >
      <v-container class="py-0 fill-height">
      <v-toolbar-title style="font-family: 'Brush Script MT', cursive;font-size:32px">Blogs</v-toolbar-title>

      <v-spacer></v-spacer>
      <v-toolbar-title color="white" text class="mr-4">
        <span class="text-subtitle-1">{{username}}<sub> {{role}}</sub></span>
      </v-toolbar-title>
      <v-btn icon @click="logout">
        <v-icon title="logout">mdi-logout</v-icon>
      </v-btn>
      
      </v-container>
    </v-app-bar>
  
</template>
<script>
export default {
  data(){
    return{
      username: localStorage.getItem("name"),
      role: localStorage.getItem("role")
    }
  },

  methods:{
    logout(){ 
      this.$store.dispatch("userLogout").then(()=>{
        this.$router.go()
      })
    }
  }
}
</script>
