<template>
  <div>
    <button class="btn btn-primary" @click="Alterar">Carregar</button>

    {{listaArray}}
    <tabela :lista="listaArray"></tabela>
  </div>
</template>
<script>
import TabelaVue from "../../components/Tabela.vue";
export default {
  extends: {

    template: {
      ...TabelaVue.template
    },
    mounted() {
    },
    methods: {
      Alertar(){
        alert('item')
      }
    },
    components: {
      tabela: TabelaVue
    },
    computed: {
      ...TabelaVue.computed
    },
    data() {
      return {
        ...TabelaVue.data(),
        listaArray: [
          {
            userId: 1,
            id: 3,
            title:
              "ea molestias quasi exercitationem repellat qui ipsa sit aut",
            body:
              "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
          }
        ]
      };
    }
  },
  methods: {
    Alterar(){
      this.TabelaVue.methods.Alertar(item);
    }
  },
};
</script>