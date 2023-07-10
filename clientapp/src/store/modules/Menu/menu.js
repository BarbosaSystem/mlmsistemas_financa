import Service from "../../../Service/index";
const menu_vuex = ({
    state: {
        Menu: [],
        ShowModalMenu: false,
        MenuAtivo:[],
        listaMenuLocal: ['SysAdmin', 'Admin', 'User'],
        MenuViewModel: {
            Titulo: '',
            Rota: '',
            Status: false,
            Icone:'',
            Tags: []
        }
    },
    mutations: {
        setShow_Modal_Menu(state){
            state.ShowModalMenu = !state.ShowModalMenu
        },
        update_Id(state, payload){
            state.MenuViewModel.Id = payload
        },
        update_Titulo(state, payload){
            state.MenuViewModel.Titulo = payload
        },
        update_Rota(state, payload){
            state.MenuViewModel.Rota = payload
        },
        update_Status(state, payload){
            state.MenuViewModel.Status = payload
        },
        update_Icone(state, payload){
            state.MenuViewModel.Icone = payload
        },
        Set_Carregar_Menu(state, payload) {
            state.Menu = payload
        },
        SET_CARREGAR_MENU_ATIVO(state, payload) {
            state.MenuAtivo = payload
        },
        Set_New_Tag(state, payload){
            state.MenuViewModel.Tags = payload
        },
        Set_Tag(state, payload){
            state.MenuViewModel.Tags[payload].status = !state.MenuViewModel.Tags[payload].status
        },
        Set_Load_Data(state, itens){
            state.ShowModalMenu = !state.ShowModalMenu
            state.MenuViewModel.Id = itens.id
            state.MenuViewModel.Titulo = itens.titulo
            state.MenuViewModel.Rota = itens.rota 
            state.MenuViewModel.Status = itens.status
            state.MenuViewModel.Icone = itens.icone
            state.MenuViewModel.Tags = itens.tags
        },
        
    },
    getters: {
        Get_ShowModal(state){
            return state.ShowModalMenu
        },
        Get_Menu(state) {
            return (state.Menu)
        },
        GET_Menu_Ativo(state) {
            return (state.MenuAtivo)
        },
        GET_MenuView(state){
            return state.MenuViewModel
        },
        Get_Menu_Local(state){
            return state.listaMenuLocal
        }
        
    },
    actions: {
        
        async Action_Carregar_Menu({commit}){
            await Service.Menu.listarMenu().then((response) =>{
                commit("Set_Carregar_Menu", response.data);
            });
        },
        Action_Create_Menu({commit, getters}){
            var Roles = getters.Get_Roles
            var tags = []
            Roles.forEach(element => {
                tags.push({
                    role : element.roleName,
                    roleId : element.id,
                    status : false
                })
            });
            commit("Set_New_Tag", tags)
            commit("update_Id", 0)
            commit("update_Titulo", "")
            commit("update_Rota", "")
            commit("update_Icone", "")
            commit("update_Status", true)
            commit("setShow_Modal_Menu")
        },
        Action_Load_Edit_Menu({commit}, payload){
            commit("Set_Load_Data", payload)
        },
        async Action_Update_Menu({commit, dispatch, getters}){
            var payload = getters.GET_MenuView
            Service.Menu.UpdateMenu(payload).then( (response) => {
                alert(response.data)
                dispatch("Action_Carregar_Menu")
                commit("setShow_Modal_Menu")
            })
        },
        async Action_Adicionar_Menu({commit, dispatch, state}){
            var menu = state.MenuViewModel
            await Service.Menu.CadastrarMenu(menu).then((response) => {
                alert(response.data);
                dispatch("Action_Carregar_Menu")
                commit("setShow_Modal_Menu")
            })
        },
        async Action_GetMenuById({commit}, payload){
            await Service.Menu.GetMenuById(payload).then((response) => {
                commit("Set_Load_Data", response.data)
            })
        },
        async Action_Load_Menu_Ativo({commit}, payload){
            var role = {
                Id : 0,
                roleName: payload
            }
            await Service.Menu.listarMenuAtivo(role).then((response) => {
                if(response.data != null){
                    commit("SET_CARREGAR_MENU_ATIVO", response.data)
                }
                else{
                    alert("Não foi encontrando nenhum item. \n Favor verificar com o administrador as suas permissões de acesso");
                }
            })
        }
    }
})

export default menu_vuex