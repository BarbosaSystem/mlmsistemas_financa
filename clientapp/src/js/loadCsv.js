import {
  mapActions, mapGetters
} from "vuex";

var loadCsv = {
  methods: {
    ...mapActions(["CarregarCsv", "RemoverPendente"]),
    loadTextFromFile(ev) {
      var files = ev.target.files || ev.dataTransfer.files;
      if (!files.length) {
        return;
      } else {
        this.getAsText(files[0]);
      }
    },
    getAsText(FileToRead) {
      var reader = new FileReader();
      reader.readAsText(FileToRead);

      reader.onload = this.loadHandler;
      reader.onerror = this.errorHandler;
    },
    loadHandler(event) {
      var csv = event.target.result;
      this.processData(csv);
    },
    errorHandler(evt) {
      if (evt.target.error.name == "NotReadableError") {
        alert("Canno't read file !");
      }
    },
    processData(csv) {
      var allTextLines = csv.split(/\r\n|\n/);
      var lines = [];
      var tarr = [];
      var data = [];
      for (let index = 1; index < allTextLines.length; index++) {
        data = allTextLines[index].split(",");
        for (var j = 0; j < data.length; j++) {
          if (data[j] !== undefined || data[j] != "") {
            tarr.push(data[j].replace('"', "").replace('"', ""));
          }
        }
        data.Select = false;
        this.lista.push(data);
        
      }
      this.CarregarCsv(this.lista);
    },
  },
  computed: {
    ...mapGetters(["GETLISTA", "GET_Unique_Select", "GET_SELECT_ITEMS"])
  },
}

export default loadCsv