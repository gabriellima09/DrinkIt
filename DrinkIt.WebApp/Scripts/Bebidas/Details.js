$(document).ready(function () {    

    $("#btnCalcularFrete").click(function () {
        $.ajax({
            dataType: "json",
            url: "/Pedidos/SimularFreteDetails/",
            data: { 'bebidaFrete': $("#bebidaFrete").val() },
            complete: function (data) {
                $("#ResultadoValorFrete").text('Frete: R$ ' + data.responseText.valueOf('frete').toString('2d') + ' aprox.');
                alert('deu certo');
            }
        }); 
    });
});
