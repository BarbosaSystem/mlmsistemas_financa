<template>
  <div class="col-12 rel_movimentos">
    <header-page texto="Lançamentos"></header-page>
    <div class="form-group">
      <input
          type="search"
          placeholder="Pesquisar..."
          v-model="searchItem"
          class="form-control"
          @change="GetMovimentos(searchItem)"
        />
      <!-- <div class="input-group mb-3">
        <div class="input-group-prepend">
          <select class="custom-select" id="inputGroupSelect01" v-model="searchItem.id">
            <option selected>Selecione...</option>
            <option value="1">Descrição</option>
            <option value="2">Data</option>
            <option value="3">Valor</option>
            <option value="4">Categoria</option>
          </select>
        </div>
        
      </div> -->
      <strong
        ><small
          >{{ GetMovimentos(searchItem).length }} - registro(s)
          encontrado(s).</small
        ></strong
      >
      <br />
      <strong
        ><small>{{ Total }}</small></strong
      >
    </div>
    <div class="form-group">
      <table class="table table-striped">
        <thead>
          <tr class="text-center">
            <th>Descrição</th>
            <th style="cursor: pointer">
              Data
              <span><strong></strong></span>
              <button-order Item="Data"></button-order>
            </th>

            <th style="cursor: pointer">
              Valor
              <button-order Item="Valor"></button-order>
            </th>
            <th style="cursor: pointer">
              Categoria
              <button-order Item="Categoria"></button-order>
            </th>
            <th>Opção</th>
          </tr>
        </thead>
        <tbody id="dados" v-if="GetMovimentos(searchItem).length > 0">
          <tr
            class="text-center"
            v-for="(item, index) in GetMovimentos(searchItem).slice(
              pagination.currentPage * pagination.itensPage,
              pagination.currentPage * pagination.itensPage +
                pagination.itensPage
            )"
            :key="index"
          >
            <td><span>Descrição:</span> {{ item.descricao }}</td>
            <td><span>Data: </span>{{ item.data | ConverterData }}</td>

            <td>R$ {{ item.valor }}</td>
            <td>
              <span><strong>Categoria: </strong></span>{{ item.categoria }}
            </td>
            <td>
              <div
                class="btn-group"
                role="group"
                aria-label="Button group with nested dropdown"
                style="width: 100%"
              >
                <button
                  type="button"
                  class="btn btn-warning"
                  @click="AbrirModalEditarMovimento(item)"
                >
                  <i class="fas fa-pen-square"></i>
                </button>
                <button
                  type="button"
                  class="btn btn-danger"
                  @click="ExcluirMovimento(item.id)"
                  disabled
                >
                  <i class="far fa-minus-square"></i>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
        <loading-data v-else />
      </table>
    </div>
    <nav
      aria-label="Page navigation example"
      v-if="GetMovimentos(searchItem).length > 0"
    >
      <ul class="pagination justify-content-center">
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
        <template>
          <li
            class="page-item"
            v-if="pagination.currentPage != 0"
            @click="ItemAtual(pagination.currentPage - 1)"
          >
            <a href="#" class="page-link">{{ pagination.currentPage }}</a>
          </li>
        </template>
        <li class="page-item active">
          <a href="#" class="page-link">{{ pagination.currentPage + 1 }}</a>
        </li>
        <template>
          <li
            class="page-item"
            v-if="!(pagination.currentPage == pagination.totalPages - 1)"
            @click="ItemAtual(pagination.currentPage + 1)"
          >
            <a href="#" class="page-link">{{ pagination.currentPage + 2 }}</a>
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
    <!-- <modal /> -->
    <modal-edit></modal-edit>
  </div>
</template>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import filters from "../../js/filters";
import Mix_Pagination from "../../js/mix_pagination";
import ButtonOrderVue from "../../components/ButtonOrder.vue";
export default {
  components: {
    "button-order": ButtonOrderVue,
  },
  methods: {
    ...mapActions([
      "ActionLoadList",
      "Action_Load_Movimento_Item",
      "Action_Buscar",
    ]),
    DownloadFile() {
      var file = new Blob([this.listaMovimentos], { type: "text/plain" });
      window.open(file);
    },
    AbrirModalEditarMovimento(movimento) {
      this.Action_Load_Movimento_Item(movimento);
      $("#MovimentoEditModal").modal("toggle");
    },
  },
  mixins: [filters, Mix_Pagination],
  data() {
    return {
      ValorOrder: false,
      searchItem: ''
    };
  },
  computed: {
    ...mapGetters(["GetMovimentos", "Get_Modal_Edit_Movimentacao", "GetListaCategoria"]),
    Total() {
      var lista = this.GetMovimentos(this.searchItem).map((x) => {
        return parseFloat(x.valor.replace(",", "."));
      });
      var resultado = lista.reduce((a, b) => a + b, 0).toFixed(2);

      return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
      }).format(resultado);
    },
    OrderValor() {
      this.ValorOrder = !this.ValorOrder;
      if (this.ValorOrder) {
        return "Asc";
      } else {
        return "Desc";
      }
    },
  },
  async created() {
/*     var vm = this;
    await this.$store.dispatch("Action_LoadList").then(()=> {
      this.$store.dispatch("Action_Categoria")
    }); */
    const promisses = [this.$store.dispatch("Action_LoadList"),]
    await Promise.allSettled(promisses).then(()=>{
      this.TotalPaginas(this.GetMovimentos(this.searchItem.value));
    })
    
  },
};
</script>
<style lang="css">
@media screen and (max-width: 442px) {
  table th:nth-child(5) {
    display: none;
  }
  /* table td:nth-child(5) {
    display: none;
  } */
}
#searchinput {
  width: 200px;
}
#searchclear {
  position: absolute;
  right: 5px;
  top: 0;
  bottom: 0;
  height: 14px;
  margin: auto;
  font-size: 14px;
  cursor: pointer;
  color: #ccc;
}
</style>
