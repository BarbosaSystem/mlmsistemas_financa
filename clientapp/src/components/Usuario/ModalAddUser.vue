<template>
  <div>
    <div
      class="modal fade bd-example-modal-md"
      :class="{show: ShowModal}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: ShowModal ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Novo Usuário</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
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
      </div>
    </div>
    <div
      class="modal-backdrop fade"
      :class="{show: ShowModal}"
      :style="{display: ShowModal ? 'block' : 'none'}"
    ></div>
  </div>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
export default {
  data() {
    return {
      ShowModal: false
    };
  },
  created() {
    this.$root.$on("ModalAddUser::show", () => {
      this.MostrarModal();
    });
    this.$root.$on("ModalAddUser::hide", () => {
      this.OcultarModal();
    });
  },
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
    ...mapActions(["Action_Registrar", "Action_LimparForm"]),
    ...mapMutations([
      "updateUsername",
      "updateNome",
      "updateSenha",
      "updateEmail",
      "updateConfirmSenha",
    ]),
    OcultarModal() {
        this.Action_LimparForm();
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  }
};
</script>