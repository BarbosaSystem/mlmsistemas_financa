<template>
  <div class="side-menu" v-if="GetUser" :class="{toggle: GetToogle}">
    <div class="logo" style="font-size: 1.5rem;">
      <div class="logo-texto text-center" style="display: inline">
        <button class="btn btn-primary" @click="setToggle()">
            <img :src="ImageToogle" :alt="GetUser.userName">
          </button>
      </div>
    </div>
    <div class="logo">
      <div class="caixa">
        <router-link to="/userprofile">
          <button class="btn btn-primary" style="max-width: 100%">
          <img :src="GetUser.photoString" class="img-thumbnail logo-item" />
          <p class="caixa-username">{{GetUser.userName}}</p>
        </button>
        </router-link>
        
        <lista-inline :toggle="GetToogle"></lista-inline>
      </div>
    </div>
    <lista-menu :toggle="GetToogle"></lista-menu>
  </div>
</template>
<script>
import lista_menu from "../components/SHARED/lista_menu";
import lista_inline from "../components/SHARED/lista_menu_inline";
import ImagemLogo from "../assets/logo-mlm.png";
import ImageToogle from "../assets/logo-toogle.png";
import { mapGetters, mapMutations } from "vuex";
import mixRezise from '../js/mix_rezise'
export default {
  mixins: [mixRezise],
  data() {
    return {
      toogle: false,
      LogoFull : ImagemLogo,
      LogoMin: ImageToogle
    }
  },
  methods: {
    ...mapMutations(["setToggle"]),
  },
  components: {
    "lista-menu": lista_menu,
    "lista-inline": lista_inline
  },
  computed: {
    ...mapGetters(["GetUser", "GetToogle"]),
    Toogle: {
      get(){
        return this.GetToogle
      },
      set(value){
        this.setToogle()
      }
    },
    ImageToogle(){
      if(this.GetToogle)
        return this.LogoMin
      else
        return this.LogoFull
    } 
  },
};
</script>
<style lang="css">

.lista-menu-item {
  cursor: pointer;
}

</style>