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
            <h5 class="modal-title">{{ItemMovimento.Descricao}}</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <label for>Informar Categoria</label>
            <select v-model="ItemMovimento.CategoriaId" class="form-control">
              <option
                v-for="item in GetListaCategoria"
                :value="item.id"
                :key="item.id"
              >{{item.nome}}</option>
            </select>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-defalut" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button type="button" class="btn btn-primary" @click="Cadastrar()">
              <i class="fas fa-upload"></i> Carregar
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
import filters from "../../js/filters";
import Movimento from "../../Service/movimento";
import Categoria from "../../Service/categoria";
import { mapActions, mapGetters } from "vuex";
export default {
  mixins: [filters],
  data() {
    return {
      ShowModal: false,
      ItemMovimento: {
        Data: "",
        Descricao: "",
        Valor: 0,
        Status: false,
        CategoriaId: 0
      }
    };
  },
  methods: {
    ...mapActions(["CarregarCategorias", "InserirMovimento"]),
    Cadastrar() {
      var vm = this;
      this.OcultarModal();
      this.InserirMovimento({
        Data: this.ItemMovimento.Data,
        Valor: this.ItemMovimento.Valor,
        Descricao: this.ItemMovimento.Descricao,
        Status: false,
        CategoriaId: this.ItemMovimento.CategoriaId
      });
    },
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  computed: {
    ...mapGetters(["GetListaCategoria"])
  },
  async mounted() {
    await this.CarregarCategorias();
  },
  created() {
    this.$root.$on("ModalInfoCategoria::show", movimento => {
      this.ItemMovimento = movimento;
      this.MostrarModal();
    });
    this.$root.$on("ModalInfoCategoria::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>