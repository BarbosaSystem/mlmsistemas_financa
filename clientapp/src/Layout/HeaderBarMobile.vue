<template>
  <nav style="z-index: 1000" class="navbar topo navbar-expand-lg bg-gray d-block d-md-none d-lg-none">
    <div class="container">
      <router-link to="/userprofile" tag="a" class="navbar-brand" exact-active-class="ativo">
        <img class="img-thumbnail" style="height: 56px; object-fit: cover; width: 56px; padding: 0" :src="GetUser.photoString" alt />
      </router-link>
      <ul class="navbar-nav mr-auto">
        <li class="nav-item tituloUser">
          <a
            class="nav-link"
            href="#"
            style="color: white; text-transform: uppercase"
          >{{GetUser.userName}}</a>
        </li>
      </ul>
      <ul class="navbar-nav text-center">
        <router-link to="/logout" tag="li" class="nav-item" exact-active-class="ativo">
          <i class="nav-link fas fa-power-off"></i>
          <p class="nav-text text-center">Sair</p>
        </router-link>
      </ul>
    </div>
  </nav>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["GetUser"]),
  },
};
</script>