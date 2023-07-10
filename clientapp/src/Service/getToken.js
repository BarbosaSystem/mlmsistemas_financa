import http from "./config";

export default {
    isToken(){
        var token = localStorage.getItem('Token')
        var des = JSON.parse(token)
        return "Bearer " +  des.token.token;
    },
    async ChekTokenValid(){
        var token = localStorage.getItem('Token')
        return await http.post('/token/VerifyToken', token.token)
    }
}