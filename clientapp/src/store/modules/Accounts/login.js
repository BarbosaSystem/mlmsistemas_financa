import {
    router
} from '../../../router/index';
import Service from "../../../Service/account";
const login_vuex = ({
    state: {
        Login: '',
        Senha: '',
        RecoverPassword: [],
        Email: '',
        Process: false
    },
    mutations: {
        updateProcess(state) {
            state.Process = !state.Process
        },
        updateEmail(state, payload) {
            state.Email = payload
        },
        updateLogin(state, payload) {
            state.Login = payload
        },
        updateSenha(state, payload) {
            state.Senha = payload
        },
        SET_RETURN_MESSAGEM(state, payload) {
            state.RecoverPassword = payload
        }

    },
    getters: {
        GetProcess(state) {
            return state.Process
        },
        GetEmail(state) {
            return state.Email
        },
        GetLogin(state) {
            var formulario = {
                Login: state.Login,
                Senha: state.Senha
            }
            return formulario
        },
    },
    actions: {
        async Action_RecoverPassword({
            commit,
            getters
        }) {
            commit("updateProcess")
            var payload = getters.GetEmail
            await Service.RecoverPassword(payload).then((response) => {
                alert(response.data.mensagem);
            })
            commit("updateProcess")
            commit("updateEmail", "")
        },
        async Action_Logout({
            commit
        }) {
            await Service.Logout().then((response) => {
                if ((response === null) || (response === undefined)) {
                    commit("MUTATION_ADD_USER_TOKEN", null)
                    commit("MUTATION_ADD_USER_PROFILE", null)
                    commit("SET_CARREGAR_MENU_ATIVO", null)
                    localStorage.removeItem('Token')
                    router.replace('/');
                }
            })
        },
        async Action_Logar({
            commit,
            getters,
            dispatch
        }) {
            commit("updateLoading", true)
            try {
                await Service.Logar({
                        UserName: getters.GetLogin.Login,
                        Password: getters.GetLogin.Senha
                    }).then((response) => {
                        if (typeof (response.data) == 'object') {
                            var JwToken = response.data

                            
                            JwToken.token.expiration *= 1000
                            localStorage.setItem('Token', JSON.stringify(JwToken))
                            var roleToken = JSON.parse(localStorage.getItem("Token"))
                            commit("SetToken", JwToken)
                            commit("MUTATION_ADD_USER_PROFILE", roleToken.usuario)
                            commit("SET_CARREGAR_MENU_ATIVO", response.data.usuario.listMenu)
                            dispatch("CarregarCategorias")
                            router.replace('/principal')
                        }
                    })
                    .catch((error) => {
                        alert(error.response.data.message)
                        commit("MUTATION_ERROR", error.response.data.message)
                    })

            } catch (error) {
                commit("MUTATION_ERROR", "Serviço off-line")
            } finally {
                commit("updateLogin", "")
                commit("updateSenha", "")
                commit("updateLoading", false)
            }
        },
    }
})

export default login_vuex
