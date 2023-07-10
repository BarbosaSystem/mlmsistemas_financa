<template>
  <div class="grafico">
    <canvas id="planet-chart"></canvas>
  </div>
</template>
<script>
export default {
  mounted() {
    this.loadingChart();
  },
  computed: {
    numeros() {
      let valor = [];
      this.valores.forEach(element => {
        valor.push(element.valor.toFixed(2));
      });
      return valor;
    }
  },
  props: ["itens", "valores"],
  methods: {
    emitClick(valor) {
      this.$emit("gerar-pdf", valor);
    },
    loadingChart() {
      var vm = this;
      const ctx = document.getElementById("planet-chart");
      const Clicar = this.emitClick();
      const myChart = new Chart(ctx, {
        type: "doughnut",
        data: {
          datasets: [
            {
              data: this.numeros,
              backgroundColor: [
                "#3498db", // Blue
                "#2c3e50",
                "#27ae60",
                "#ff6361",
                "#f1c40f"
              ]
            }
          ],

          // These labels appear in the legend and in the tooltips when hovering different arcs
          labels: this.itens
        },
        options: {
          responsive: true,
          legend: {
            position: "right"
          },
          animation: {
            animateScale: true,
            animateRotate: true
          }
        }
      });
      ctx.onclick = function(evt) {
        var activePoints = myChart.getElementAtEvent(evt);
        if (activePoints[0]) {
          var chartData = activePoints[0]["_chart"].config.data;
          var idx = activePoints[0]["_index"]; //indice do array
          var valor = chartData.labels;
          if(confirm("Deseja gerar relatório da categoria '" + valor[idx] + "'")){
              vm.$emit("gerar-pdf", valor[idx]);
          }
        }
      };
    }
  }
};
</script>