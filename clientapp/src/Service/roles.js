import http from './config'
export default {
    async listaRoles (){
        return await http.get(
            '/role').catch((response) => {
            })
    },
    async PostRole(Role){
        return await http.post('/role', {
            Id : "",
            RoleName : Role.RoleName})
    },
    async PutRole(Role){
        return await http.put('/role', {
            Id : Role.Id,
            RoleName : Role.RoleName})
    }
}