/* export function SomaAgrupada(antigo) {
    var resultado = [];

    antigo.reduce(function(novo, item) {
      if (!novo[item.categoria]) {
        novo[item.categoria] = {
          valor: 0,
          nome: item.categoria
        };

        resultado.push(novo[item.categoria]);
      }

      novo[item.categoria].valor += item.valor;

      return novo;
    }, {});

    return resultado;
  } */

function colorsBackground(index){
  var lista = ["#3498db", // Blue
  "#2c3e50",
  "#27ae60",
  "#ff6361",
  "#f1c40f"]
  return lista[index]
}

export default colorsBackground