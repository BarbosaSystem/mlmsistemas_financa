<template>
  <div>
    <header-page texto="Configurações"></header-page>
    <div class="col">
      <vue-tabs active-tab-color="#2c3e50" 
           active-text-color="white">
        <v-tab  icon="fas fa-list-alt" title="Menu">
          <menu-tab></menu-tab>
        </v-tab>
        <v-tab  icon="fas fa-user" title="Usuários">
          <user-tab></user-tab>
        </v-tab>
        <v-tab  icon="fas fa-user-lock"  title="Nível de Acesso">
          <acesso-tab></acesso-tab>
        </v-tab>
        <v-tab icon="fas fa-upload"  title="Carregar Arquivos">
          <file-upload></file-upload>
        </v-tab>
      </vue-tabs>
    </div>
    <modal-menu></modal-menu>
  </div>
</template>

  
<script>
import { mapActions, mapGetters } from "vuex";
import Menu from './Abas/Menu'
import Usuario from './Abas/Usuários'
import Acesso from './Abas/Acesso'
import fileUpload from '../../components/FileReader'
export default {
  components: {
    "file-upload" : fileUpload,
    "menu-tab": Menu,
    "user-tab" : Usuario,
    "acesso-tab" : Acesso
  },
  data() {
    return {
      Search: "",
      ShowModalAddOrEdit: false
    };
  },
};
</script>