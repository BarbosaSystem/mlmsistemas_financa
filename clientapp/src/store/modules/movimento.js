import Service from "../../Service/index";
const movimento_vuex = ({
    state: {
        listaUltimosMovimentos: [],
        listaMovPendentes: [],
        MovimentoItem: {
            id: '',
            descricao: "",
            valor: "",
            data: "",
            categoria: "",
            status: ""
        },
        search: "",
        listaMovimentos: [],
        ValorOrder: false,
        ModalEditMovimentacao: false
    },
    mutations: {
        updateSearch(state, payload) {
            state.search = payload
        },
        updateModalEditMovimentacao(state) {
            state.ModalEditMovimentacao != state.ModalEditMovimentacao
        },
        updateMovimentoItemData(state, payload) {
            state.MovimentoItem.data = payload
        },
        updateMovimentoItemDescricao(state, payload) {
            state.MovimentoItem.descricao = payload
        },
        updateMovimentoItemValor(state, payload) {
            state.MovimentoItem.valor = payload
        },
        updateMovimentoItemCategoria(state, payload) {
            state.MovimentoItem.categoria = payload
        },
        MUTATION_ADD_ListaPendente(state, payload) {
            payload.forEach(element => {
                state.listaMovPendentes.push({
                    Select: false,
                    Data: element[0],
                    Descricao: element[2],
                    Valor: parseFloat(element[3])
                })
            });
        },
        MUTATION_DELETE_ListaPendente(state, payload) {
            var index = state.listaMovPendentes.findIndex(item => item.Data == payload.Data && item.Descricao == payload.Descricao && item.Valor == parseFloat(payload.Valor))
            if (index > -1) state.listaMovPendentes.splice(index, 1);
        },
        MUTATION_ADD_ITEM(state, payload) {
            state.item = payload
        },
        MUTATION_MOVIMENTOS(state, payload) {
            state.listaMovimentos = payload
        },
        MUTATION_MUDAR_CHECKED(state, payload) {
            var item = state.listaMovPendentes[payload];
            state.listaMovPendentes[payload].Select = !item.Select;
        },
        MUTATION_DELETE_MOVIMENTOS(state, payload) {
            var index = state.listaMovPendentes.find((listaItem) => {
                return listaItem.id === payload
            })
            var item = state.listaMovPendentes.indexOf(index);
            if (index > -1) state.listaMovPendentes.splice(index, 1);
        },
        MUTATION_CINCO_ULTIMOS(state, payload) {
            state.listaUltimosMovimentos = payload;
        },
        OrdenarMovimentos(state, payload) {
            state.ValorOrder = !state.ValorOrder
            var itens = state.listaMovimentos
            switch (payload.Item) {
                case "Categoria":
                    switch (state.ValorOrder) {
                        case true:
                            state.listaMovimentos = itens.sort((a, b) => {
                                if (a.categoria < b.categoria) {
                                    return -1;
                                }
                                if (a.categoria > b.categoria) {
                                    return 1;
                                }
                                return 0;
                            })
                            break;
                        case false:
                            state.listaMovimentos = itens.reverse((a, b) => {
                                if (a.categoria < b.categoria) {
                                    return -1;
                                }
                                if (a.categoria > b.categoria) {
                                    return 1;
                                }
                                return 0;
                            })
                        default:
                            break;
                    }
                    break;
                case "Valor":
                    switch (state.ValorOrder) {
                        case true:
                            state.listaMovimentos = itens.sort((a, b) => {
                                return parseFloat(a.valor.replace(',', '.')) - parseFloat(b.valor.replace(',', '.'))
                            })
                            break;
                        case false:
                            state.listaMovimentos = itens.reverse((a, b) => {
                                return parseFloat(a.valor.replace(',', '.')) - parseFloat(b.valor.replace(',', '.'))
                            })
                        default:
                            break;
                    }

                    break;
                case "Data":
                    switch (state.ValorOrder) {
                        case true:
                            state.listaMovimentos = itens.sort((a, b) => {
                            })
                            break;
                        case false:
                            state.listaMovimentos = itens.reverse((a, b) => {
                                return generateDate(a.data) - generateDate(b.data)
                            })
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        },
        Set_Movimento_Item(state, payload) {
            state.MovimentoItem = payload
        }
    },
    actions: {
        Action_Load_Movimento_Item({ commit }, payload) {
            commit("updateModalEditMovimentacao")
            commit("Set_Movimento_Item", payload)
        },
        CarregarCincoUltimosMovimentos({ commit }) {
            Service.Movimento.UltimosLancamentos().then(response => {
                commit("MUTATION_CINCO_ULTIMOS", response.data);
            })
        },
        async Action_LoadList({ commit }) {
            await Service.Movimento.listarMovimento().then((response) => {
                commit("MUTATION_MOVIMENTOS", response.data)
            })
        },
        async Action_Update_Movimento({ getters }) {
            var item = getters.Get_Movimento_Item

            var categoria = getters.GetListaCategoria

            var idCategoria = categoria.find(x => x.nome == item.categoria)
            var movimento = {
                Id: item.id,
                Valor: parseFloat(item.valor.replace(',', '.')),
                Descricao: item.descricao,
                CategoriaId: idCategoria.id,
                Data: item.data,
                Status: item.status

            }
            await Service.Movimento.AtualizarMovimento(movimento).then((response) => {
                alert(response.data)
            }).catch((error) => {
                alert(error.toString())
            }).finally(() => {
                $("#MovimentoEditModal").modal('toggle')
            })
        },
        checkedItem({ commit }, payload) {
            commit('MUTATION_MUDAR_CHECKED', payload);
        },
        RemoverPendente({ commit }, payload) {
            if (payload != undefined) {
                commit("MUTATION_DELETE_ListaPendente", payload)
            }
        },
        CarregarCsv({ commit }, payload) {
            commit("MUTATION_ADD_ListaPendente", payload)
        },
        InserirMovimento({ commit }, payload) {
            commit("updateLoading", true)
            Service.Movimento.postMovimento(payload).then((response) => {
                commit("MUTATION_DELETE_ListaPendente", payload)
                alert(response.data)
            })
            commit("updateLoading", false)
        },
        InserirListaMovimentos({ commit }, payload) {
            Service.Movimento.postListMovimentos(payload).then((response) => {
                payload.forEach(element => {
                    commit("MUTATION_DELETE_ListaPendente", element)
                });
                alert(response.data)
            })
        },
        UploadSelects({ getters }) {
            var itens = getters.GET_SELECT_ITEMS;
            return itens;
        },
        Action_Buscar({ commit }, payload) {
            commit('updateSearch', payload)
        },
        //CARREGAR ITEM
        CarregarItem({
            commit,
            state
        }, payload) {
            var item = state.listaMovPendentes.find(x => x.id === payload.id)
            commit("MUTATION_ADD_ITEM", item)
        },
        //ATUALIZAR ITEM
        AtualizarPendente({
            commit
        }, payload) {
            Service.Movimento.AtualizarMovimento(payload).then((response) => {
                commit("MUTATION_DELETE_ListaPendente", payload)
            })
        },
        //REMOVER ITEM 
        Action_Delete_Movimento(payload) {
            var codigo = parseIntint(payload)
            Service.Movimento.DeletarMovimento(payload).then((response) => {
                commit("MUTATION_DELETE_MOVIMENTOS", payload);
                alert(response.data)
            })
        }
    },
    getters: {
        Get_Modal_Edit_Movimentacao(state) {
            return state.ModalEditMovimentacao
        },
        GETLISTA(state) {
            return state.listaMovPendentes
        },
        GetItem(state) {
            return (id) => {
                return state.listaMovPendentes.find((receitaItem) => {
                    return receitaItem.id === id
                })
            }
        },
        Get_Movimento_Item(state) {
            return state.MovimentoItem
        },
        GetUniqueItem(state) {
            return state.item
        },
        GetMovimentos(state) {
            return (search) => {
                /* if(search.value != ''){
                    switch (search.id) {
                        case 0:
                            
                            break;
                        case 1:
                            return state.listaMovimentos.filter((x) => {
                                return x.descricao.includes(search.value);
                            })
                            break;
                        case 2:
                            return state.listaMovimentos.filter((x) => {
                                console.log(search)
                                return x.data.includes(search.value);
                            })
                            break;
                        case 3:
                            return state.listaMovimentos.filter((x) => {
                                return x.valor.includes(search.value);
                            })
                            break;
                        case 4:
                            return state.listaMovimentos.filter((x) => {
                                return x.categoria.includes(search.value);
                            })
                            break;
                        default:
                            
                            break;
                    }
                }
                else{
                    return state.listaMovimentos;
                } */
                
                if(search == ''){
                    return state.listaMovimentos
                }else{
                    var inde = state.listaMovimentos.filter((x) => {
                        return x.descricao.includes(search);
                      })

                    return inde
                }
            }
        },
        GET_Unique_Select(state) {
            return state.listaMovPendentes.find((selecao) => {
                if (selecao.Select) {
                    return true;
                }
            })
        },
        GET_SELECT_ITEMS(state) {
            var itens = []
            state.listaMovPendentes.find((selecao) => {
                if (selecao.Select) {
                    itens.push(selecao);
                }
            })
            return itens;
        },
        GET_CINCO_ULTIMOS(state) {
            return state.listaUltimosMovimentos;
        }
    },
    modules: {}
})
function dataAtualFormatada() {
    var data = new Date(),
        dia = data.getDate().toString(),
        diaF = (dia.length == 1) ? '0' + dia : dia,
        mes = (data.getMonth() + 1).toString(), //+1 pois no getMonth Janeiro começa com zero.
        mesF = (mes.length == 1) ? '0' + mes : mes,
        anoF = data.getFullYear();
    return diaF + "/" + mesF + "/" + anoF;
}
function generateDate(dataString) { /*24/04/2020 */
    var dia = dataString.substring(0, 1)
    var mes = dataString.substring(3, 4)
    var ano = dataString.substring(6, 9)
    return new Date(ano, mes, dia)
}
export default movimento_vuex