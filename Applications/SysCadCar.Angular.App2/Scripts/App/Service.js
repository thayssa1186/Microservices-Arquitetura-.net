app.service("syscadcarAJService", function ($http) {

    
    this.getCars = function () {
        return $http.get("Home/GetAllCars");
    };

    // get Book by bookId
    this.getCar = function (placa) {
        var response = $http({
            method: "post",
            url: "Home/Details",
            params: {
                Placa: JSON.stringify(placa)
            }
        });
        return response;
    }

    this.AddCar = function (Carro) {
        var response = $http({
            method: "post",
            url: "Home/Create",
            data: JSON.stringify(Carro),
            dataType: "json"
        });
        return response;
    }

});