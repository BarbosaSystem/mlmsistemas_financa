<template>
  <transition name="bounce" v-if="Get_CategoriaPorPeriodo != null">
    <template>
      <div>
        <div class="modal-photo">
          <div class="modal-photo-area">
            <div class="botao" style="align-self: flex-end; margin-top: 20px;">
              <button
                class="btn btn-default"
                style="color: white; font-size: 1.5rem;"
                @click="Fechar"
              >
                <i class="fas fa-times"></i>
              </button>
            </div>

            <div class="area" style="background-color: white; width: 80%; align-itens: center; border-radius: 10px">
                <div class="card-header bg-primary" style="color: white">
                  <h2 class="text-center">{{Get_CategoriaPorPeriodo.categoria_Nome}}</h2>
                </div>
                <barra-grafico :itens="Get_CategoriaPorPeriodo.lista | Referencia" :valores="Get_CategoriaPorPeriodo.lista | Dados" ></barra-grafico>
            </div>

            


          </div>
        </div>
        <div class="modal-transparencia"></div>
      </div>
    </template>
  </transition>
</template>
<script>
import { mapActions, mapGetters } from 'vuex'
import BarraGrafico from './Categoria/GraphBarra'
export default {
    components: {
        BarraGrafico
    },
    computed: {
        ...mapGetters(["Get_CategoriaPorPeriodo"])
    },
    filters: {
    Referencia(valor){
      var lista = []
      valor.forEach(element => {
        lista.push(element.referencia)
      });
      return lista
    },
    Dados(valor){
      var lista = []
      valor.forEach(element => {
        lista.push(element.lista_Movimentos)
      });
      return lista
    }
  },
    props: ['codigo'],
    methods: {
        Fechar(){
            this.$emit('FecharCategoriaDetail')
        },
        ...mapActions(["Action_Get_CategoriaPorPeriodo"])
    },
    async mounted() {
        await this.Action_Get_CategoriaPorPeriodo(parseInt(this.codigo))
    },
}
</script>
<style lang="css" scoped>
.modal-photo {
  position: fixed;
  top: 0;
  bottom: 0;
  z-index: 1111;
  right: 0;
  left: 0;
}
.modal-photo-area {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-content: center;
  align-items: center;
  padding: 15px;
}
.modal-transparencia {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1040;
  background-color: #000;
  opacity: 0.65;
  transition: 0.5s linear;
}
.bounce-enter-active {
  animation: bounce-in .5s;
}
.bounce-leave-active {
  animation: bounce-in .3s reverse;
}
@keyframes bounce-in {
  0% {
    opacity: 0;
  }
  
  100% {
    opacity: 1;
  }
}
</style>