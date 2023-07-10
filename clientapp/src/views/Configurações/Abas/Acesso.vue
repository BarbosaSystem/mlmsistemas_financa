<template>
  <div>
    <div class="form-group text-left mt-5">
      <button class="btn btn-success" @click="Set_Modal_Role" :disabled="false">
        <i class="fa fa-plus"></i>
        Nível de Acesso
      </button>
    </div>
    <table class="table table-bordered table-striped">
      <thead>
        <tr class="text-center">
          <th>Nível</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody id="dados">
        <tr v-for="(item, index) in Get_Roles" :key="index">
          <td>{{item.roleName}}</td>
          <td>
            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
              <button type="button" class="btn btn-warning" @click="ActionGetRole(item)">
                <i class="fas fa-pen-square"></i>
              </button>
              <button type="button" disabled="disabled" class="btn btn-danger">
                <i class="far fa-minus-square"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <modal-role></modal-role>
  </div>
</template>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import ModalRole from "../../../components/Roles/ModalRoles";
export default {
  components: {
    "modal-role": ModalRole
  },
  mounted() {
    this.Action_GetRoles()
  },
  methods: {
    ...mapActions(["Action_GetRoles", "ActionGetRole"]),
    ...mapMutations(["Set_Modal_Role"]),
    AbrirModal() {
      this.$root.$emit("ModalRoles::show");
    }
  },
  computed: {
    ...mapGetters(["Get_Roles"])
  }
};
</script>