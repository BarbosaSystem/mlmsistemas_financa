<template>
  <div class="col">
    <header-page  texto="Relação de Períodos"></header-page>
    <div class="form-group text-left">
      <button class="btn btn-primary" @click="AbrirModalPeriodo()">
        <i class="fa fa-plus"></i>
        Novo Periodo
      </button>
    </div>
    <info-box
      @load-info="MostrarMovimentosPeriodo"
      :objeto="item"
      v-for="(item, index) in GET_Lista_Periodo"
      :key="index"
    ></info-box>
    <modal-periodo />
    <periodo-movimento />
  </div>
</template>
<script>
import ModalPeriodo from "../../components/Periodo/ModalPeriodo";
import PeriodoMovimento from "../../components/Periodo/ModalPeriodoMovimentos";
import mixPeriodo from "../../js/mix_periodo";
import filters from "../../js/filters";
import InfoBox from "../../components/SHARED/info-box";
import { mapGetters, mapActions } from "vuex";

export default {
  mixins: [filters],
  components: {
    "info-box": InfoBox,
    ModalPeriodo,
    PeriodoMovimento
  },
  filters: {
    situacao(valor) {
      if (valor) {
        return "Fechado";
      } else {
        return "Em aberto";
      }
    }
  },
  methods: {
    ...mapActions(["Action_Periodo_Carregar"]),
    AbrirModalPeriodo() {
      this.$root.$emit("ModalViewPeriodo::show");
    },
    MostrarMovimentosPeriodo(codigo) {
      this.$root.$emit("ModalPeriodoMovimento::show", codigo);
    }
  },
  computed: {
    ...mapGetters(["GET_Lista_Periodo"])
  },
  created() {
    this.Action_Periodo_Carregar();
    
  }
};
</script>
<style lang="css" scoped>
@media screen and (max-width: 442px) {
  table th:nth-child(3) {
    display: none;
  }
  table td:nth-child(3) {
    display: none;
  }
}
</style>