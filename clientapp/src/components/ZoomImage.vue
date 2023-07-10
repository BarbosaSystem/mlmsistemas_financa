<template>
  <transition name="bounce">
    <template>
      <div>
        <div class="modal-photo">
          <div class="modal-photo-area">
            <div class="botao" style="align-self: flex-end; margin-top: 20px;">
              <button
                class="btn btn-default"
                style="color: white; font-size: 1.5rem;"
                @click="Fechar"
              >
                <i class="fas fa-times"></i>
              </button>
            </div>
            <img
              style="height: 512px; width: 512px; object-fit: cover;"
              :src="image"
              class="img-thumbnail"
              v-if="image != null"
            />
          </div>
        </div>
        <div class="modal-transparencia"></div>
      </div>
    </template>
  </transition>
</template>
<script>
export default {
    props: ['image'],
    methods: {
        Fechar(){
            this.$emit('FecharZoom')
        }
    },
}
</script>
<style lang="css">
.modal-photo {
  position: fixed;
  top: 0;
  bottom: 0;
  z-index: 1111;
  right: 0;
  left: 0;
}
.modal-photo-area {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-content: center;
  align-items: center;
  padding: 15px;
}
.modal-transparencia {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1040;
  background-color: #000;
  opacity: 0.65;
  transition: 0.5s linear;
}
.bounce-enter-active {
  animation: bounce-in .5s;
}
.bounce-leave-active {
  animation: bounce-in .3s reverse;
}
@keyframes bounce-in {
  0% {
    opacity: 0;
  }
  
  100% {
    opacity: 1;
  }
}
</style>