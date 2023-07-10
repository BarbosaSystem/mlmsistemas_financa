<template>
  <div>
    <div class="form-group text-left mt-5">
      <button class="btn btn-success" :disabled="false" @click="AbrirModal()">
        <i class="fa fa-plus"></i>
        Adicionar Usuário
      </button>
    </div>
    <table class="table table-bordered table-striped">
      <thead>
        <tr class="text-center">
          <th>Nome de usuário</th>
          <th>Opção</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in Get_List_Users" :key="index">
          <td>{{item.userName}}</td>
          <td><button class="btn btn-info" @click="Modalboots(item.id)"><i class="fas fa-info-circle"></i></button></td>
        </tr>
      </tbody>
    </table>
    <modal-user></modal-user>
    <modal-user-info></modal-user-info>
  </div>
</template>
<script>
import ModalUser from '../../../components/Usuario/ModalAddUser'
import { mapGetters, mapActions } from 'vuex';
export default {
    components: {
        'modal-user' : ModalUser
    },
    mounted() {
      this.Action_Load_Users()
    },
    methods: {
      
      ...mapActions(["Action_Load_Users"]),
        AbrirModal(){
            this.$root.$emit("ModalAddUser::show");
        },
        async Modalboots(codigo){
          this.$store.commit("updateLoadingImage")
          this.$store.dispatch("Action_Load_User", codigo)
          $("#staticBackdrop").modal('toggle')
        }
    },
    computed: {
      ...mapGetters(["Get_List_Users"])
    },
};
</script>
