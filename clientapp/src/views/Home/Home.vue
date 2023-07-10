<template>
  <div>
    <header-page texto="Página Inicial"></header-page>
    <div class="col mt-5">
      <div class="card-group">
        <div class="card">
          <h5 class="card-header bg-primary text-white">Últimos Lançamentos</h5>
          <table class="table table-striped">
        <tbody id="dados" v-if="listaUltimos">
          <tr
            v-for="(item, index) in listaUltimos"
            :key="index" style="font-size: .8rem;"
          >
            <td>
              <span>Descrição:</span> {{ item.descricao }}
            </td>
            <td>
              <span>Data: </span
              >{{ item.data }}
            </td>
            
            <td>{{ item.valor | ConverterFloat }}</td>
            <td>
              <span><strong>Categoria: </strong></span>{{ item.categoria }}
            </td>
            
          </tr>
        </tbody>
        <loading-data v-else />
      </table>

          <!-- <ul class="list-group">
             <li
              class="list-group-item skeleton skeleton-text"
              v-for="(item, index) in listaUltimos"
              :key="index"
            >
              {{ item.data }} - {{ item.descricao }} -
              {{ item.valor | ConverterFloat }} - {{ item.categoria }}
            </li>
          </ul> -->
        </div>
        <div class="card">
          <h5 class="card-header bg-primary text-white">Último Período</h5>
          <div class="card-body">
            <h5 class="card-title text-center">{{UltimoPeriodo.referencia}}</h5>
            <div class="linha-periodo">
              <m-data-periodo-principal :icon="UltimoPeriodo.inicio ? 'fas fa-calendar-alt' : '' " position="center" :label="UltimoPeriodo.inicio ? 'Início' : '' " :dia="UltimoPeriodo.inicio"></m-data-periodo-principal>
              <m-data-periodo-principal :icon="UltimoPeriodo.fim ? 'fas fa-calendar-alt' : ''" position="center" :label="UltimoPeriodo.fim ? 'Final' : '' " :dia="UltimoPeriodo.fim"></m-data-periodo-principal>
              <m-data-periodo-principal :icon="UltimoPeriodo.fim ? 'fa-money-bill-wave': ''" position="center" :label="UltimoPeriodo.valor ? 'Total' : '' " :dia="UltimoPeriodo.valor | ConverterFloat"></m-data-periodo-principal>
              <div class="linha-periodo-progress">

                <div class="progress skeleton skeleton-text mt-2" v-for="(item, index) in UltimoPeriodo.categoria" 
                  :key="index" role="progressbar" data-placement="top" data-toggle="tooltip" :title="`${item.nome_Categoria}`">
                  
                  <div 
                  class="progress-bar progress-bar-animated progress-bar-striped" 
                  
                  style="width: 0%;" aria-valuemin="0" ref="progresso">
                    {{item.nome_Categoria}}: {{item.valor | ConverterFloat}}
                    </div>
                </div>
              </div> 
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col mt-5">
      <div class="card-group">
        <div class="card">
          <div class="text-center pt-5">
            <i class="fas fa-4x fa-calendar-alt"></i>
          </div>
          <div class="card-body">
            <h5 class="card-title">Períodos</h5>
            <p class="card-text">
              This is a wider card with supporting text below as a natural
              lead-in to additional content. This content is a little bit
              longer.
            </p>
            <p class="card-text">
              <small class="text-muted">Last updated 3 mins ago</small>
            </p>
          </div>
        </div>
        <div class="card">
          <div class="text-center pt-5">
            <i class="fas fa-4x fa-money-bill-wave"></i>
          </div>
          <div class="card-body">
            <h5 class="card-title">Movimentação</h5>
            <p class="card-text">
              This is a wider card with supporting text below as a natural
              lead-in to additional content. This content is a little bit
              longer.
            </p>
            <p class="card-text">
              <small class="text-muted">Last updated 3 mins ago</small>
            </p>
          </div>
        </div>
        <div class="card">
          <div class="text-center pt-5">
            <i class="fas fa-4x fa-upload"></i>
          </div>
          <div class="card-body">
            <h5 class="card-title">Carregar arquivos (Upload)</h5>
            <p class="card-text">
              This is a wider card with supporting text below as a natural
              lead-in to additional content. This content is a little bit
              longer.
            </p>
            <p class="card-text">
              <small class="text-muted">Last updated 3 mins ago</small>
            </p>
          </div>
        </div>
      </div>
    </div>
    
  </div>
</template>
<style>
.skeleton:empty {
  opacity: .7;
  animation: skeleton-loading 1s linear infinite alternate;
  
}

.skeleton-text:empty {
  width: 100%;
  height: .5rem;
  margin-bottom: .25rem;
  border-radius: .125rem;
  height: 49px;
  padding: 5px 10px;
}

@keyframes skeleton-loading {
  0% {
    background-color: hsl(200, 20%, 70%);
  }

  100% {
    background-color: hsl(200, 20%, 95%);
  }
}
</style>
<script>
import filters from "../../js/filters";
import { mapActions, mapGetters } from "vuex";
import colorsBackground from '../../js/functions';
export default {

  mixins: [filters],
  data() {
    return {
    };
  },
  methods: {
    ...mapActions(["CarregarCincoUltimosMovimentos", "Action_Periodo_Ultimo"]),
    CalcPorCento(valor, total){
      return valor * 100 / total;
    },
    QuantidadeArray(){
      const items = this.UltimoPeriodo.categoria.map((categoria) => (
        {
          valor: categoria.valor,
          cor: categoria.cor
        } 
      ))
      const cor_items = items.map((cor) => { return cor.cor })
      const soma_items = items.reduce((a, b) => a + b.valor, 0)

      for (let index = 0; index < items.length; index++) {
        this.$refs.progresso[index].style.backgroundColor = cor_items[index]
        this.$refs.progresso[index].style.width = this.CalcPorCento(items[index].valor, soma_items) + '%'
      }
    }
  },
  async mounted() {
    const promisses = [this.CarregarCincoUltimosMovimentos(), this.Action_Periodo_Ultimo()]
    await Promise.allSettled(promisses).then(() => {
      this.QuantidadeArray()
    });
  },
  
  computed: {
    ...mapGetters(["GET_CINCO_ULTIMOS", "Get_UltimoPeriodo"]),
    
    listaUltimos() {
      return this.GET_CINCO_ULTIMOS;
    },
    UltimoPeriodo(){
      return this.Get_UltimoPeriodo
    },


  },
};
</script>