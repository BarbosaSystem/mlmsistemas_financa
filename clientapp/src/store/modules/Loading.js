const loading_vuex = ({
    state:{
        Loading: null,
        LoadingData: false
    },
    mutations: {
        updateLoading(state, payload){
            state.Loading = payload
        },
        updateLoadingData(state, payload){
            state.LoadingData = payload
        }
    },
    actions: {
        
    },
    getters: {
        GetLoading(state){
            return state.Loading
        },
        GetLoadingData(state){
            return state.LoadingData
        }
    }

})
export default loading_vuex