<template>
  <div class="linha-periodo-item" >
    <div class="">
      <i :class="`fas ${icon}`"></i><strong class="ml-2 d-none d-sm-block">{{label}}</strong>
      <label for="" class="skeleton skeleton-text" style="font-size: 0.9rem">{{dia}}</label>
    </div>
  </div>
</template>
<script>
export default {
  props: ['label', 'dia', 'position', 'icon']
};
</script>