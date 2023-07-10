<template>
<div
      v-if="Get_Movimento_Item"
      class="modal fade"
      id="MovimentoEditModal"
      data-backdrop="static"
      data-keyboard="false"
      tabindex="-1"
      aria-labelledby="MovimentoEditModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="MovimentoEditModalLabel">{{ descricao }}</h5>
            <button
              type="button"
              class="close"
              aria-label="Close"
              @click="OcultarModal()"
            >
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">

            <label for=""><strong>Data:</strong></label>
            <div class="input-group mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="basic-addon1"><i class="fas fa-calendar-alt"></i></span>
              </div>
              <input
                type="datetime-local"
                class="form-control"
                v-model="data"
                aria-describedby="basic-addon1"
              />
            </div>
            <label for=""><strong>Descrição:</strong></label>
            <div class="input-group mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="basic-addon1"><i class="fas fa-calendar-alt"></i></span>
              </div>
              <input
                required
                type="text"
                class="form-control"
                v-model="descricao"
                aria-describedby="basic-addon1"
              />
            </div>
            <label for=""><strong>Valor:</strong></label>
            <div class="input-group mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="basic-addon1">R$</span>
              </div>
              <input
                type="text"
                class="form-control"
                v-model="valor"
                aria-describedby="basic-addon1"
              />
            </div>
            <p>
              <label for><strong>Categoria:</strong></label>
              <select name id class="form-control" v-model="categoria" required>
                <option v-for="(item, index) in GetListaCategoria" :key="index">
                  {{ item.nome }}
                </option>
              </select>
            </p>
          </div>
          <div class="modal-footer">
            <button
              type="reset"
              class="btn btn-defalut"
              @click="OcultarModal()"
            >
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button
              type="button"
              class="btn btn-primary"
              @click="Action_Update_Movimento()"
            >
              <i class="fas fa-upload"></i> Carregar
            </button>
          </div>
        </div>
      </div>
    </div>
<!--   <div>
    
    <div
      class="modal-backdrop fade"
      :class="{ show: Get_Modal_Edit_Movimentacao }"
      :style="{ display: Get_Modal_Edit_Movimentacao ? 'block' : 'none' }"
    ></div>
  </div> -->
</template>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import Service from "../../Service/movimento";
export default {
  data() {
    return {
      ShowModal: false,
    };
  },
  methods: {
    ...mapActions(["Action_Load_Movimento_Item", "Action_Update_Movimento"]),
    ...mapMutations([
      "updateModalEditMovimentacao",
      "updateMovimentoItemData",
      "updateMovimentoItemDescricao",
      "updateMovimentoItemValor",
      "updateMovimentoItemCategoria",
    ]),
    OcultarModal() {
      $("#MovimentoEditModal").modal('toggle')
    },
    MostrarModal() {
      this.ShowModal = true;
    },
    ConvertFloatPure(valor) {
      var valor = parseFloat(valor).toFixed(2);
      return valor.replace(".", ",");
    },

  },
  computed: {
    ...mapGetters([
      "Get_Modal_Edit_Movimentacao",
      "GetListaCategoria",
      "Get_Movimento_Item",
    ]),
    data: {
      get() {
        return this.Get_Movimento_Item.data;
      },
      set(value) {
        this.updateMovimentoItemData(value);
      },
    },
    descricao: {
      get() {
        return this.Get_Movimento_Item.descricao;
      },
      set(value) {
        this.updateMovimentoItemDescricao(value);
      },
    },
    valor: {
      get() {
        return this.Get_Movimento_Item.valor;
      },
      set(value) {
        this.updateMovimentoItemValor(value);
      },
    },
    categoria: {
      get() {
        return this.Get_Movimento_Item.categoria;
      },
      set(value) {
        this.updateMovimentoItemCategoria(value);
      },
    },
  },
};
</script>
<style lang="sass" scoped>

</style>