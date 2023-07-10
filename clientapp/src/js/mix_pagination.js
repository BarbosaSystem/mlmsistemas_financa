import * as jsPDF from "jspdf"
import Service from '../Service/movimento'
import ModalEdit from '../components/Movimento/ModalEditMovimentacao';
import {
    Date
} from "core-js";
var mix_pagination = {
    components: {
        ModalEdit
    },
    data() {
        return {
            listaMovimentos: [],
            pagination: {
                totalPages: 0,
                currentPage: 0,
                itensPage: 10
            }
        }
    },
    mounted() {

    },
    methods: {
        RetornaDataHoraAtual() {
            var dNow = new Date();
            var localdate = dNow.getDate() + '/' + (dNow.getMonth() + 1) + '/' + dNow.getFullYear() + ' ' + dNow.getHours() + ':' + dNow.getMinutes();
            return localdate;
        },
        Exportar() {
            this.GeneratePdf();
        },
        GeneratePdf(categoria) {
            if (categoria) {
                var pdf = new jsPDF("p", "pt", "a4"); // A4 size page of PDF
                pdf.setFontSize(14);
                pdf.setFontType('bold');
                let pagina = 1;
                let x = 25;
                let y = 130;
                pdf.text(pagina.toString(), 550, 800);
                pdf.text("Cartão Nubank", 245, 45);
                pdf.text("Categoria: " + categoria, 25, 65);
                pdf.text("Data: " + this.RetornaDataHoraAtual(), 25, 80);
                pdf.text("Descrição", 55, 100)
                pdf.text("Valor", 500, 100)
                pdf.setFontSize(12);
                pdf.setFontType('normal');

                var valorSoma = 0;
                for (var index = 0; index < this.listaMovimentosPeriodo.length; index++) {
                    var element = this.listaMovimentosPeriodo[index];
                    if (element.categoria == categoria) {
                        valorSoma += element.valor;
                        pdf.text(element.id.toString(), x, y);
                        pdf.text(element.descricao, x + 30, y);
                        pdf.text("R$ " + element.valor.toFixed(2).replace('.', ','), x + 500, y);
                        y += 30;
                        if (y >= 770) {
                            x = 25;
                            y = 80;
                            pagina++;
                            pdf.addPage();
                            pdf.text("Cartão Nubank", 245, 45);
                            pdf.text(pagina.toString(), 550, 800);
                        }
                    }
                }

                pdf.text("Total: R$" + valorSoma.toFixed(2).replace('.', ','), 25, 775);

                if (categoria === "Casal") {
                    pdf.text("Valor dividido: R$" + (valorSoma/2).toFixed(2).replace('.', ','), 25, 820);
                }
                var nomestring = categoria + ".pdf";
                pdf.save(nomestring);
            }
        },
        TotalPaginas(lista) {
            return this.pagination.totalPages = Math.ceil(lista.length / this.pagination.itensPage)
        },
        ItemAtual(codigo) {
            this.pagination.currentPage = codigo
        },
        NextPage() {
            if (this.pagination.currentPage < this.pagination.totalPages) {
                this.pagination.currentPage += 1
            }
        },
        PreviousPage() {
            if (this.pagination.currentPage > 0) {
                this.pagination.currentPage -= 1
            }
        },
        
        EditarMovimento(item) {
            this.$root.$emit("ModalExportMovimento::show", item);
        }
    },
    computed: {
        Registros() {
            return "Registros encontrados: " + this.listaMovimentos.length;
        },
        ValorDivido() {
            var total = this.listaMovimentosPeriodo.reduce((prev, cur) => {
                return prev + cur.valor;
            }, 0);
            return "R$ " + (total / 2).toFixed(2).replace(".", ",");
        },
        SomaTotal() {
            var total = this.listaMovimentosPeriodo.reduce((prev, cur) => {
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
}

export default mix_pagination