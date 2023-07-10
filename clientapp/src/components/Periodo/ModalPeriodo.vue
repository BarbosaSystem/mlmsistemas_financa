<template>
  <div>
    <div
      class="modal fade bd-example-modal-md"
      :class="{show: GET_Modal_Periodo}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: GET_Modal_Periodo ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <form @submit.prevent="Action_Periodo_Adicionar()">
            <div class="modal-header">
              <h5 class="modal-title">Adicionar Periodo</h5>
              <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
                <span aria-hidden="true">×</span>
              </button>
            </div>
            <div class="modal-body text-left">
              <div class="form-group">
                <label for>
                  <strong>Referência:</strong>
                </label>
                <input type="text" class="form-control" v-model="Referencia" required />
              </div>
              <div class="form-group">
                <label for>
                  <strong>Data Inicial:</strong>
                </label>
                <input type="date" class="form-control" v-model="Data_Inicial" required />
              </div>
              <div class="form-group">
                <label for>
                  <strong>Data Final:</strong>
                </label>
                <input type="date" class="form-control" v-model="Data_Final" required />
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-defalut" @click="OcultarModal()">
                <i class="fa fa-times-circle"></i> Cancelar
              </button>
              <button class="btn btn-primary" type="submit">
                <i class="fa fa-check"></i> Salvar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div
      class="modal-backdrop fade"
      :class="{show: GET_Modal_Periodo}"
      :style="{display: GET_Modal_Periodo ? 'block' : 'none'}"
    ></div>
  </div>
</template>
<script>
import mix_periodo from "../../js/mix_periodo";
import { mapActions, mapMutations, mapGetters } from "vuex";
export default {
  mixins: [mix_periodo],
  data() {
    return {
      ShowModal: false
    };
  },
  methods: {
    ...mapMutations(["SET_Referencia", "SET_Data_Inicial", "SET_Data_Final"]),
    ...mapActions(["Action_Set_Modal_Periodo","Action_Periodo_Adicionar"]),
    OcultarModal() {
      this.Action_Set_Modal_Periodo(false)
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  computed: {
    ...mapGetters(["GET_Periodo", "GET_Modal_Periodo"]),
    Referencia: {
      get() {
        return this.GET_Periodo.referencia;
      },
      set(value) {
        this.SET_Referencia(value);
      }
    },
    Data_Inicial: {
      get() {
        return this.GET_Periodo.DataInicial;
      },
      set(value) {
        this.SET_Data_Inicial(value);
      }
    },
    Data_Final: {
      get() {
        return this.GET_Periodo.DataFinal;
      },
      set(value) {
        this.SET_Data_Final(value);
      }
    }
  },
  created() {
    this.$root.$on("ModalViewPeriodo::show", () => {
      this.Action_Set_Modal_Periodo(true)
    });
    this.$root.$on("ModalViewPeriodo::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>
<style lang="sass" scoped>

</style>