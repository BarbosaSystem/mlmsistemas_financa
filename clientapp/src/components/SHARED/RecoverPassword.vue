<template>
  <div class="card" v-if="recover" :disabled="GetProcess">
    <div class="card-body">
      <div class="text-center">
        <h3>
          <i class="fa fa-lock fa-4x"></i>
        </h3>
        <h2 class="text-center">Esqueceu a Senha?</h2>
        <p>Você pode redefinir sua senha aqui</p>
        <form @submit.prevent="Action_RecoverPassword()">
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon">
                <i class="glyphicon glyphicon-envelope color-blue"></i>
              </span>
              <input
                id="email"
                name="email"
                placeholder="Informe E-mail"
                class="form-control"
                type="email"
                v-model="Email"
                :disabled="GetProcess"
              />
            </div>
          </div>
          <div class="form-group">
            <button
              name="recover-submit"
              class="btn btn-lg btn-primary btn-block"
              type="submit"
              placeholder="Resetar Senha"
              :disabled="!EmailValidate || GetProcess"> 
              <i class="fas fa-circle-notch fa-spin" v-if="GetProcess"></i> Resetar Senha </button>
          </div>
        </form>
      </div>
      <div class="text-left" >
        <button class="btn btn-default" @click="FecharTela" >
          <i class="fas fa-hand-point-left"></i> Voltar para tela de login
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapGetters, mapMutations } from 'vuex';
export default {
  props: ["recover"],
  data() {
      return {
      }
  },
  computed: {
    ...mapGetters(["GetEmail", "GetProcess"]),
    Email: {
      get(){
        return this.GetEmail
      },
      set(value){
        this.updateEmail(value)
      }
    },
    EmailValidate() {
      const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(String(this.Email).toLowerCase());
    }
  },
  methods: {
    ...mapMutations(["updateEmail"]),
    ...mapActions(["Action_RecoverPassword"]),
    FecharTela() {
      this.$emit("fechar-recover", false);
    }
  }
};
</script>