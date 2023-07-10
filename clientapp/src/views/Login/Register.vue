<template>
  <div class="card login">
    <div class="card-body">
      <header-page texto="Novo Usuário"></header-page>
      <form @submit.prevent="Action_Registrar()">
        <div class="form-row">
          <div class="col-md-6">
            <div class="form-group">
              <label class="small mb-1" for>Nome Completo</label>
              <input
                type="text"
                class="form-control py-4"
                placeholder="Nome Completo"
                v-model="Nome"
                required
              />
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group">
              <label class="small mb-1" for>Apelido (Username)</label>
              <input
                type="text"
                class="form-control py-4"
                placeholder="Nome de Usuário"
                v-model="Username"
                required
              />
            </div>
          </div>
        </div>

        <div class="form-row">
          <div class="col">
            <div class="form-group">
              <label class="small mb-1" for>E-mail</label>
              <input
                type="text"
                class="form-control py-4"
                placeholder="E-mail"
                v-model="Email"
                required
              />
            </div>
          </div>
        </div>
        <div class="form-row">
          <div class="col-md-6">
            <div class="form-group">
              <label for="" class="small mb-1">Senha</label>
              <input type="password" class="form-control py-4" placeholder="Senha" v-model="Senha" />
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group">
              <label for="" class="small mb-1">Senha</label>
              <input
                type="password"
                class="form-control py-4"
                placeholder="Confirmar Senha"
                v-model="ConfirmSenha"
              />
            </div>
          </div>
        </div>
        
        
        <button
          type="submit"
          class="btn btn-primary btn-block mt-3"
          :disabled="!GetIsValid"
        >Cadastrar</button>
      </form>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
export default {
  computed: {
    ...mapGetters(["GetFormulario", "GetIsValid"]),
    Nome: {
      get() {
        return this.GetFormulario.Nome;
      },
      set(value) {
        this.updateNome(value);
      }
    },
    Username: {
      get() {
        return this.GetFormulario.Username;
      },
      set(value) {
        this.updateUsername(value);
      }
    },
    Email: {
      get() {
        return this.GetFormulario.Email;
      },
      set(value) {
        this.updateEmail(value);
      }
    },
    Senha: {
      get() {
        return this.GetFormulario.Senha;
      },
      set(value) {
        this.updateSenha(value);
      }
    },
    ConfirmSenha: {
      get() {
        return this.GetFormulario.ConfirmSenha;
      },
      set(value) {
        this.updateConfirmSenha(value);
      }
    }
  },
  methods: {
    ...mapActions(["Action_Registrar"]),
    ...mapMutations([
      "updateUsername",
      "updateNome",
      "updateSenha",
      "updateEmail",
      "updateConfirmSenha"
    ])
  }
};
</script>
<style lang="sass">
.bg-error
  background-color: lighten(#dc3545, 20% )
  text-color: #fff !important
</style>