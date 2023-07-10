import Service from "../../../Service"

const token_vuex = ({
    state:{
        Token: null
    },
    mutations: {
        SetToken(state, payload){
            state.Token = payload
        }
    },
    actions: {
        Action_Check_Token({commit}){
            Service.Token.ChekTokenValid().then((response) => {
                commit("SetToken", response.data)
            })
        }
    },
    getters: {
        Get_Check_Token(state){
            return state.Token
        }
    }

})
export default token_vuex