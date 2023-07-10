import Service from "../../../Service/account";
const register_vuex = ({
    state: {
        Form_Registro: {
            Nome: "",
            Username: "",
            Email: "",
            Senha: "",
            ConfirmSenha: ""
        }
    },
    mutations: {
        clearFormRegistro(state){
            state.Form_Registro= {
                Nome: "",
                Username: "",
                Email: "",
                Senha: "",
                ConfirmSenha: ""            }
        },
        updateNome(state, payload) {
            state.Form_Registro.Nome = payload
        },
        updateUsername(state, payload) {
            state.Form_Registro.Username = payload
        },
        updateEmail(state, payload) {
            state.Form_Registro.Email = payload
        },
        updateSenha(state, payload) {
            state.Form_Registro.Senha = payload
        },
        updateConfirmSenha(state, payload) {
            state.Form_Registro.ConfirmSenha = payload
        },
    },
    getters: {
        GetIsValid(state) {
            return (state.Form_Registro.Senha == state.Form_Registro.ConfirmSenha) && (state.Form_Registro.Senha.length > 4)
        },
        GetFormulario(state) {
            return state.Form_Registro
        }
    },
    actions: {
        Action_LimparForm({commit}){
            commit("clearFormRegistro")
        },
        async Action_Registrar({ commit,
            getters,
            dispatch
        }) {
            var formulario = {
                UserName: getters.GetFormulario.Username,
                Email: getters.GetFormulario.Email,
                Password: getters.GetFormulario.Senha,
                FullName: getters.GetFormulario.Nome,
                Role: ""
            }
            var myJson = JSON.stringify(formulario)
            Service.Registrar(myJson)
                .then((response) => {
                    if (response.data.message) {
                        if (confirm("Usuário criado com sucesso! Deseja acessar o sistema agora?")) {
                            commit("updateLogin", formulario.UserName)
                            commit("updateSenha", formulario.Password)
                            dispatch("Action_Logar");
                        }
                    }
                }).catch((error) => {
                    alert("Não foi possível executar a sua solicitação");
                });
        }
    }
})

export default register_vuex