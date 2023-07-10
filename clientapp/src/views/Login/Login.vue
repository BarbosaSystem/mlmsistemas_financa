<template>
  <div class="text-center">
    <div class="login" v-if="!Recover">
      <form @submit.prevent="Action_Logar()" class="form-group">
        <h5 class="modal-title">Acesso ao Sistema</h5>
        <input type="text" class="form-control mt-3" placeholder="Nome de Usuário" v-model="Login" :disabled="GetLoading" />
        <input type="password" class="form-control mt-5" placeholder="Password" v-model="Senha" :disabled="GetLoading" />
        <button
          class="btn btn-primary btn-block mt-5"
          type="submit"
          :disabled="!(Login != '' && Senha != '') "
        ><i class="fas fa-circle-notch fa-spin" v-if="GetLoading"></i> Acessar</button>
      </form>
      <button class="btn btn-default" @click="Recover = !Recover">
        Esqueci a minha senha
      </button>
    </div>
    <recover-password :recover="Recover" @fechar-recover="recover"></recover-password>
  </div>
</template>
<style lang="stylus" scoped></style>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import Service from "../../Service/account";
import RecoverPassword from '../../components/SHARED/RecoverPassword'
export default {
  components: {
    'recover-password' : RecoverPassword
  },
  data() {
    return {
      Email: '',
      Recover: false
    };
  },
  methods: {
    ...mapActions(["Action_Logar"]),
    ...mapMutations(["updateLogin", "updateSenha"]),
    recover(payload){
      this.Recover = payload
    },
  },
  computed: {
    ...mapGetters(["GetLogin", "GetUser", "GetLoading"]),
    Login: {
      get() {
        return this.GetLogin.Login;
      },
      set(value) {
        this.updateLogin(value);
      }
    },
    Senha: {
      get() {
        return this.GetLogin.Senha;
      },
      set(value) {
        this.updateSenha(value);
      }
    }
  },

};
</script>