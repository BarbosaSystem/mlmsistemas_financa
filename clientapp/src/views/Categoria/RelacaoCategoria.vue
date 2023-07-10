<template>
  <div class="col-12">
    <header-page texto="Relação de Categorias"></header-page>
    <div class="form-group text-left">
      <button class="btn btn-success" @click="NovaCategoria()">
        <i class="fa fa-plus"></i>
        Adicionar Categoria
      </button>
    </div>
    <table class="table table-hover table-bordered table-striped">
<!--       <thead>
        <tr>
          <th>Descrição</th>
          <th>Opção</th>
        </tr>
      </thead> -->
      <tbody id="dados">
        <tr v-for="(item, index) in GetListaCategoria" :key="index" >
          <td>{{item.nome}}</td>
          <td class="text-center">
            
            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
              <button type="button" class="btn btn-info" @click="SelecionarCategoria(item.id)">
                <i class="fas fa-chart-bar"></i>
              </button>
              <button type="button" class="btn btn-warning" @click="EditarCategoria(item)">
                <i class="fas fa-pen-square"></i>
              </button>
              <button type="button" class="btn btn-danger" @click="ExcluirMovimento(item.id)">
                <i class="far fa-minus-square"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <modal />
    <categoria-detail @FecharCategoriaDetail="CategoriaChange" v-if="Zoom && CategoriaSelecionado != 0" :codigo="CategoriaSelecionado"></categoria-detail >
  </div>
</template>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import Modal from "../../components/Categoria/ModalAddOrEdit";
import CategoriaDetail from '../../components/CategoriaDetail.vue';
export default {
  components: {
    Modal,
    CategoriaDetail
  },
  data() {
    return {
      Zoom: false,
      CategoriaSelecionado: 0
    };
  },
  methods: {
    ...mapMutations(["Set_CategoriaPorPeriodo"]),
    ...mapActions(["CarregarCategorias"]),
    SelecionarCategoria(valor){
      this.Set_CategoriaPorPeriodo(null)
      this.CategoriaSelecionado = valor
      if(this.CategoriaSelecionado != 0){
        this.Zoom = true
      }
    },
    CategoriaChange() {
      this.Zoom = !this.Zoom;
    },
    NovaCategoria() {
      this.$root.$emit("ModalAddOrEdit::show");
    },
    EditarCategoria(categoria) {
      this.$root.$emit("ModalAddOrEdit::show", categoria);
    }
  },
  computed: {
    ...mapGetters(["GetListaCategoria"])
  },
  created() {
    this.CarregarCategorias();
  }
};
</script>
