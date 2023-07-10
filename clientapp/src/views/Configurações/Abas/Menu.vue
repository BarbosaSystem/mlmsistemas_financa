<template>
  <div>
    <div class="form-group text-left mt-5">
      <button
        class="btn btn-success"
        :disabled="false"
        @click="Action_Create_Menu()"
      >
        <i class="fa fa-plus"></i>
        Adicionar Menu
      </button>
    </div>
    <table class="table table-bordered table-striped">
      <thead>
        <tr class="text-center">
          <th>Titulo</th>
          <th>Perfil</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody id="dados" v-if="Get_Menu.length > 0">
        <tr v-for="(item, index) in Get_Menu" :key="index">
          <td>{{ item.titulo }}</td>
          <td>
            <div  style="display: inline" v-for="(item, index) in item.tags" :key="index">
              <span class="badge badge-secondary" style="margin: 1px; font-size: 100%"   v-if="item.status">
              {{ item.role }}
            </span>
            </div>
            
          </td>
          <td>
            <div
              class="btn-group"
              role="group"
              aria-label="Button group with nested dropdown"
            >
              <button
                type="button"
                @click="NovoMenu(item)"
                class="btn btn-warning"
              >
                <i class="fas fa-pen-square"></i>
              </button>
              <button type="button" disabled="disabled" class="btn btn-danger">
                <i class="far fa-minus-square"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
      <loading-data v-else />
    </table>
  </div>
</template>
<script>
import { mapGetters, mapActions } from "vuex";
export default {
  computed: {
    ...mapGetters(["Get_Menu"]),
  },
  methods: {
    ...mapActions(["Action_Carregar_Menu", "Action_Create_Menu"]),

    NovoMenu(valor) {
      this.$store.dispatch("Action_Load_Edit_Menu", valor);
    },
  },
  mounted() {
    this.Action_Carregar_Menu();
  },
};
</script>