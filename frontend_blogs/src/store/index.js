import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({

  getters: {
    loggedIn() {
      return localStorage.getItem("jwt") != null;
    },
  },
  actions: {
    userLogout(context) {
      if (context.getters.loggedIn) {
        localStorage.removeItem("jwt");
        localStorage.removeItem("role");
        localStorage.removeItem("name");
      }
    },
  },
  modules: {},
});
