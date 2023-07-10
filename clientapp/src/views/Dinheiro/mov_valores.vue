<template>
  <div>
    <header-page texto="Página Inicial"></header-page>
    <div class="card-group">
      <div class="card p-5">
        <ul class="list-group">
          <li class="list-group-item active text-center">Entrada</li>
          <li class="list-group-item text-center">
              <button class="btn btn-primary" @click="AdicionarMovimento('Entrada')">Entradas de valores Fixa</button>
          </li>
        </ul>
      </div>
      <div class="card p-5">
        <ul class="list-group">
          <li class="list-group-item active text-center">Saída</li>
          <li class="list-group-item text-center">
              <button class="btn btn-primary" @click="AdicionarMovimento('Entrada')">Adicionar Item</button>
          </li>
        </ul>
      </div>
      <div class="card p-5">
        <ul class="list-group">
          <li class="list-group-item active text-center">Itens Fixos</li>
          <li class="list-group-item text-center">
              <button class="btn btn-primary" @click="AdicionarMovimento('Entrada')">Cadastrar Item</button>
          </li>
        </ul>
      </div>
    </div>
    <modal-valor :titulo="titulo" />
  </div>
</template>
<script>
import ModalValor from '../../components/Movimento/MovimentoValor';
export default {
    components: {
        ModalValor
    },
    data(){
        return {
            titulo: ''
        }
    },
    methods: {
        AdicionarMovimento(item) {
            this.titulo = item
            this.$root.$emit("ModalMovimentoValor::show");
        }
    }
}
</script>