<template>
  <div class="grafico-barra" style="padding: 20px">
    <div class="card">
      <div class="card-body">
        <div class="form-group">
          <label for="">Quantidade de Registros: {{valores.length}}</label>
          <br>
          <label for="">Total: {{soma_Valores}}</label>
          <br>
          <label for="">Media: {{Media}}</label> 
        </div>
        <canvas id="grafico_barra" width="70%"></canvas>
      </div>
    </div>
    
  </div>
</template>
<script>
export default {
  props: ["itens", "valores"],
  mounted() {
    this.LoadingBarChart();
  },
  computed: {
    Media(){
      var soma = this.valores.reduce(function(soma, i) {
          return soma + i;
      }).toFixed(2)

      return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
      }).format((soma / this.valores.length).toFixed(2));
    },
    soma_Valores(){
      var soma_val = this.valores.reduce(function(soma, i) {
          return soma + i;
      }).toFixed(2)

      return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
      }).format(soma_val);
    }
  },
  methods: {
    LoadingBarChart() {
      const ctx = document.getElementById(
        "grafico_barra"
      );
      const myChart = new Chart(ctx, {
        type: "bar",
        data: {
          labels: this.itens,
          datasets: [
            {
              backgroundColor: new flatColor().hex,
              data: this.valores,
            },
          ],
        },
        options: {
            scales: {
        xAxes: [{
          ticks: {}
        }],
        yAxes: [{
          ticks: {
                    beginAtZero: true,
                    // Return an empty string to draw the tick line but hide the tick label
                    // Return `null` or `undefined` to hide the tick line entirely
                    userCallback: function(value, index, values) {
                        // Convert the number to a string and splite the string every 3 charaters from the end
                        value = value.toString();
                        value = value.split(/(?=(?:...)*$)/);
                        
                        // Convert the array to a string and format the output
                        value = value.join('.');
                        return 'R$ ' + value;
                        }
                }
                }]
            },
          responsive: true,
          legend: {
            position: "right",
            display: false
          },
          animation: {
            animateScale: true,
            animateRotate: true,
            tension: {
              duration: 1000,
              easing: 'linear',
              from: 1,
              to: 0,
              loop: true
            }
          },
        },
      });
    },
  },
};
</script>