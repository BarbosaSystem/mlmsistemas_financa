import Service from "../../Service/categoria";
const categoria_vuex = ({
    state: {
        listaCategoria: [],
        categoria: {
            Id: 0,
            Nome: ''
        },
        CategoriaPorPeriodo : null
    },
    mutations: {
        SET_ADICIONAR_CATEGORIA(state, payload) {
            state.listaCategoria = payload
        },
        SET_CATEGORIA(state, payload){
            state.categoria.Id = payload.id
            state.categoria.Nome = payload.nome
        },
        SET_CATEGORIA_NOME(state, payload){
            state.categoria.Nome = payload
        },
        Set_CategoriaPorPeriodo(state, payload){
            state.CategoriaPorPeriodo = payload
        }
    },
    actions: {
        async Action_Get_CategoriaPorPeriodo({commit}){
            await Service.CategoriaPorPeriodo().then((response) => {
                commit('Set_CategoriaPorPeriodo', response.data)
            })
        },
        async Action_Get_CategoriaPorPeriodo({commit}, payload){
            await Service.CategoriaPorPeriodoByCatId(payload).then((response) => {
                commit('Set_CategoriaPorPeriodo', response.data)
            })
        },
        Action_Categoria({commit}, payload){
            commit("SET_CATEGORIA", payload)
        },
        async CarregarCategorias({commit}){
            await Service.listarCategorias().then((response) => {
                commit("SET_ADICIONAR_CATEGORIA", response.data)
            })
        },
        AdicionarOuAtualizarCategoria({dispatch, getters}){
            var payload = {
                id: getters.Get_Categoria.Id,
                nome : getters.Get_Categoria.Nome
            }
            Service.AddOrUpdateCategoria(payload).then((response) => {
                alert(response.data)
                dispatch("CarregarCategorias")
            });
        }
    },
    getters: {
        Get_Categoria(state){
            return state.categoria
        },
        GetListaCategoria (state){
            return state.listaCategoria
        },
        Get_CategoriaPorPeriodo (state){
            return state.CategoriaPorPeriodo
        }
    },
    modules: {}
})
export default categoria_vuex