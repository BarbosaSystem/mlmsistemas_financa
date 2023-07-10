<template >
  <div v-if="Get_User_Info"
    class="modal fade"
    id="staticBackdrop"
    data-backdrop="static"
    data-keyboard="false"
    tabindex="-1"
    aria-labelledby="staticBackdropLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="staticBackdropLabel">{{Get_User_Info.fullName}}</h5>
          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="bd-example" >

              <div class="form-group text-center">
                  <img class="img-fluid" style="max-width: 50% !important; border-radius: 5px" :src="Get_User_Info.photoString" alt="">
              </div>

            <address>
              <strong>Nome de usuário:</strong> {{Get_User_Info.usuario}}<br />
              <!-- 1355 Market St, Suite 900<br />
              San Francisco, CA 94103<br /> -->
              <!-- <abbr title="Phone">P:</abbr> (123) 456-7890 -->
            </address>

            <address>
              <!-- <strong>Full Name</strong><br /> -->
              <strong>E-mail: </strong><a href="mailto:">{{Get_User_Info.email}}</a>
            </address>
            <address>
                <strong>Nível de Acesso:</strong> {{Get_User_Info.role}} <br/>

                
            </address>
            <div class="form-group">
                 <button class="btn btn-success" @click="ChangeEdit()">Alterar Dados:</button>
            </div>
            <div class="card p-3 m-0" v-if="Edit"> 
                <form @submit.prevent="Action_Update_Email_And_Role()"> 
                <div class="form-group">
                    <label for="exampleInputEmail1">Email address</label>
                    <input type="email" v-model="Email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">
                    <!-- <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small> -->
                </div>
                <!-- <div class="form-group form-check">
                    <input type="checkbox" class="form-check-input" id="exampleCheck1">
                    <label class="form-check-label" for="exampleCheck1">Check me out</label>
                </div> -->
                <div class="form-group">
                    <label for="">Nível de acesso</label>
                    <select class="custom-select" id="inputGroupSelect02" v-model="Rolename" >
                        <option v-for="(item, index) in Get_Roles" :key="index">{{item.roleName}}</option>
                    </select>
                </div>
                <div class="form-group">
                    <button type="submit" class="btn btn-primary"><i class="fas fa-sync-alt"></i> Atualiar</button>
                </div>
                </form>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" @click="FecharModal">
            Fechar
          </button>
          <!-- <button type="button" class="btn btn-primary">Understood</button> -->
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapGetters } from 'vuex';
export default {
    computed: {
        ...mapGetters(["Get_User_Info", "Get_Roles"]),
        Email: {
            get() {
                return this.Get_User_Info.Email;
            },
            set(value) {
                this.$store.commit("updateUserInfoEmail", value);
            }
        },
        Rolename: {
            get() {
                return this.Get_User_Info.role;
            },
            set(value) {
                this.$store.commit("updateUserInfoRole", value)
            }
        }
    },
    data() {
        return {
            Edit: false
        }
    },
    methods: {
        ...mapActions(["Action_Update_Email_And_Role"]),
        ChangeEdit(){
            this.Edit = !this.Edit
        },
        FecharModal(){
            this.Edit = false
            $("#staticBackdrop").modal('toggle')
        }
    }
};
</script>