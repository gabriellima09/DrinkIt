$(document).ready(function () {
    $("#btnCalcularFrete").click(function myfunction() {
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/Pedidos/CalcularFretePedido",
            data: { pedido: pedido },
            success: function (data) {
                $("#ResultadoValorFrete").text(data.Valor);
            }
        });
    });
});