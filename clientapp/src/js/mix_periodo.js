import Service from '../Service/periodo'
var mix_pagination = {
    data() {
        return {
            listaPeriodo: [],
            periodo: {
                referencia: '',
                DataInicial: '',
                DataFinal: ''
            }
        }
    },
    mounted() {

    },
    
    methods: {
        CadastrarPeriodo(periodo){
            Service.postPeriodo(periodo).then((response) => {
                alert(response.data)
            })
        },
        ExcluirPeriodo(codigo){
            if(confirm("Deseja excluir este registro?")){
                Service.DeletarMovimento(codigo).then( (response) => {
                    alert(response.data)
                })
            }
        },
        EditarPeriodo(item){
            this.$root.$emit("ModalExportMovimento::show", item);
        }
    },
    computed: {
        Registros() {
            return "Registros encontrados: " + this.listaMovimentos.length;
        },
        Categorias(){
            if(this.listaMovimentosPeriodo.length > 0)
                return [... new Set(this.listaMovimentosPeriodo.map(x => x.categoria))]
            else
                return
        },
        ValorDivido() {
            var total = this.listaMovimentos.reduce((prev, cur) => {
                return prev + cur.valor;
            }, 0);
            return "R$ " + (total / 2).toFixed(2).replace(".", ",");
        },
        SomaTotal() {
            var total = this.listaMovimentos.reduce((prev, cur) => {
                return prev + cur.valor;
            }, 0);
            return "R$ " + total.toFixed(2).replace(".", ",");
        },
        anterior() {
            return this.pagination.currentPage === 0
        },
        ultimo() {
            return this.pagination.currentPage == this.pagination.totalPages - 1
        },
    },
    watch: {
        listarMovimentos(old, new_v) {
            if (this.Search.length === 0) {
                this.TotalPaginas(this.listaMovimentos);
                this.ItemAtual(0);
            } else {
                this.TotalPaginas(new_v)
                this.ItemAtual(0);
            }
        }
    },
}

export default mix_pagination