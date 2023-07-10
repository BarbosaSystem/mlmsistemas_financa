import http from './config'

export default {
    async listarMenu (){
        return await http.get('/menu')
    },
    async listarMenuAtivo(formulario){
        return await  http.post('/menu/GetMenuByRole', formulario)
    },
    async PostMenu(formulario){
        return await http.post('/menu', formulario)
    },
    async GetMenuById(codigo){
        return await http.get('/menu/GetMenuById/'+codigo)
    },

    async UpdateMenu(formulario){
        return await http.post('/menu/UpdateMenu', formulario)
    },

    async CadastrarMenu(formulario){
        return await http.post('/menu/SetMenu', formulario)
    }
}