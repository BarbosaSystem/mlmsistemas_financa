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
            <h5 class="modal-title">{{Titulo}}</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="form-group">
              <label for>Nome:</label>
              <input v-model="Nome" type="text" class="form-control" />
              
            </div>
            <div class="form-group">
              <label for>Status:</label>
              <label class="switch">
                <input
                  type="checkbox"
                  class="primary"
                />
                <span class="slider round"></span>
              </label>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button type="button" class="btn btn-primary" @click="AddOrUpdate()">
              <i class="fa fa-check"></i> Atualizar
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
import { mapActions, mapGetters, mapMutations } from "vuex";
export default {
  data() {
    return {
      Titulo: "",
      ShowModal: false,
    };
  },
  methods: {
    ...mapActions(["AdicionarOuAtualizarCategoria", "Action_Categoria"]),
    ...mapMutations(["SET_CATEGORIA_NOME", "SET_CATEGORIA"]),
    AddOrUpdate() {
      this.AdicionarOuAtualizarCategoria(this.ItemCategoria);
      this.OcultarModal();
    },
    OcultarModal() {
      this.SET_CATEGORIA({id: 0, nome: ""});
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  computed: {
    ...mapGetters(["Get_Categoria"]),
    Id: {
      get() {
        return this.Get_Categoria.Id;
      },
    },
    Nome: {
      get() {
        return this.Get_Categoria.Nome;
      },
      set(value) {
        this.SET_CATEGORIA_NOME(value);
      }
    }
  },
  created() {
    this.$root.$on("ModalAddOrEdit::show", categoria => {
      if (categoria != undefined) {
        this.Titulo = "Editar Categoria";
        this.Action_Categoria(categoria);
      } else {
        this.Titulo = "Adicionar Categoria";
      }
      this.MostrarModal();
    });
    this.$root.$on("ModalAddOrEdit::hide", () => {
      
      this.OcultarModal();
    });
  }
};
</script>
<style lang="sass" scoped>

</style>