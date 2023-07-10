<template>
  <div class="item text-center">
    <input type="checkbox" name id="check1" v-model="check" />
    <label for="check1">
      <template>
        <button class="btn btn-primary btn-blocked" @click="toggleCheck">
          <i class="fas" :class="{'fa-angle-double-up' : check, 'fa-angle-double-down' : !check}"></i>
        </button>
      </template>
    </label>
    <transition name="fade">
      <ul class="lista-menu" v-if="check && GET_Menu_Ativo">
        <router-link
          v-for="(item, index) in GET_Menu_Ativo"
          :key="index"
          class="nav-link lista-menu-item"
          :class="{'text-center': toggle, 'text-left': !toggle}"
          tag="li"
          :to="item.rota"
          exact-active-class="ativo"
        >
          <i :class="item.icone"></i>
          <template v-if="!toggle"> {{item.titulo}}</template>
        </router-link>
      </ul>
      
    </transition>
    <!-- <div v-if="GetLoadingData" class="mini-loading" style="padding: 50% auto; text-align: center;">
        <i class="fas fa-2x fa-sync fa-spin"></i>
        <p>Carregando...</p>
    </div> -->
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  props: ["toggle"],
  computed: {
    ...mapGetters(["GetUser", "GET_Menu_Ativo", "GetLoadingData"])
  },
  methods: {
    toggleCheck() {
      this.check = !this.check;
    }
  },
  data() {
    return {
      check: true
    };
  }
};
</script>
<style>
.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to{
  opacity: 0;
}
</style>