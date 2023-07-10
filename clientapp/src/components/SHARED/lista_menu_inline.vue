<template>
  <div>
    <ul class="lista-inline">
      <router-link class="btn btn-primary" tag="li" to="/logout">
        <i class="fas fa-lock"></i>
        <template v-if="!toggle"> Bloquear Sessão</template>
      </router-link>
    </ul>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  props: ["toggle"],
  computed: {
    ...mapGetters(["GetUser"])
  }
};
</script>
<style lang="scss">
.lista-inline {
  padding: 0px;
  margin-top: 15px;
  list-style: none;
  &-item {
    display: inline-block;
    cursor: pointer;
  }
}
</style>