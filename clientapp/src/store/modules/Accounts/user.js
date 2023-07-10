import Service from "../../../Service/account"

const user_vuex = ({
    state: {
        listUser : [],
        ModalChangePassword: false,
        ChangePassword: {
            OldPassword: '',
            NewPassword: '',
            ConfirmPassword: ''
        },
        UserInfo: {
            photoString : "https://i.pinimg.com/originals/45/12/4d/45124d126d0f0b6d8f5c4d635d466246.gif"
        }
    },
    mutations: {
        Set_Modal_Change(state){
            state.ModalChangePassword = !state.ModalChangePassword
        },
        updateLoadingImage(state){
            state.UserInfo.photoString = "https://i.pinimg.com/originals/45/12/4d/45124d126d0f0b6d8f5c4d635d466246.gif"
        },
        Set_List_Users(state, payload){
            state.listUser = payload
        },
        updateOldPassword(state, payload){
            state.ChangePassword.OldPassword = payload
        },
        updateNewPassword(state, payload){
            state.ChangePassword.NewPassword = payload
        },
        updateConfirmPassword(state, payload){
            state.ChangePassword.ConfirmPassword = payload
        },
        Set_User_Info(state, payload){
            state.UserInfo = payload
        },
        updateUserInfoEmail(state, payload){
            state.UserInfo.Email = payload
        },
        updateUserInfoRole(state, payload){
            state.UserInfo.Rolename = payload
        }
    },
    actions: {
        Action_Load_Users({commit}){
            Service.GetUsersList().then((response) => {
                commit("Set_List_Users", response.data);
            })
        },
        async Action_Load_User({commit}, payload){
            await Service.GetUserInfo(payload).then((response) => {
                commit("Set_User_Info", response.data)
            })
        },
        Action_Change_Password({getters, dispatch}, payload){
            var formulario = {
                Id : payload,
                NewPassword : getters.Get_NewPassword,
                OldPassword: getters.Get_OldPassword,
                ConfirmPassword : getters.Get_ConfirmPassword
            }
            Service.ChangePassword(formulario).then((response) => {
                if(response.data == "Senha alterada com sucesso"){
                    alert(response.data +" \nPara completar a ação, o acesso ao sistema precisará ser reiniciado.")
                    dispatch("Action_Logout")
                }
                else{
                    alert(response.data)
                }
            })
        },
        async Action_Update_Email_And_Role({getters}){
            var payload = {
                Id: getters.Get_User_Info.id,
                Email: getters.Get_User_Info.Email,
                RoleName: getters.Get_User_Info.Rolename
            }
            await Service.ChangeEmailAndRole(payload).then((response) => {
                alert(response.data)
            })
        },
    },
    getters: {
        Get_User_Info(state){
            return state.UserInfo
        },
        Get_OldPassword(state){
            return state.ChangePassword.OldPassword
        },
        Get_NewPassword(state){
            return state.ChangePassword.NewPassword
        },
        Get_ConfirmPassword(state){
            return state.ChangePassword.ConfirmPassword
        },
        Get_List_Users(state){
            return state.listUser
        },
        Get_Modal_Change(state){
            return state.ModalChangePassword
        },
        Get_Correct_Password(state){
            return !(
                (state.ChangePassword.OldPassword.length >=5) && 
                (state.ChangePassword.NewPassword == state.ChangePassword.ConfirmPassword) && 
                (state.ChangePassword.NewPassword.length >= 5))
        }
    }
})

export default user_vuex