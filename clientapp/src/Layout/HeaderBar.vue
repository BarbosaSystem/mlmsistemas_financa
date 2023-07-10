<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark" v-if="GetUser">
    <router-link to="/principal">
    <img src="../assets/search.png" />
      <!-- <img src="../src/assets/search.png" class="navbar-brand" style="height: 40px" /> -->
    </router-link>

    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#conteudoNavbarSuportado"
      aria-controls="conteudoNavbarSuportado"
      aria-expanded="false"
      aria-label="Alterna navegação"
      @click="MostrarToggle()"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="conteudoNavbarSuportado" :class="{show: ShowToggle}">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item">
          <router-link class="nav-link" to="/upload" >Upload files</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/periodo">Periodos</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/movimentos">Movimentações</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/categoria">Categoria</router-link>
        </li>
      </ul>
      <ul class="navbar-nav ml-auto">
        <li class="nav-item" style="cursor: pointer">
          <router-link class="nav-link" :to="'/userprofile/' + GetUser.codigo">
            <img :src="GetUser.photo" class="img-logo" />
            {{GetUser.userName}}
          </router-link>
        </li>
        <li class="nav-item" style="cursor: pointer">
          <router-link class="nav-link" to="/logout">
          <img src="../assets/logout.png" /> Sair
          </router-link>
        </li>
      </ul>
    </div>
  </nav>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["GetUser"])
  },
  data() {
    return {
      ShowToggle: false
    };
  },
  methods: {
    MostrarToggle() {
      this.ShowToggle = !this.ShowToggle;
    },
    Logout() {
      if (confirm("Deseja Sair?")) {
        this.Action_Logout();
      }
    }
  }
};
</script>
<style lang="scss">
.img-logo {
  border-radius: 50%;
  display: inline;
  margin-right: 5px;
  object-fit: cover;
}
.user-name-logo {
  display: inline;
}
</style>