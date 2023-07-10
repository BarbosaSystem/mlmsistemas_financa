import http from './config'

export default {
    listaPeriodo (){
        return http.get('/periodo')
    },
    postPeriodo(periodo){
        return http.post('/periodo/', periodo)
    },
    async buscarPeriodo(codigo){
        return http.get('/periodo/BuscarPeriodo/'+codigo)
    },
    EncerrarPeriodo(codigo){
        return http.get('/periodo/EncerrarPeriodo/'+ codigo)
    },
    async UltimoPeriodo(){
        return http.get('periodo/UltimoPeriodo')
    }
}