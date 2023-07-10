import axios from 'axios'
import {router} from '../router/index'

const http = axios.create({
    baseURL: 'https://mlmsistemas.somee.com/api',
    headers: {
        'Access-Control-Allow-Origin': '*',
    },
    /* baseURL: '/api/' */
});

http.interceptors.request.use(config => {
    if (!config.url.endsWith("/ApplicationUser/Login")) {
        const token = JSON.parse(localStorage.getItem("Token"))
        if(Date.now() < token.token.expiration){
            config.headers.Authorization = `Bearer ${token.token.token}`
        }
    }

    return config;
})

export default http