import Service from "../../../Service/periodo";
const periodo_vuex = ({
    state: {
        listaPeriodo: [],
        periodo: {
            referencia: '',
            DataInicial: '',
            DataFinal: ''
        },
        MovimentoPeriodo: [],
        ModalPeriodo: false,
        PeriodoEscolhido: null,
        UltimoPeriodo: false
    },
    mutations: {
        SetPeriodoEscolhido(state, payload){
            state.PeriodoEscolhido = payload
        },
        SetMovimentacaoPeriodo(state, payload){
            state.MovimentoPeriodo = payload
        },
        SET_Modal_Periodo(state){
            state.ModalPeriodo = !state.ModalPeriodo
        },
        SET_Carregar_Lista(state, payload){
            state.listaPeriodo = payload.sort((a, b) =>{
                if(a.id > b.id){
                    return -1;
                }
                if(a.id < b.id){
                    return 1;
                }
                return 0;
            });
        },
        SET_Referencia(state, payload) {
            state.periodo.referencia = payload
        },
        SET_Data_Inicial(state, payload) {
            state.periodo.DataInicial = payload
        },
        SET_Data_Final(state, payload) {
            state.periodo.DataFinal = payload
        },
        SET_Ultimo_Periodo(state, payload) {
            state.UltimoPeriodo = payload
        },
    },
    getters: {
        GET_Modal_Periodo (state){
            return state.ModalPeriodo
        },
        GET_Lista_Periodo(state){
            return state.listaPeriodo;
        },
        GET_Periodo(state){
            return state.periodo;
        },
        Get_ItensMovimentacao(state){
            return state.MovimentoPeriodo
        },
        Get_PeriodoEscolhido(state){
            return state.PeriodoEscolhido
        },
        Get_MovEscolhido(state){
            return state.MovimentoPeriodo.movimentacao
        },
        Get_UltimoPeriodo(state){
            return state.UltimoPeriodo
        }
    },
    actions: {
        Action_Set_Modal_Periodo({commit}, payload){
            commit("SET_Modal_Periodo", payload)
        },
        Action_Periodo_Adicionar({dispatch, getters}){
            var periodo = {
                referencia : getters.GET_Periodo.referencia,
                DataInicial : getters.GET_Periodo.DataInicial,
                DataFinal : getters.GET_Periodo.DataFinal
            } 
            Service.postPeriodo(periodo).then((response) => {
                alert(response.data)
                dispatch("Action_Periodo_Carregar")
                dispatch("Action_Set_Modal_Periodo", false)
            }).catch(() => {
                alert('Erro ao processar a sua solicitação')
                dispatch("Action_Set_Modal_Periodo", false)
            })
            
        },
        async Action_BuscarPeriodoById({commit}, payload){
            var codigo = parseInt(payload)
            await Service.buscarPeriodo(codigo).then((response) => {
                commit("SetMovimentacaoPeriodo", response.data)
            })
        },
        Action_Periodo_Carregar({commit}){
            Service.listaPeriodo().then((response) => {
                commit("SET_Carregar_Lista", response.data)
            })
        },
        async Action_Periodo_Ultimo({commit}){
            await Service.UltimoPeriodo().then((response)=> {
                commit("SET_Ultimo_Periodo", response.data)
            })
            
        }
    }
})

export default periodo_vuex