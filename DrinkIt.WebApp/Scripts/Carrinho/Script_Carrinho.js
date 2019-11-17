$(document).ready(function () {
    VerificaValores();
});

$(".Quantidade").change(function () {
    $.ajax({
        dataType: "json",
        method: 'POST',
        url: "/Pedidos/AtualizarQuantidadeBebida/",
        data: { 'idBebida': $(this).attr('data-id'), 'quantidade': $(this).val() },
    });

    VerificaValores();

});


function VerificaValores() {

    var total = 0;
    var valor = 0;
    var qtde = 0;


    $('.Quantidade').each(function () {
        qtde = $(this).val();
        valor = $(this).attr('data-value');

        qtde = qtde.toString().replace(',', '.');

        valor = valor.toString().replace(',', '.');

        total = total + (parseFloat(qtde) * parseFloat(valor));

    });

    $('#ddValorTotal').html('R$' + total.toFixed(2).toString().replace('.', ','));

    if (parseFloat(total) < 10) {
        $('#divPedidoSuficiente').css('display', 'none');
        $('#divPedidoInsuficiente').css('display', 'block');
    }
    else {
        $('#divPedidoSuficiente').css('display', 'block');
        $('#divPedidoInsuficiente').css('display', 'none');
    }
}