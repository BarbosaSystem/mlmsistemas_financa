<template>
  <div class="col-12">
    <h1 class="page-header">Relação de Movimentos "Pendentes"</h1>
    <table class="table table-bordered">
      <thead>
        <tr>
          <th>Código</th>
          <th>Data</th>
          <th>Descrição</th>
          <th>Valor</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody id="dados">
        <tr v-for="(item, index) in GETLISTA" :key="index">
          <td>{{item.id}}</td>
          <td>{{item.data}}</td>
          <!-- <td>
            <select name id class="form-control">{{item.categoria}}</select>
          </td> -->
          <td>{{item.descricao}}</td>
          <td>{{ item.valor | ConverterFloat }}</td>
          <td>
            <button class="btn btn-warning" @click="AbrirModal(item)">Carregar</button>
          </td>
        </tr>
      </tbody>
    </table>
    <modal />
  </div>
</template>
<script>

import {mapActions, mapGetters} from 'vuex'
import Modal from "../components/ModalMovimento";
import filters from '../js/filters'
export default {
  mixins: [filters],
  components: {
    Modal
  },
  data() {
    return {
      listaPendentes: []
    };
  },
  methods: {
    ...mapActions(["CarregarPendentes", "CarregarItem"]),
    AbrirModal(item) {
      this.CarregarItem(item);
      this.$root.$emit("ModalViewMovimento::show");
    },
    CarregarPendencias() {
      var vm = this;
      Movimento.listaPendentes().then(Response => {
        vm.listaPendentes = Response.data;
      });
    }
  },
  filters: {
    
  },
  computed: {
    ...mapGetters(["GETLISTA"]),
  },
  created() {
    this.CarregarPendentes();
  }
};
</script>