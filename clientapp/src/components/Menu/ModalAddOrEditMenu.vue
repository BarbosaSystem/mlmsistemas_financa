<template>
  <div>
    <div
      class="modal fade bd-example-modal-md"
      :class="{show: Get_ShowModal}"
      tabindex="-1"
      role="dialog"
      aria-modal="true"
      :style="{display: Get_ShowModal ? 'block': 'none'}"
    >
      <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <template v-if="Titulo == ''">
               <h5 class="modal-title">Adicionar Menu</h5>
            </template>
            <template v-else>
               <h5 class="modal-title">Editar Menu</h5>
            </template>
            <button type="button" class="close" aria-label="Close" @click="setShow_Modal_Menu()">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body text-left">
            <div class="form-group">
              <label for>Titulo:</label>
              <input v-model="Titulo" type="text" class="form-control" />
            </div>
            <div class="form-group">
              <label for>Rota:</label>
              <input v-model="Rota" type="text" class="form-control" />
            </div>
            <div class="form-group">
              <label for>Ícone:</label>
              <input v-model="Icone" type="text" class="form-control" />
            </div>
            <div class="form-group">
              <label for>Status:</label>
              <label class="switch">
                <input v-model="Status" type="checkbox" class="primary" />
                <span class="slider round"></span>
              </label>
              
            </div>
            <div class="form-group">
              <label for>Nível de Acesso:</label>
              <div class="form-group">
                <menu-roles v-if="Get_ShowModal" :ListaRegras="GET_MenuView.Tags"></menu-roles>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" @click="setShow_Modal_Menu()">
              <i class="fa fa-times-circle"></i> Cancelar
            </button>
            <template v-if="GET_MenuView.Id == 0">
              <button type="button" class="btn btn-primary" @click="Action_Adicionar_Menu()">
              <i class="fa fa-check"></i> Cadastrar
            </button>
            </template>
            <template v-else>
              <button type="button" class="btn btn-primary" @click="Action_Update_Menu()">
              <i class="fa fa-check"></i> Atualizar
            </button>
            </template>
          </div>
        </div>
      </div>
    </div>
    <div
      class="modal-backdrop fade"
      :class="{show: Get_ShowModal}"
      :style="{display: Get_ShowModal ? 'block' : 'none'}"
    ></div>
  </div>
</template>
<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
export default {
  data() {
    return {
      ShowModal: false,
      listaLocal: ['SysAdmin', 'Admin', 'User']
    };
  },
  methods: {
    ...mapMutations(["setShow_Modal_Menu", "update_Titulo", "update_Rota", "update_Icone","update_Status"]),
    ...mapActions(["Action_Adicionar_Menu", "Action_GetMenuById", "Action_Update_Menu"]),
    OcultarModal() {
      this.ShowModal = false;
    },
    MostrarModal() {
    },
 
   },
  computed: {
    ...mapGetters(["GET_MenuView", "Get_ShowModal", "Get_Roles"]),
    
    Titulo: {
      get(){
        return this.GET_MenuView.Titulo
      },
      set(value){
        this.update_Titulo(value)
      }
    },
    Rota: {
      get(){
        return this.GET_MenuView.Rota
      },
      set(value){
        this.update_Rota(value)
      }
    },
    Icone: {
      get(){
        return this.GET_MenuView.Icone
      },
      set(value){
        this.update_Icone(value)
      }
    },
    Status: {
      get(){
        return this.GET_MenuView.Status
      },
      set(value){
        this.update_Status(value)
      }
    }
  },
  created() {
    
    this.$root.$on("ModalAddOrEditMenu::show", menu => {
      if (menu != 0) {
        this.Action_GetMenuById(menu);
      } else {
      }
      this.setShow_Modal_Menu()
    });
    this.$root.$on("ModalAddOrEditMenu::hide", () => {
      this.OcultarModal();
    });
  }
};
</script>