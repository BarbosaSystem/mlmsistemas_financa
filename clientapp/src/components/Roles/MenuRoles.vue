<template>
  <div>
    <button
      class="btn mr-1"
      v-for="(item, index) in ListaRegras" @click="Set_Tag(index)"
      :key="index" :class="[{'btn-primary': item.status, 'btn-warning' : !item.status}]"
    >
      <i class="fas" :class="[{'fa-check' : item.status, 'fa-times' : !item.status}]" ></i> {{ item.role }}
    </button>
  </div>
</template>
<script>
import { mapGetters, mapMutations } from 'vuex';
export default {
  props: ["ListaRegras"],
  methods: {
    ...mapMutations(["Set_Tag"])
  },
  computed: {
      ...mapGetters(["Get_Roles"]),
      CheckRole(){
            var resultado = []
            var roles = this.Get_Roles
            var lista = this.ListaRegras

            roles.forEach(element => {
                if(lista.find(x => x == element.roleName)){
                    resultado.push({ role: element.roleName, status: true})
                }else{
                    resultado.push({ role: element.roleName, status: false})
                }
            })
    
            return resultado
      }
  },

};
</script>