<template>
    <div class="text-center" :class="{'mt-30' : window.width <= 767, 'mt-5' : window.width > 767}">
        <h2 class="page-header">{{texto}}</h2>
    </div>
</template>
<style lang="css">
    .mt-30{
        margin-top: 90px;
    }
</style>
<script>
import mix_rezise from "../../js/mix_rezise"
export default {
    props:['texto'],
    mixins:[mix_rezise]
}
</script>