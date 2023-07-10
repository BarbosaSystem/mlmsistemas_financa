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
            <h5 class="modal-title">{{ItemMovimento.descricao}}</h5>
            <button type="button" class="close" aria-label="Close" @click="OcultarModal()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <p><label for=""><strong>Data:</strong> {{ItemMovimento.data}}</label></p>
            <p><input type="text" class="form-control" v-model="ItemMovimento.descricao"></p>
            <p><label for=""><strong>Valor:</strong> {{ItemMovimento.valor | ConverterFloat}}</label></p>
            <p>
              <label for=""><strong>Categoria:</strong></label>
              <select v-model="ItemMovimento.categoria" class="form-control"> 
                <option v-for="item in categoria" :value="contador+1" :key="item">
                  {{item}}
                </option>
              </select>
            </p>
           

          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-defalut" @click="OcultarModal()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <button type="button" class="btn btn-primary" @click="Cadastrar()" >
              <i class="fa fa-print"></i> Atualizar
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
import filters from '../js/filters'
import Movimento from '../Service/movimento'
import {mapActions, mapGetters} from 'vuex'
export default {
  mixins: [filters],
  data() {
    return {
      ShowModal: false,
      ItemMovimento: {
        id: '',
        data: '',
        descricao: '',
        valor: 0,
        categoria: ''
      },
      contador: 0,
      categoria: ["Pessoal", "Casal", "Welberty", "Matheus", "Pagamento Recebido"],
      selected: ''
    };
  },
  methods: {
    ...mapActions([]),

    Cadastrar(){
      this.OcultarModal();
    },
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
      this.ShowModal = true;
    },
  },
  computed: {
    ...mapGetters([])
  },
  created() {
    this.$root.$on("ModalViewMovimento::show", (movimento) => {
      this.MostrarModal();
      this.ItemMovimento = movimento
    });
    this.$root.$on("ModalViewMovimento::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>