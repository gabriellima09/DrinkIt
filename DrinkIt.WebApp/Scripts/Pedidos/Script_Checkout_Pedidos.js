//Script para Checkout de pedidos

$(document).ready(function () {

    VerificarValorTotal();

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

                    $('#hiddenValorCupom').val(data.responseJSON.Cupom[0].Valor.toFixed(2));

                    SubtrairValorTotal(data.responseJSON.Cupom[0].Valor.toFixed(2));

                    SetarValorCartoes();
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
                $('#hiddenValorFrete').val(data.responseJSON.frete.toString().replace('.', ','));
                AdicionarValorTotal(data.responseJSON.frete.toFixed(2));

                SetarValorCartoes();
            }
        }); 
    });

    $("#Pagar2Cartoes").click(function () {
        $("#divCartao2").toggle();

        if ($("#Pagar2Cartoes").prop('checked')) {
            SetarValorDoisCartoes();
            $('#valorCartao1').attr('readonly', false);
        } else {
            SetarValorUmCartao();
            $('#valorCartao1').attr('readonly', true);
        }
    });

    $("#btnFinalizarPedido").unbind().click(function () {
        $("#FormPedido").submit(); 
    });

    function AdicionarValorTotal(add) {
        var valor = $("#ValorTotalInicial").val();
        var desconto = $('#hiddenValorCupom').val();

        valor = valor.toString().replace(',', '.');
        add = add.toString().replace(',', '.');
        desconto = desconto.toString().replace(',', '.');

        valor = (parseFloat(valor) + parseFloat(add));

        valor = (parseFloat(valor) - parseFloat(desconto));

        valor = parseFloat(valor) < 0 ? 0 : valor;

        valor = parseFloat(valor).toFixed(2).toString().replace('.', ',');

        $("#ValorTotal").val(valor);
        $("#spanValorTotal").text("R$ " + valor);
        VerificarValorTotal();
    }

    function SubtrairValorTotal(sub) {
        var valor = $("#ValorTotalInicial").val();
        var frete = $('#hiddenValorFrete').val();

        valor = valor.toString().replace(',', '.');
        sub = sub.toString().replace(',', '.');
        frete = frete.toString().replace(',', '.');

        valor = (parseFloat(valor) - parseFloat(sub));

        valor = (parseFloat(valor) + parseFloat(frete));

        valor = parseFloat(valor).toFixed(2).toString().replace('.', ',');

        valor = parseFloat(valor) < 0 ? 0 : valor;

        $("#ValorTotal").val(valor);
        $("#spanValorTotal").text("R$ " + valor);
        VerificarValorTotal();
    }

    $("#Pagar2Cartoes").click(function () {
        if ($("#Pagar2Cartoes").prop('checked')) {
            $("#Pagar2Cartoes").prop('checked', true);
        } else {
            $("#Pagar2Cartoes").prop('checked', false);
        }
    });

    $(".pay").on('focusout', function () {
        var value = $(this).val();

        RecalcularValorCartoes(value, $(this).attr('id'));
    });

    function RecalcularValorCartoes(valorCartao, nomeInput) {

        valorCartao = parseFloat(valorCartao.toString().replace(',', '.')).toFixed(2)

        var valor = (parseFloat($("#ValorTotal").val().toString().replace(',', '.')) - valorCartao)

        if (nomeInput == 'valorCartao1') {
            $("#valorCartao2").val(valor.toFixed(2).toString().replace('.', ','));
        } else if (nomeInput == 'valorCartao2'){
            $("#valorCartao1").val(valor.toFixed(2).toString().replace('.', ','));
        }
    }

    function SetarValorCartoes() {
        if ($("#Pagar2Cartoes").prop('checked')) {
            SetarValorDoisCartoes();
        } else {
            SetarValorUmCartao();
        }
    }

    function SetarValorUmCartao() {
        var valor = $("#ValorTotal").val();

        $("#valorCartao1").val(valor.toString().replace('.', ','));
        
    }

    function SetarValorDoisCartoes() {
        var valor = parseFloat($("#ValorTotal").val().toString().replace(',', '.'));

        valor = (parseFloat(valor) / 2).toFixed(2);

        $("#valorCartao1").val(valor.toString().replace('.', ','));
        $("#valorCartao2").val(valor.toString().replace('.', ','));

    }

    function VerificarValorTotal() {

        var total = $('#ValorTotal').toString().replace('.', ',');

        if (parseFloat(total) >= 20) {
            $('#divCartao2').css('display', 'block');
        } else {
            $('#divCartao2').css('display', 'none');
        }
    }

});