<template>
  <div>
    <div class="card">
      <div class="card-header bg-white text-center">
        <img
          style="height: 128px; width: 128px; object-fit: cover; cursor: pointer"
          :src="GetUser.photoString"
          class="img-logo"
          v-if="GetUser.photoString != null"
          @click="ZoomChange"
        />
        <h2 class>{{GetUser.fullName}}</h2>
        <file-upload :guid="GetUser.id" />
      </div>
      <div class="card-body">
        <ul class="list-group">
          <li class="list-group-item">
            <i class="fas fa-user"></i>
            <strong>Codigo:</strong>
            {{GetUser.id}}
          </li>
          <li class="list-group-item">
            <i class="fas fa-envelope"></i>
            <strong> E-mail:</strong>
            {{GetUser.email}}
          </li>
          <li class="list-group-item">
            <i class="fas fa-phone"></i>
            <strong> Telefone:</strong>
            {{GetUser.phoneNumber}}
          </li>
          <li class="list-group-item">
            <i class="fas fa-user-shield"></i>
             Perfil de Acesso: {{GetUser.role}}
          </li>
          <li class="list-group-item">
            <button class="btn btn-success" @click="Set_Modal_Change">
              <i class="fas fa-key"></i> Alterar Senha
            </button>
          </li>
        </ul>
      </div>
    </div>
    <modal-change :Id="GetUser.id"></modal-change>
    <zoom-image @FecharZoom="ZoomChange" v-if="Zoom" :image="GetUser.photoString"></zoom-image>
  </div>
</template>

<script>
import Service from "../../Service/account";
import FileUpload from "../../components/SHARED/LoadFile";
import ModalChange from "../../components/Usuario/ModalChangePassword";
import ZoomImagem from '../../components/ZoomImage'
import { mapGetters, mapMutations } from "vuex";
export default {
  components: {
    FileUpload,
    "modal-change": ModalChange,
    "zoom-image" : ZoomImagem
  },
  data() {
    return {
      ChangePass: false,
      Zoom: false,
    };
  },
  methods: {
    ...mapMutations(["Set_Modal_Change"]),
    ZoomChange() {
      this.Zoom = !this.Zoom;
    },
  },
  computed: {
    ...mapGetters(["GetUser"]),
  },
};
</script>