$(document).ready(function () {    

    $("#btnCalcularFrete").click(function () {

        if ($("#Cep").val().length == 9) {
            $.ajax({
                dataType: "json",
                url: "/Pedidos/SimularFreteDetails/",
                data: { 'bebidaFrete': $("#bebidaFrete").val() },
                complete: function (data) {
                    $('#lbValorFrete').show();
                    $('#lbCepInvalido').hide();
                    $("#ResultadoValorFrete").text('Frete: R$ ' + data.responseText.valueOf('frete').toString('2d') + ' aprox.');
                }
            });
        } else {
            $('#lbValorFrete').hide();
            $('#lbCepInvalido').show();
        }
        
    });
});
