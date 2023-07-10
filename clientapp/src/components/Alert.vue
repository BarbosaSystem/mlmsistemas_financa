<template >
  <div class="alert alert-dismissible fade show" v-if="GetError != null" :class="GetError.tipo">
    <strong>{{ GetError.error[0].code | mensagem}}</strong>

    <button type="button" class="close" @click="OcultarModal()">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
export default {
  filters: {
    mensagem(value){
      if(value == "DuplicateRoleName"){
        return "Já existe este nivel de acesso"
      }
    }
  },
  computed: {
    ...mapGetters(["GetError"])
  },
  data() {
    return {
      ShowAlert: false,
      Message: {
          mensagem: '',
          type: ''
      }
    };
  },
  methods: {
    ...mapActions(["Action_CleanError"]),
    OcultarModal() {
      this.Action_CleanError();
    },
    MostrarModal() {
      this.ShowAlert = true;
    }
  },
};
</script>