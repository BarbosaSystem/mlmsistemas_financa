import Vue from 'vue'
import Vuex from 'vuex'
import movimento from './modules/movimento.js'
import categoria from './modules/categoria.js'
import account from './modules/account.js'
import periodo from './modules/Periodo/index';
import pagination from './modules/Shared/pagination'
import menu from './modules/Menu/menu'
import roles from './modules/Roles/roles'
import Token from './modules/Token/token'
import Users from './modules/Accounts/user'
import Loading from './modules/Loading'

Vue.use(Vuex)

export default new Vuex.Store({
  strict: true,
  state: {
    Toogle: false
  },
  mutations: {
    setLoading(state, payload) {
      state.Loading = payload
    },
    setToggle(state){
      state.Toogle = !state.Toogle
    }
  },
  actions: {
  },
  getters: {
    GetToogle(state){
      return state.Toogle
    }
  },
  modules: {
    movimento,
    categoria,
    account,
    periodo,
    pagination,
    menu,
    roles,
    Token,
    Users,
    Loading
  }
})