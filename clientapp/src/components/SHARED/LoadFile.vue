<template>
  <div class="form-group">
    <button class="btn btn-warning" @click="BuscarFoto">Alterar Foto</button>
    <input type="file" style="display: none" ref="btn_alterar" @change="btn_alterar_change" />
  </div>
</template>
<script>
import Service from "../../Service/account";
export default {
  name: "FileUpload",
  data(){
      return {
        imageUrl: "",
        image: null
      }
  },
  props: ['guid'],
  methods: {
    BuscarFoto() {
      this.$refs.btn_alterar.click();
    },
    emitClick(index){

    },
    async CarregarFoto() {
      var dados = {
        UserId: this.guid,
        files: this.image
      };
      await Service.LoadImageProfile(dados)
        .then(response => {
          if (response.data) {
            if(response.data.messagem){
              this.$store.commit("SetPhotoString", response.data.photo)
              alert("Imagem Carregada com sucesso");
            }else{
              alert(response.data.messagem)
            }
          }
        })
        .catch(error => {
          alert(error);
        });
    },
    async btn_alterar_change(event) {
      const files = event.target.files;
      let filename = files[0].name;
      if (filename.lastIndexOf(".") <= 0) {
        return alert("Por favor adicionar um arquivo válido");
      }
      const fileReader = new FileReader();
      fileReader.addEventListener("load", () => {
        this.imageUrl = fileReader.result;
      });
      fileReader.readAsDataURL(files[0]);
      this.image = files[0];
      if (confirm("Deseja salvar esta foto?")) {
        await this.CarregarFoto();
      }
    }
  }
};
</script>