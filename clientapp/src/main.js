import Vue from 'vue'
import App from './App.vue'
import {router} from './router'
import store from './store'
import SideBar from './Layout/SideBar.vue'
import HeaderBar from './Layout/HeaderBar.vue'
import HeaderPage from './components/SHARED/HeaderPage.vue'
import ModalMenu from './components/Menu/ModalAddOrEditMenu.vue'
import VueTabs from 'vue-nav-tabs/dist/vue-tabs'
import 'vue-nav-tabs/themes/vue-tabs.css'
import 'vue-nav-tabs/dist/vue-tabs'
import LoadingData from './components/SHARED/loading_data.vue'
import Loading from './components/SHARED/Loading.vue'
import LabelData from './components/SHARED/M_Data_Periodo.vue'
import MenuRoles from './components/Roles/MenuRoles.vue'
import ModalUserInfo from './components/Usuario/ModalUserInfo.vue'

Vue.use(VueTabs)
require('./Service/index');
Vue.component('modal-menu', ModalMenu)
Vue.component('side-component', SideBar)
Vue.component('header-component', HeaderBar)
Vue.component('header-page', HeaderPage)
Vue.component('loading-data', LoadingData)
Vue.component('loading', Loading)
Vue.component('menu-roles', MenuRoles)
Vue.component('modal-user-info', ModalUserInfo)
Vue.component('m-data-periodo-principal', LabelData)

Vue.config.productionTip = false
router.beforeEach((to, from, next) => {
  // to and from are both route objects. must call `next`.
  
  store.commit('updateLoading', true)
  document.title = to.meta.title + ' - Projeto Financa'
  next()
})


router.afterEach(() => {
  if(localStorage.getItem("Token")){
    var roleToken = JSON.parse(localStorage.getItem("Token"))
    store.commit("SET_CARREGAR_MENU_ATIVO", roleToken.usuario.listMenu)
  }
  store.commit('updateLoading', false)
})  

const app = new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')


