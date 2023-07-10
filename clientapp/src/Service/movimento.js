import http from './config'

export default {
    listarMovimento (){
        return http.get('/movimentacao')
    },
    postMovimento(movimento){
        return http.post('/movimentacao/', movimento)
    },
    listaPendentes(){
        return http.get('/movimentacao/PesquisaPendentes')
    },
    AtualizarMovimento(movimento){
        return http.put('/movimentacao/', movimento)
    },
    DeletarMovimento(codigo){
        return http.delete('/movimentacao/DeleteMovimento/' + codigo)
    },
    BuscarMovimento(codigo){
        return http.get('/movimentacao/BuscaMovimento/'+codigo)
    },
    UltimosLancamentos(){
        return http.get('/movimentacao/UltimosLancamentos')
    },
    postListMovimentos(movimentos){
        return http.post('/movimentacao/CarregarListaMovimentos', movimentos);
    }

}