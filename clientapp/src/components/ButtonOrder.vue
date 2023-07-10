<template>
  <div style="display: inline; float: right;">
    <i class="fas fa-sort" @click="OrdenarMovimentos({Item: Item})"></i>
  </div>
</template>
<script>
import { mapMutations } from 'vuex'
export default {
    props:['Item'],
    methods: {
        ...mapMutations(["OrdenarMovimentos"]),
    },
}
</script>