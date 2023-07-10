<template>
  <div id="app">
    <header-mobile v-if="!noLogin"></header-mobile>
    <loading v-if="GetLoading"></loading>
    <div class="container-fluid" >
      <div class="row">
        <side-component></side-component>
        <div class="area-side" :class="{fullarea: noLogin, toogle: GetToogle}">
          <router-view></router-view>
        </div>
      </div>
    </div>
    <footer-mobile v-if="!noLogin"></footer-mobile>
    
  </div>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import Service from "./Service/account";
import Loading from "./components/SHARED/Loading";
import FooterMobile from "./Layout/FooterBarMobile";
import HeaderMobile from './Layout/HeaderBarMobile';
export default {
  data() {
    return {
      loading: false
    };
  },
  components: {
    'loading' : Loading,
    'footer-mobile' : FooterMobile,
    'header-mobile' : HeaderMobile
  },
  methods: {
    ...mapActions(["Action_Logout", "Action_VerifyToken", "Action_Profile"]),
    MostrarAlert() {
      this.$root.$emit("Alerta::show", {
        mensagem: "response.data",
        type: "alert-warning"
      });
    }
  },
  computed: {
    ...mapGetters(["GetUser", "GetToogle", "GetLoading"]),
    noLogin() {
      if (this.GetUser != null || this.GetUser != undefined) {
        return false;
      } else {
        return true;
      }
    }
  },
   async moutend(){
    if(localStorage.getItem('Token') == null){
      this.$store.commit("MUTATION_ADD_USER_PROFILE", null)
    }
  }
};
</script>
<style lang="scss">
@import "../src/Style/_plugin.scss";
.img-logo {
  border-radius: 50%;
  display: inline;
  margin-right: 5px;
  object-fit: cover;
  @include invisible(992px);
}
.user-name-logo {
  display: inline;
}
</style>