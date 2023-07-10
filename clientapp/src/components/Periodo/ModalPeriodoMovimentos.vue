<template>
  <div>
    <div
      class="modal fade bd-example-modal-xl"
      :class="{show: ShowModal}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: ShowModal ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-xl" role="document">
        <div class="modal-content" v-if="ItemPeriodo.id !=0">
          <div class="modal-header">
            <h5 class="modal-title">{{ItemPeriodo.referencia}}</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="card-group">
              <div class="card">
                <p>
                  <label for>
                    <strong>Data Inicial:</strong>
                    {{ItemPeriodo.DataInicio}}
                  </label>
                </p>
                <p>
                  <label for>
                    <strong>Data Final:</strong>
                    {{ItemPeriodo.DataFinal}}
                  </label>
                </p>
                <p>
                  <label for>
                    <strong>{{ItemPeriodo.movimentacoes.length}} Registros identificados.</strong>
                    
                  </label>
                </p>
                <p>
                  <label for>
                    <button
                      class="btn btn-success"
                      v-if="ItemPeriodo.Status"
                    >Valor: R$ {{ItemPeriodo.Valor}}</button>
                    <button
                      class="btn btn-danger"
                      v-if="!ItemPeriodo.Status"
                      @click="EncerrarPeriodo(ItemPeriodo.id)"
                    >Status: Em aberto</button>
                  </label>
                </p>
              </div>
              <div class="card">
                <grafico
                  @gerar-pdf="GeneratePdf"
                  :itens="itensCategoria"
                  :valores="CategoriasAgrupado"
                ></grafico>
              </div>
            </div>

            <table class="table table-bordered table-striped" >
              <thead>
                <tr>
                  <th>Data</th>
                  <th>Descrição</th>
                  <th>Valor</th>
                  <th>Categoria</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(item, index) in listaMovimentosPeriodo.slice(pagination.currentPage * pagination.itensPage, ((pagination.currentPage * pagination.itensPage) + pagination.itensPage) )"
                  :key="index"
                >
                  <td>{{item.data | ConverterData}}</td>
                  <td>{{item.descricao}}</td>
                  <td>{{ item.valor | ConverterFloat }}</td>
                  <td>{{ item.categoria }}</td>
                </tr>
              </tbody>
            </table>
            <nav aria-label="Page navigation example" v-if="ItemPeriodo.movimentacoes.length > 0">
              <ul class="pagination justify-content-center">
                <!--     ANTERIOR    -->

                <template>
                  <div>
                    <li class="page-item disabled" v-if="anterior">
                      <a class="page-link" href="#">Anterior</a>
                    </li>

                    <li class="page-item" @click="PreviousPage()" v-else>
                      <a class="page-link" href="#">Anterior</a>
                    </li>
                  </div>
                </template>

                <!--     ANTERIOR    -->
                <template>
                  <li
                    class="page-item"
                    v-if="(pagination.currentPage != 0)"
                    @click="ItemAtual(pagination.currentPage - 1)"
                  >
                    <a href="#" class="page-link">{{pagination.currentPage}}</a>
                  </li>
                </template>
                <li class="page-item active">
                  <a href="#" class="page-link">{{pagination.currentPage + 1}}</a>
                </li>
                <template>
                  <li
                    class="page-item"
                    v-if="!(pagination.currentPage == pagination.totalPages - 1)"
                    @click="ItemAtual(pagination.currentPage + 1)"
                  >
                    <a href="#" class="page-link">{{pagination.currentPage + 2}}</a>
                  </li>
                </template>

                <template>
                  <div>
                    <li class="page-item disabled" v-if="ultimo">
                      <a class="page-link" href="#">Próximo</a>
                    </li>
                    <li class="page-item" @click="NextPage()" v-else>
                      <a class="page-link" href="#">Próximo</a>
                    </li>
                  </div>
                </template>
                <!--     PROXIMO    -->

                <!--     PROXIMO    -->
              </ul>
            </nav>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-defalut" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Fechar
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
import Periodo from "../../Service/periodo";
import filters from "../../js/filters";
import mix_pagination from "../../js/mix_pagination";
import Grafico from "../Categoria/GraphPie";
export default {
  components: {
    Grafico
  },
  mixins: [filters, mix_pagination],
  data() {
    return {
      itensCategoria: [],
      ShowModal: false,
      ItemPeriodo: {
        DataInicio: "",
        DataFinal: "",
        Status: "",
        Valor: 0,
        referencia: "",
        id: 0,
        movimentacoes: []
      }
    };
  },
  computed: {
    listaMovimentosPeriodo() {
      var lista_mov = this.ItemPeriodo.movimentacoes;
      return lista_mov;
    },
    CategoriasAgrupado() {
      return this.SomaAgrupada(this.ItemPeriodo.movimentacoes);
    }
  },
  methods: {
    SomaAgrupada(antigo) {
      var resultado = [];

      antigo.reduce(function(novo, item) {
        if (!novo[item.categoria]) {
          novo[item.categoria] = {
            valor: 0,
            nome: item.categoria
          };

          resultado.push(novo[item.categoria]);
        }

        novo[item.categoria].valor += item.valor;

        return novo;
      }, {});

      return resultado;
    },
    async CarregarMovimentacaoPeriodo(codigo) {
      var vm = this;
      Periodo.buscarPeriodo(parseInt(codigo)).then(response => {
        var result = response.data;
        vm.ItemPeriodo = {
          id: result.id,
          DataInicio: result.dataInicio,
          DataFinal: result.dataFinal,
          Status: result.status,
          Valor: result.valor,
          referencia: result.referencia,
          movimentacoes: result.movimentacao
        };
        if(result.movimentacao.length == 0){
          alert("Não foram encontradas relacionados ao período");
          vm.OcultarModal();
        }
        vm.TotalPaginas(vm.ItemPeriodo.movimentacoes);
        vm.itensCategoria = Array.from(
          new Set(vm.ItemPeriodo.movimentacoes.map(s => s.categoria))
        );
      });
    },
    EncerrarPeriodo(codigo) {
      if (!this.ItemPeriodo.status) {
        if (
          confirm(
            "Deseja encerrar o periodo de referência " +
              this.ItemPeriodo.referencia +
              "?"
          )
        ) {
          Periodo.EncerrarPeriodo(parseInt(codigo)).then(response => {
            alert(response.data);
            this.CarregarMovimentacaoPeriodo(codigo);
          });
        }
      }
    },
    OcultarModal() {
      this.ShowModal = false;
      this.ItemPeriodo = {
        DataInicio: "",
        DataFinal: "",
        Status: "",
        Valor: 0,
        referencia: "",
        id: 0,
        movimentacoes: []
      };
    },
    MostrarModal() {
      this.ShowModal = true;
    }
  },
  created() {
    this.$root.$on("ModalPeriodoMovimento::show", codigo => {
      this.CarregarMovimentacaoPeriodo(codigo);
      this.MostrarModal();
    });
    this.$root.$on("ModalPeriodoMovimento::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>
<style lang="css" scoped>
@media screen and (max-width: 442px) {
  table th:nth-child(1) {
    display: none;
  }
  table td:nth-child(1) {
    display: none;
  }
}
.card {
  border: none !important;
}
.modal-body {
  max-height: calc(100vh - 200px);
  overflow-y: auto;
}
</style>