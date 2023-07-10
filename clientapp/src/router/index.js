import Vue from 'vue'
import VueRouter from 'vue-router'
import { Auth, isSignedIn } from '../js/Auth'
import store from "../store/index"
Vue.use(VueRouter)

const routes = [
  {
    path: '*',
    name: '404',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Home/HomeTest.vue')
  },
  {
    
    path: '/',
    name: 'home',
    meta: {
      title: 'MLM SISTEMAS',
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Home/HomeTest.vue')
  },
  {
    path: '/movimentos',
    name: 'movimentos',
    /* beforeEnter: authGuard, */
    meta: {
      title: 'Movimentações',
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Movimentos/RelacaoMovimentacoes.vue')
  },
  {
    path: '/categoria',
    name: 'categoria',
    /* beforeEnter: authGuard, */
    meta: {
      title: 'Categoria',
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Categoria/RelacaoCategoria.vue')
  },
  {
    path: '/periodo',
    name: 'periodo',
    /* beforeEnter: authGuard, */
    meta: {
      title: 'Período',
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Periodo/RelacaoPeriodo.vue')
  },
  {
    path: '/principal',
    name: 'principal',
    /* beforeEnter: authGuard, */
    beforeMount() {
      
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    meta: {
      title: 'Página Principal',
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Home/Home.vue')
  },
  {
    path: '/logout',
    name: 'logout',
    meta: {
      title: 'Sessão Bloqueada',
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Login/Logout.vue')
  },
  {
    path: '/settings',
    name: 'settings',
    /* beforeEnter: authGuard, */
    meta: {
      title: 'Configurações',
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Configurações/configuration.vue')
  },
  {
    path: '/userprofile',
    name: 'profile',
    /* beforeEnter: authGuard, */
    meta: {
      title: 'Perfil de Usuário',
    },
    beforeEnter: (to, from, next) => {
      // ...
      newFunction(next, to)
      
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import( /* webpackChunkName: "about" */ '../views/Perfil/UserProfile.vue')
  }
]

export const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

function newFunction(next, to) {
  if (localStorage.getItem('Token')) {
    let token = JSON.parse(localStorage.getItem("Token"))
    if (Date.now() < token.token.expiration) {
      store.commit("MUTATION_ADD_USER_PROFILE", token.usuario)
      next()
    } else {
      next('/')
      store.commit("MUTATION_ADD_USER_PROFILE", null)
      localStorage.removeItem("Token")
      alert('Sessão encerrada')
      store.commit('updateLoading', false)
      /* alert("Sessão expirada... Você será redirecionado para a página de login")
      localStorage.removeItem('Token')
      store.commit("MUTATION_ADD_USER_PROFILE", null)
      document.title = to.meta.title + ' - Projeto Financa'
      next('/') */
    }
  }
  else {
    document.title = to.meta.title + ' - Projeto Financa'
    next('/')
  }
}

