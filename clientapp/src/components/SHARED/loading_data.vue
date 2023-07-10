<template>
    <tbody>
        <tr>
          <td colspan="5" class="text-center">
            <img :src="imagem" alt=""> Carregando...</td>
        </tr>
      </tbody>
</template>
<script>
import image from '../../assets/loading_ball.gif'

export default {
    data() {
        return {
            imagem : image
        }
    },    
}
</script>