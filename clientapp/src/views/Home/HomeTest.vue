<template>
  <div>
    <div class="conteudo">
      <nav class="navbar navbar-expand-lg navbar-dark fixed-top py-3">
        <div class="container">
          <a href class="navbar-brand"><img src="../../assets/logo-transparent.png" alt=""/></a>
          <div class="navbar-collapse collapse" id="navbarResponsive" style>
            <ul class="navbar-nav ml-auto my-2 my-lg-0">
              <li class="nav-item">
                <button class="btn btn-light" @click="AbrirLogin()">Sistema de Finança</button>
              </li>
            </ul>
          </div>
        </div>
      </nav>
      <header class="masthead">
        <div class="container h-100">
          <div class="row h-100 align-items-center justify-content-center text-center">
            <div class="col-lg-10 align-self-end">
              <h1 class="text-uppercase text-white font-weight-bold">MLM SISTEMAS</h1>
              <hr class="divider my-4" />
            </div>
            <div class="col-lg-8 align-self-baseline">
              <p class="text-white-75 font-weight-light mb-5">TRANSFORMANDO A SUA DOR EM SATISFAÇÃO.</p>
              <p class="text-white-75 font-weight-light mb-5"><button class="btn btn-light" @click="AbrirLogin()">Sistema de Finança</button></p>
            </div>
          </div>
        </div>
        <!-- https://startbootstrap.com/themes/creative/# -->
      </header>
    </div>
    <modal-login></modal-login>
  </div>
</template>
<script>
import ModalLogin from "../../components/ModalLogin.vue";
import { mapGetters } from 'vuex';
export default {
  computed: {
    ...mapGetters(["GetUser"]),
  },
  components: {
    ModalLogin
  },
  methods: {
    AbrirLogin() {
      this.$root.$emit("ModalLogin::show");
    }
  },
  beforeMount() {
    if(localStorage.getItem('Token')){
      this.$router.replace('/principal')
    }
  },
};
</script>
<style lang="scss">
.conteudo {
  background: linear-gradient(
      to bottom,
      rgba(44, 62, 80, 0.5) 0%,
      rgba(44, 62, 80, 0.5) 100%
    ),
    url("../../assets/fundo.jpg");
    background-position: center;
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-size: cover;
  .navbar {
    box-shadow: none;
    background-color: transparent;
    &-brand {
      color: white;
    }
    .navbar-collapse {
      .nav-item {
        .nav-link {
          color: white;
        }
      }
    }
  }
  header.masthead {
    height: 100vh;
    min-height: 40rem;
    padding-top: 4.5rem;
    padding-bottom: 0;
  }
  .page-section {
    padding: 8rem 0;
  }
}
</style>
