<template>
  <div>
    <div
      class="modal fade bd-example-modal-md"
      :class="{show: Get_Modal_Role}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: Get_Modal_Role ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <form @submit.prevent="Action_SetRole">
            <div class="modal-header">
              <h5 class="modal-title">Nível de Acesso</h5>
              <button type="button" class="close" aria-label="Close" @click="Set_Modal_Role()">
                <span aria-hidden="true">×</span>
              </button>
            </div>
            <div class="modal-body text-left">
              <div class="form-group">
                <input type="text" class="form-control" v-model="RoleName" />
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-defalut" @click="Set_Modal_Role()">
                <i class="fa fa-times-circle"></i> Cancelar
              </button>
              <button :disabled="!Get_Role.RoleName.length >= 4" type="submit" class="btn btn-primary">
                <i class="fa fa-print"></i> Confirmar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div
      class="modal-backdrop fade"
      :class="{show: Get_Modal_Role}"
      :style="{display: Get_Modal_Role ? 'block' : 'none'}"
    ></div>
  </div>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from 'vuex';
export default {
  computed: {
    ...mapGetters(["Get_Modal_Role", "Get_Role"]),
    RoleName: {
      get(){
        return this.Get_Role.RoleName
      },
      set(value){
        this.updateRoleNome(value)
      }
    }
  },
  data() {
    return {
      ShowModal: false
    };
  },
  methods: {
    ...mapActions(["Action_SetRole"]),
    ...mapMutations(["updateRoleNome", "Set_Modal_Role"]),
    OcultarModal() {
      this.RoleName = ""
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
};
</script>
<style lang="sass" scoped>

</style>