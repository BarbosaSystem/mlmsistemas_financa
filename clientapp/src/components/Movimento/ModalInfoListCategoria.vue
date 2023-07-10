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
            <h5 class="modal-title">{{listaItensMovimento.length}} registros selecionados.</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="accordion" id="accordionExample">
              <div class="card">
                <div class="card-header" id="headingOne">
                  <h2 class="mb-0">
                    <button
                      class="btn btn-link"
                      type="button"
                      @click="ChangeCollapse()"
                    >Exibir Itens</button>
                  </h2>
                </div>

                <div
                  id="collapseOne"
                  class="collapse" :class="{show: Collapse}"
                  aria-labelledby="headingOne"
                  data-parent="#accordionExample" style="overflow-y: scroll !important;
max-height: 300px !important;"
                >
                  <div class="card-body">
                    <ul class="list-group">
                      <li
                        class="list-group-item"
                        v-for="(item, index) in listaItensMovimento"
                        :key="index"
                      >{{item.Descricao}} - {{item.Valor | ConverterFloat}}</li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>

            <label for>Informar Categoria</label>
            <select v-model="CategoriaId" class="form-control">
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
      Collapse: false,
      ShowModal: false,
      listaItensMovimento: [],
      CategoriaId: 0
    };
  },
  methods: {
    ...mapActions(["CarregarCategorias", "InserirListaMovimentos"]),
    ChangeCollapse(){
      this.Collapse = !this.Collapse
    },
    Cadastrar() {
      var vm = this;
      var listPush = [];
      this.listaItensMovimento.forEach(element => {
        var movimento = {
          Data: element.Data,
          Descricao: element.Descricao,
          Valor: element.Valor,
          CategoriaId: this.CategoriaId
        };
        listPush.push(movimento);
      });
      this.InserirListaMovimentos(listPush);
      this.OcultarModal();
    },
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  computed: {
    ...mapGetters(["GetListaCategoria", "GET_SELECT_ITEMS"])
  },
  async mounted() {
    await this.CarregarCategorias();
  },
  created() {
    this.$root.$on("ModalInfoListCategoria::show", () => {
      this.listaItensMovimento = this.GET_SELECT_ITEMS;
      this.MostrarModal();
    });
    this.$root.$on("ModalInfoListCategoria::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>