﻿//Script para Checkout de pedidos

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

                    $('#hiddenValorCupom').val(data.responseJSON.Cupom[0].Valor.toFixed(2).toString().replace('.', ','));

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

        var tamanhoCep = $('#Cep').val().length;

        if (tamanhoCep == 9) {
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
                                        
                    $('#hiddenValidarCep').val(1);
                    $('#msgCepInvalido').hide();

                    SetarValorCartoes();
                }
            });
        } else {
            $("#ResultadoValorFrete").text('');
            $('#hiddenValidarCep').val(0);
            $('#msgCepInvalido').show();
        }

        
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
        var flgValidacao = 1;
        var valEndereco = $('#dropEndereco').val();
        var valCartao1 = $('#dropCartao1').val();
        var valCartao2 = $('#dropCartao2').val();
        var valCep = $('#hiddenValidarCep').val();

        if (valEndereco == '0') {
            $('#msgEnderecoInvalido').show();
            flgValidacao = 0;
        } else {
            $('#msgEnderecoInvalido').hide();
        }

        if (valCartao1 == '0') {
            $('#msgCartaoInvalido').show();
            flgValidacao = 0;
        } else {
            $('#msgCartaoInvalido').hide();
        }

        if (valCep == 0) {
            $('#msgCepInvalido').show();
            flgValidacao = 0;
        }

        if (valCartao2 == '0' && $("#Pagar2Cartoes").prop('checked') == true) {
            $('#msgCartaoInvalido2').show();
            flgValidacao = 0;
        } else {
            $('#msgCartaoInvalido2').hide();
        }


        if (flgValidacao == 1) {
            $("#FormPedido").submit();
        }
        
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

        //$("#ValorTotal").val(valor);
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

        if (parseFloat(valor) < 0) {
            var valorTroco;
            valorTroco = $("#ValorTotalInicial").val();

            valorTroco = valorTroco.toString().replace(',', '.');

            valorTroco = (parseFloat(valorTroco) + parseFloat(frete));

            valorTroco = (parseFloat(sub) - parseFloat(valorTroco));

            valorTroco = parseFloat(valorTroco).toFixed(2).toString().replace('.', ',');

            
            $('#spanVlrTroco').html('R$' + valorTroco);
            $('#warnTroco').css('display', 'block');
        } else {
            $('#warnTroco').css('display', 'none');
        }

        valor = parseFloat(valor) < 0 ? 0 : valor;

        //$("#ValorTotal").val(valor);
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

        valorCartao = parseFloat(valorCartao.toString().replace(',', '.')).toFixed(2);

        //var valorTotalCompra = parseFloat($("#ValorTotal").val()).toFixed(2);

        var valor = (parseFloat($("#ValorTotal").val().toString().replace(',', '.')) - parseFloat(valorCartao));
        if (valorCartao < 10) {
            valorCartao = 10;
            valor = (parseFloat($("#ValorTotal").val().toString().replace(',', '.')) - parseFloat(valorCartao));
        } else if (valor < 10) {
            valor = 10;
            valorCartao = (parseFloat($("#ValorTotal").val().toString().replace(',', '.')) - parseFloat(valor));
        }
        
        if (nomeInput == 'valorCartao1') {
            $('#valorCartao1').val(parseFloat(valorCartao).toFixed(2).toString().replace('.', ','));
            $('#valorCartao2').val(valor.toFixed(2).toString().replace('.', ','));
            
        } else if (nomeInput == 'valorCartao2') {
            $('#valorCartao2').val(parseFloat(valorCartao).toFixed(2).toString().replace('.', ','));
            $('#valorCartao1').val(valor.toFixed(2).toString().replace('.', ','));
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

        var total = $('#ValorTotal').val().toString().replace('.', ',');

        if (parseFloat(total) >= 20) {
            $('#divCheckCartao2').css('display', 'block');
        } else {
            $('#divCheckCartao2').css('display', 'none');
        }
    }

});