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
          <div class="modal-header" :class="[Status ? 'bg-success' : 'bg-danger', !Status]">
            <h5 class="modal-title">{{titulo}}</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="form-group">
              <label for>
                <strong>Descrição:</strong>
              </label>
              <input v-model="Movimento.Descricao" type="text" class="form-control" />
            </div>
            <div class="form-group">
              <label for>
                <strong>Data:</strong>
              </label>
              <input type="Date" v-model="Movimento.Data" class="form-control" />
            </div>
            <div class="form-group">
              <label for>
                <strong>Valor:</strong> (Somente números)
              </label>
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">R$</span>
                </div>
                <input
                  type="decimal"
                  class="form-control text-right"
                  v-model="Movimento.Valor"
                  placeholder="0,00"
                  aria-label="Amount (to the nearest dollar)"
                  @keypress="stripTheGarbage"
                  @blur="formatDollars"
                  @focus="Foco"
                />
              </div>
            </div>
            <div class="form-group">
              <label for>
                <strong>Situação:</strong>
              </label>
              <label class="switch">
                <input type="checkbox" class="success" v-model="Status" />
                <span class="slider round"></span>
              </label>
            </div>
            <div class="form-group">
              <label for>
                <strong>Observação:</strong>
              </label>
              <textarea v-model="Movimento.Observacao" rows="5" class="form-control"></textarea>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-defalut" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button type="button" class="btn btn-primary" @click="Cadastrar">
              <i class="fas fa-upload"></i> Confirmar
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
export default {
  data() {
    return {
      ShowModal: false,
      Movimento: {
        Descricao: "",
        Observacao: "",
        Valor: "",
        Status: false,
        Data: new Date().toISOString().slice(0, 10)
      },
      titulo: "Entrada",
      Status: false
    };
  },
  methods: {
    Foco() {
      this.Movimento.Valor = ""
    },
    Validar() {
      let zero_valor = "0,";
      let valor_antigo = this.Movimento.Valor;
    },
    stripTheGarbage(e) {
      if ((e.keyCode < 48 && e.keyCode !== 46) || e.keyCode > 59) {
        e.preventDefault();
      }
      if((e.keyCode == 48) && (this.Movimento.Valor.length == 0)){
          e.preventDefault();
      }
    },
    formatDollars() {
      if (this.Movimento.Valor != "") {
        var valor = this.Movimento.Valor;
        var centavos = valor.substr(valor.length - 2);
        var inteiro = valor.substring(0, valor.length - 2);
        var result = inteiro + "." + centavos;

        var numero = result.split(".");
        numero[0] = numero[0].split(/(?=(?:...)*$)/).join(".");
        this.Movimento.Valor = numero.join(",");
      }
    },
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    },
    ConvertFloatPure(valor) {
      var valor = parseFloat(valor).toFixed(2);
      return valor.replace(".", ",");
    }
  },
  created() {
    this.$root.$on("ModalMovimentoValor::show", codigo => {
      this.MostrarModal();
    });
    this.$root.$on("ModalMovimentoValor::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>
<style lang="sass" scoped>
.bg-danger, .bg-success
    color: white
textarea
    resize: none
</style>