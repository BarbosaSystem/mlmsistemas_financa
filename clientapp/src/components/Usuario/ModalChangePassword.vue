<template>
  <div>
    <div
      class="modal fade bd-example-modal-md"
      :class="{show: Get_Modal_Change}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: Get_Modal_Change ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <div class="modal-header bg-primary" style="color: white">
            <h5 class="modal-title">Trocar Senha</h5>
            <button type="button" class="close" aria-label="Close" @click="Set_Modal_Change">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="form-group">
              <input
                type="password"
                class="form-control mt-3"
                :disabled="Progress"
                v-model="SenhaAntiga"
                placeholder="Senha Atual"
              />
              <input
                type="password"
                class="form-control mt-3"
                :disabled="Progress"
                v-model="NovaSenha"
                placeholder="Nova Senha"
              />
              <input
                type="password"
                class="form-control mt-3"
                :disabled="Progress"
                v-model="ConfirmaSenha"
                placeholder="Confirmação de Senha"
              />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" @click="Set_Modal_Change()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button
              type="button"
              class="btn btn-primary"
              :disabled="(Get_Correct_Password)"
              @click="Cadastrar()"
            >
              <i class="fa fa-check"></i> Carregar
            </button>
          </div>
        </div>
      </div>
    </div>
    <div
      class="modal-backdrop fade"
      :class="{show: Get_Modal_Change}"
      :style="{display: Get_Modal_Change ? 'block' : 'none'}"
    ></div>
  </div>
</template>
<script>
import { mapMutations, mapGetters, mapActions } from "vuex";
export default {
  props: ["Id"],
  data() {
    return {
      Progress: false
    };
  },
  computed: {
    ...mapGetters([
      "Get_Modal_Change",
      "Get_Correct_Password",
      "Get_OldPassword",
      "Get_NewPassword",
      "Get_ConfirmPassword"
    ]),
    SenhaAntiga: {
      get() {
        return this.Get_OldPassword;
      },
      set(value) {
        this.updateOldPassword(value);
      }
    },
    NovaSenha: {
      get() {
        return this.Get_NewPassword;
      },
      set(value) {
        this.updateNewPassword(value);
      }
    },
    ConfirmaSenha: {
      get() {
        return this.Get_ConfirmPassword;
      },
      set(value) {
        this.updateConfirmPassword(value);
      }
    }
  },
  watch: {
    Get_Modal_Change(value) {
      if (value) {
        this.updateOldPassword("");
        this.updateNewPassword("");
        this.updateConfirmPassword("");
        this.Progress = false;
      }
    }
  },
  methods: {
    ...mapMutations([
      "Set_Modal_Change",
      "updateOldPassword",
      "updateNewPassword",
      "updateConfirmPassword"
    ]),
    ...mapActions(["Action_Change_Password"]),
    Cadastrar() {
      var vm = this;
      this.Progress = !this.Progress;
      this.Action_Change_Password(this.Id).then(response => {
        vm.Set_Modal_Change();
      });
    },
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  }
};
</script>
<style lang="sass" scoped>

</style>