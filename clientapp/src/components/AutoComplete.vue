<template>
  <div class="wrapper">
    <header-page texto="Perfil de Acesso"></header-page>
    <div class="form-group">
      <label
        class="badge badge-primary"
        @click="Remover(index)"
        v-for="(items, index) in listaItens"
        :key="index"
      >
        {{items}}
      </label>
    </div>
    <select multiple class="form-control">
      <option
        :value="item"
        v-for="(item, index) in itemsLocal"
        @dblclick="Adicionar(index)"
        :key="index"
      >{{item}}</option>
    </select>
  </div>
</template>
<script>
import { mapMutations } from 'vuex';
export default {
  props: {
    listaItens: {
      type: Array,
      required: false,
      default: () => []
    },
    itemsLocal: {
      type: Array,
      required: false,
      default: () => []
    }
  },
  data() {
    return {
      listaSelect: []
    };
  },
  methods: {
    ...mapMutations(["clear_Perfil", "remove_Update_Perfil", "adicionar_update_Perfil"]),
    Adicionar(valor) {
      var AddItem = this.itemsLocal[valor];
      this.adicionar_update_Perfil(AddItem);
      this.removerListaPai(valor)
    },
    removerListaPai(valor) {
      this.$emit("remove-item", valor);
    },
    Remover(valor) {
      var item = this.listaItens[valor];
      this.remove_Update_Perfil(item)
      this.itemsLocal.push(item);
    }, 
  },
  beforeDestroy() {
    this.clear_Perfil()
  },
};
</script>
<style lang="scss" scoped>
.wrapper {
  position: relative;
}

input {
  padding-left: 48px;
}
label {
  font-size: 100%;
}
.wrapper span {
  text-align: center;
  position: absolute;
  left: 10px;
  top: 8px;
  background-color: black;
  border-radius: 10px;
  color: white;
  padding: 0 10px 0px 10px;
  cursor: pointer;
}
</style>