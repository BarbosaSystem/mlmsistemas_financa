import http from './config'

export default {
    async listarCategorias (){
        return await http.get('/categoria/RelacaoCategorias')
    },
    AddOrUpdateCategoria(categoria){
        return http.post('/categoria', categoria)
    },
    AtualizarCategoria(categoria){
        return http.put('/categoria/', categoria)
    },
    async CategoriaPorPeriodo(){
        return await http.get('/categoria/CategoriaPorPeriodo/')
    },
    async CategoriaPorPeriodoByCatId(codigo){
        return await http.get('/categoria/CategoriaPorPeriodoByCatId/'+codigo)
    }

}