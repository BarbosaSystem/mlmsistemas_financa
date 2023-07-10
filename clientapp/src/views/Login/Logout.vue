<template>
  <div class="col-12 logout">
    <div class="logout-area">
      <div class="card logout-area-card">
        <div class="card-header logout-area-card-header">
          <h4>Sistema de Financas</h4>
        </div>
        <div class="card-body logout-area-card-body">
          <div class="form-group">
            <img :src="UserRelogin.Photo" alt class="logout-area-card-body-imagem" />
            <div class="form-group mt-2">
              <h5 style="color: black;">{{UserRelogin.UserName}}</h5>
            </div>
            <div class="input-group input-logout">
              <input v-model="Password" type="password" class="form-control" placeholder="password" />

              <div class="input-group-btn">
                <button type="button" class="btn" @click="Relogin()">
                  <i class="fas fa-door-open"></i> Acessar
                </button>
              </div>
            </div>
          </div>
        </div>
        <div class="card-footer logout-area-card-footer">
          <div class="form-group">
            <button class="btn btn-primary" @click="Action_Logout()">
              <i class="fas fa-door-closed"></i> Encerrar Sessão
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      UserRelogin: {
        UserName: "",
        Photo: null
      },
      Password: "",
      ShowModal: true
    };
  },
  computed: {
    ...mapGetters(["GetUser"])
  },
  methods: {
    ...mapActions(["Action_Logout", "Action_Logar"]),
    GuardarDados() {
      if (this.GetUser != null) {
        var reLogin = {
          UserName: this.GetUser.userName,
          Photo: this.GetUser.photoString
        };
        this.UserRelogin = reLogin;
      }
    },
    Relogin(){
      var formulario = {
        UserName: this.UserRelogin.UserName,
        Password: this.Password
      };
      this.$store.commit("updateSenha", this.Password)
      this.$store.commit("updateLogin", this.GetUser.userName)
      this.Action_Logar()
    }
  },
  computed: {
    ...mapGetters(["GetUser"]),
    VerifyRelogin() {
      if (this.UserRelogin.UserName != "") return true;
      else return false;
    }
  },
  created() {
    this.GuardarDados();
    if (!this.VerifyRelogin) {
      this.$router.replace("/");
    }
    
  }
};
</script>
<style lang="sass" scoped>

</style>