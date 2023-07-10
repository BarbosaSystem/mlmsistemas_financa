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
            <h5 class="modal-title">TITULO</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-defalut" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button type="button" class="btn btn-primary" @click="Cadastrar()">
              <i class="fa fa-print"></i> Carregar
            </button>
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
export default {
  mixins: [filters],
  data() {
    return {
      ShowModal: false,
    };
  },
  methods: {
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  created() {
    this.$root.$on("ModalInfoCategoria::show", () => {
      this.MostrarModal();
    });
    this.$root.$on("ModalInfoCategoria::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>
<style lang="sass" scoped>

</style>