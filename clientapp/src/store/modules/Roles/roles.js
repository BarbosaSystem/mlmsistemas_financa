import Service from "../../../Service"

const roles_vuex = ({
    state: {
        listaRoles: [],
        ModalRole: false,
        Role: {
            Id: '',
            RoleName: ''
        },
    },
    mutations: {
        cleanRoles(state){
            state.listaRoles = []
        },
        updateRoleNome(state, payload){
            state.Role.RoleName = payload
        },
        updateIdRole(state, payload){
            state.Role.Id = payload
        },
        Set_Lista_Roles(state, payload){
            state.listaRoles = payload
        },
        Set_Modal_Role(state){
            state.ModalRole = !state.ModalRole
            if(!state.ModalRole){
                state.Role.Id = ''
                state.Role.RoleName = ''
            }
        }
    },
    actions: {
        ActionGetRole({commit}, payload){
            commit("updateRoleNome", payload.roleName)
            commit("updateIdRole", payload.id)
            commit("Set_Modal_Role")
        },
        async Action_GetRoles({commit}){
            await Service.Role.listaRoles().then((response) => {
                if(response.status == 200){
                    commit("cleanRoles")
                    commit("Set_Lista_Roles", response.data)
                }
            })
        },
        /* async Action_GetRoles({commit}){
            await Service.Role.listaRoles().then( (response) => {
                var items = response.data
                if((items != null) || (items != undefined)){
                    commit("cleanRoles")
                    items.forEach(element => {
                        var item = {
                            Id : element.id,
                            RoleName: element.roleName
                        }
                        commit("Set_Lista_Roles", item)
                    });
                }
            })
        }, */
        async Action_Update_Roles({dispatch, commit}, payload){
            await Service.Role.PutRole({Id: payload.Id, RoleName: payload.RoleName}).then((response) => {
                alert(response.data)
                dispatch("Action_GetRoles")
            });
        },
        async Action_Create_Roles({dispatch, commit}, payload){
            await Service.Role.PostRole({Id: payload.Id, RoleName: payload.RoleName}).then((response) => {
                alert(response.data)
                dispatch("Action_GetRoles")
                
            });
        },
        async Action_SetRole({dispatch, commit, getters}){
            var role = {
                Id : getters.Get_Role.Id,
                RoleName : getters.Get_Role.RoleName
            }
            if(role.Id != ""){
                await dispatch("Action_Update_Roles", role)
                commit("Set_Modal_Role")
            }else {
                await dispatch("Action_Create_Roles", role)
                commit("Set_Modal_Role")
            }
        }        
    },
    getters: {
        Get_Roles(state){
            return state.listaRoles
        },
        Get_Modal_Role(state){
            return state.ModalRole
        },
        Get_Role(state){
            return state.Role
        }
    }
})

export default roles_vuex