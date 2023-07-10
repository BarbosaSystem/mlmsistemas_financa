<template>
  <div class="col mt-5">
    <div class="card">
      <div class="card-body">
        <div class="form-group text-center">
          <button class="btn btn-primary" @click="ClicarBotao()">
            <i class="fas fa-file-upload"></i> Carregar Arquivo
          </button>
          <label style="display: none;">
            <input type="file" ref="loadFromFile" @change="loadTextFromFile" />
          </label>
        </div>
        <div class="form-group text-center mt-4" v-if="GET_Unique_Select">
          <button class="btn btn-success" @click="SelectItens()">
            <i class="fas fa-upload"></i> Carregar Selecionados
          </button>
        </div>
      </div>
    </div>
    <div class="form-group mt-2" v-if="listaMov.length > 0">
          <p class="btn">{{listaMov.length}} registros encontrado(s)</p>
        </div>
    <table class="table table-bordered table-condensed" v-if="listaMov.length > 0">
      <thead>
        <tr>
          <th></th>
          <th>Data</th>
          <th>Descrição</th>
          <th>Valor</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody id="dados">
        <tr @dblclick="ChangeItem(index)" v-for="(item, index) in listaMov" :key="index" :class="{'sucesso' : item.Select }">
          <td>
            <label class="switch">
              <input
                type="checkbox"
                class="primary"
                :checked="item.Select"
                @click="ChangeItem(index)"
              />
              <span class="slider round"></span>
            </label>
          </td>
          <td>{{item.Data | ConverterData}}</td>
          <td>{{item.Descricao}}</td>
          <td>{{ item.Valor | ConverterFloat }}</td>
          <td class="text-center">
            <button
              class="btn btn-warning m-1"
              :disabled="item.Select"
              v-if="!item.Select"
              @click="ADD_Movimento(item)"
            ><i class="fas fa-upload"></i></button>
            <button class="btn btn-danger m-1" @click="RemoverPendente(item)"><i class="fas fa-times"></i></button>
          </td>
        </tr>
      </tbody>
    </table>
    <modal-movimento></modal-movimento>
    <modal-list></modal-list>
  </div>
</template>
<style scoped>
@media screen and (max-width: 442px) {
  table th:nth-child(5) {
    display: none;
  }
  table th:nth-child(1) {
    display: none;
  }
  table td:nth-child(5) {
    display: none;
  }
  table td:nth-child(1) {
    display: none;
  }
}
.nav-link{
  padding: 0rem 1rem !important;
}
.sucesso {
  background-color: #27ae60;
  color: white;
  font-style: italic;
}
.error {
  background-color: lightsalmon;
}
</style>
<script>
import ModalMovimento from "../components/Movimento/ModalInfoCategoria.vue";
import ModalList from "../components/Movimento/ModalInfoListCategoria.vue";
import loadCsv from "../js/loadCsv";

export default {
  mixins: [loadCsv],
  components: {
    ModalMovimento,
    ModalList
  },

  data() {
    return {
      status: null,
      lista: [],
      contador: 0,
      message: {
        titulo: "",
        status: ""
      }
    };
  },
  computed: {
    listaMov() {
      return this.GETLISTA;
    }
  },
  filters: {
    ConverterData(valor) {
      var dataString = valor.split("-");
      return dataString[2] + "/" + dataString[1] + "/" + dataString[0];
    },
    ConverterFloat(valor) {
      var valor = parseFloat(valor).toFixed(2);
      return "R$ " + valor.replace(".", ",");
    }
  },
  methods: {
    ClicarBotao(){
      this.$refs.loadFromFile.click();
    },
    SelectItens() {
      this.$root.$emit("ModalInfoListCategoria::show");
    },
    ChangeItem(item) {
      this.$store.dispatch("checkedItem", item);
    },
    checkClass(item) {
      if (item.Select) return "sucesso";
    },
    ADD_Movimento(item) {
      var movimento = {
        Data: item.Data,
        Descricao: item.Descricao,
        Valor: parseFloat(item.Valor).toFixed(2),
        Status: false,
        CategoriaId: 0
      };
      this.$root.$emit("ModalInfoCategoria::show", movimento);
    }
  }
};
</script>