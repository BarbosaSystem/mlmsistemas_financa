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
            <img src="../assets/logo-mlm.png" class="img-thumbnail" alt="">
            
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body">
              <login></login>
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
import login from "../views/Login/Login";
export default {
  components: {
    login
  },
  data() {
    return {
      ShowModal: false
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
    this.$root.$on("ModalLogin::show", () => {
      this.MostrarModal();
    });
    this.$root.$on("ModalLogin::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>
<style lang="sass" scoped>

</style>