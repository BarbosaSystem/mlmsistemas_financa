const pagination_vuex = ({
    state: {
        pagination: {
            totalPages: 0,
            currentPage: 0,
            itensPage: 20
        },
        Loading: false
    },
    actions: {

    },
    getters: {
        GetPagination(state) {
            return state.pagination
        },
    }
})
export default pagination_vuex