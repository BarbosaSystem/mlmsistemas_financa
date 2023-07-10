import Service from "../../Service/account";
import Login from './Accounts/login.js'
import Register from './Accounts/register';

const account_vuex = ({
    state: {
        user: null,
        token: null,
        error: null,
    },
    mutations: {
        MUTATION_ADD_USER_TOKEN(state, payload) {
            state.token = payload
        },
        MUTATION_ADD_USER_PROFILE(state, payload) {
            state.user = payload
        },
        MUTATION_ERROR(state, payload) {
            state.error = payload
        },
        SetPhotoString(state, payload){
            state.user.photoString = payload
        }
    },
    actions: {
        
        //CARREGAR Informações de Tokens e Perfil de Usuário
        async CarregarInfo({
            commit
        }, payload) {
            commit("MUTATION_ADD_USER_TOKEN", payload.Token)
            commit("MUTATION_ADD_USER_PROFILE", payload.userProfile)
        },
        
        Action_CleanError({
            commit
        }) {
            commit("MUTATION_ERROR", null)
        },
        async Action_Profile({
            commit,
            dispatch
        }, payload) {
            await Service.UserProfile(payload).then((response) => {
                if (response == undefined) {
                    dispatch('Action_Logout')
                }
                commit("MUTATION_ADD_USER_PROFILE", response.data)
                
                dispatch("Action_Carregar_Menu_Ativo", response.data.role[0])
            })
        },
        async Action_VerifyToken({
            dispatch
        }) {
            await Service.VerifyToken().then((response) => {
                if (response.status == 401) {
                    dispatch('Action_Logout')
                } else {
                    return;
                }
            });
        },
        async parseJwt({
            dispatch
        }, payload) {
            var token = payload
            if (token != null) {
                var base64Url = token.split('.')[1];
                var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
                    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                }).join(''));

                var info = {
                    Token: payload,
                    userProfile: JSON.parse(jsonPayload)
                }
                localStorage.setItem('Token', JSON.stringify(info))
                dispatch("Action_Profile", info.userProfile.UserID)
                dispatch("Action_Carregar_Menu_Ativo", info.userProfile.role)
            }
        },
    },
    getters: {
        GetUser(state) {
            return state.user
        },
        GetToken(state) {
            return state.token
        },
        GetError(state) {
            return state.error
        },
    },
    modules: {
        Login, Register
    }
})
export default account_vuex