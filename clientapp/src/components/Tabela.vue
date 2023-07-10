<template>
  <div>

    <table class="table table-bordered">
      <thead>
        <tr class="text-center">
          <th v-for="item in listaChave" :key="item">{{item}}</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in listagem" :key="index">
          <td
            v-for="(count, count_index) in  listaChave"
            :key="count_index"
          >{{ item[listaChave[count_index]] }}</td>
          <td>
            <button class="btn btn-info" @click="Alertar(item)">info</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  name: "tabela",
  props: ['lista'],
  computed: {
    ...mapGetters(["GetPagination", "GetDados"]),
    pagination() {
      return this.GetPagination;
    },
    chaves() {
      return Object.keys(this.listaArray);
    },
    listaChave() {
      return Object.keys(Object.assign({}, ...this.listaArray));
    },
    listagem() {
      return this.lista;
    }
  },

  data() {
    return {
      Unico: {
        userId: 1,
        id: 1,
        title:
          "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
        body:
          "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
      },
      listaArray: [
        {
          userId: 1,
          id: 1,
          title:
            "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
          body:
            "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
        },
        {
          userId: 1,
          id: 2,
          title: "qui est esse",
          body:
            "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
        },
        {
          userId: 1,
          id: 3,
          title: "ea molestias quasi exercitationem repellat qui ipsa sit aut",
          body:
            "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
        }
      ]
    };
  },
  methods: {
    Alertar(item) {
      alert(JSON.stringify(item));
    }
  },
  mounted() {}
};
</script>