<template>
  <div class="info-box bg-white" >
    <div class="info-box-content" :class="[objeto.status ? 'bg-success' : 'bg-danger', !objeto.status]" style="color: white">
      <span class="info-box-icon">
        <button class="btn btn-blocked" @click="CarregarPeriodoInfo(objeto.id)">
          <i class="fas fa-info"></i>
        </button>
      </span>
      <span class="info-box-text">{{objeto.referencia}}</span>
      <span class="info-box-number">{{ objeto.valor | ConverterFloat }}</span>
    </div>
  </div>
</template>
<script>
import { mapActions } from "vuex";
export default {
  props: ["objeto"],
  methods: {
    ...mapActions(["Action_Periodo_Carregar"])
  },
  filters: {
    ConverterFloat(valor) {
      var valor = parseFloat(valor).toFixed(2);
      return "R$ " + valor.replace(".", ",");
    }
  },
  methods: {
    CarregarPeriodoInfo(valor) {
      this.$emit("load-info", valor);
    }
  }
};
</script>
<style lang="scss" scoped>
</style>