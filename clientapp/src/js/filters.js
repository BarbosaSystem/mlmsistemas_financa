var filters = {
    filters: {
        ConverterData(valor) {
            if(valor){
                var teste = new Date(valor)
                return teste.toLocaleDateString()
            }
        },
        ConverterFloat(valor) {
            if(valor){
                var valor = parseFloat(valor).toFixed(2);
                return "R$ " + valor.replace(".", ",");
            }
            else{
                return ""
            }
        },
        ConvertFloatPure(valor){
            var valor = parseFloat(valor).toFixed(2);
            return valor.replace(".", ",");
        },
        rgbToHex(r, g, b) {
            return "#" + ((1 << 24) + (r << 16) + (g << 8) + b).toString(16).slice(1);
        },
    }
}

export default filters