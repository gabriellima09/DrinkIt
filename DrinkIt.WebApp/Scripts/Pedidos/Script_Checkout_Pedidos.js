//Script para Checkout de pedidos

$(document).ready(function () {

    $('#btnAddCartao').click(function () {
        $('#divCartaoAdicional').css('display', 'block');
        $('#btnRemoverCartao').css('display', 'block');
        $('#btnAddCartao').css('display', 'none');
    });

    $('#btnRemoverCartao').click(function () {
        $('#divCartaoAdicional').css('display', 'none');
        $('#btnAddCartao').css('display', 'block');
        $('#btnRemoverCartao').css('display', 'none');
    });

    $('#btnNovoEndereco').click(function () {
        $('#divNovoEndereco').css('display', 'block');
        $('#btnNovoEnderecoFechar').css('display', 'block');
    });

    $('#btnNovoEnderecoFechar').click(function () {
        $('#divNovoEndereco').css('display', 'none');
        $(this).css('display', 'none');
    });

    $('#btnValidarCupom').click(function () {
        $.ajax({
            dataType: "json",
            method: 'POST',
            url: "/Cupons/ValidarCupom/",
            data: { 'cupom': $("#inputCupom").val() },
            complete: function (data) {

                if (data.responseJSON.Resultado == true) {
                    $("#spanCupomInvalido").hide();
                    $("#spanCupomValido").show();

                    $("#spanDesconto").text(data.responseJSON.Cupom[0].Valor.toFixed(2).toString().replace('.', ','));

                    $("#IdCupom").val(data.responseJSON.Cupom[0].Id);

                    $("#Desconto").val(data.responseJSON.Cupom[0].Valor.toString().replace('.', ','));
                } else {
                    $("#spanCupomValido").hide();
                    $("#spanCupomInvalido").show();
                }
            }
        }); 
    });

    $("#btnCalcularFrete").click(function () {
        $.ajax({
            dataType: "json",
            method: 'POST',
            url: "/Pedidos/CalcularFretePedido/",
            data: { 'pedido': $("#pedido").val() },
            complete: function (data) {
                $("#ResultadoValorFrete").text('Frete: R$ ' + data.responseJSON.frete.toFixed(2));
                $("#Frete").val(data.responseJSON.frete.toString().replace('.', ','));
                $("#spanFrete").text(data.responseJSON.frete.toFixed(2).toString().replace('.', ','));
            }
        }); 
    });

    $("#2cartoes").click(function () {
        $("#divCartao2").toggle();
    });

    $("#btnFinalizarPedido").unbind().click(function () {
        $("#FormPedido").submit(); 
    });
});