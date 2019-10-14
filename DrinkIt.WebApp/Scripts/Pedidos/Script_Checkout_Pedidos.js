//Script para Checkout de pedidos

$(document).ready(function () {

    $('#btnAddCartao').click(function () {
        $('#divCartaoAdicional').css('display', 'block');
        $('#btnRemoverCartao').css('display', 'block');
        $('#btnAddCartao').css('display', 'none');
        $(".newCart").removeAttr('disabled');
    });

    $('#btnRemoverCartao').click(function () {
        $('#divCartaoAdicional').css('display', 'none');
        $('#btnAddCartao').css('display', 'block');
        $('#btnRemoverCartao').css('display', 'none');
        $(".newCart").attr('disabled', 'disabled');
    });

    $('#btnNovoEndereco').click(function () {
        $('#divNovoEndereco').css('display', 'block');
        $('#btnNovoEnderecoFechar').css('display', 'block');
        $('.newEnd').removeAttr('disabled');
    });

    $('#btnNovoEnderecoFechar').click(function () {
        $('#divNovoEndereco').css('display', 'none');
        $(this).css('display', 'none');
        $('.newEnd').attr('disabled', 'disabled');
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
                AdicionarValorTotal(data.responseJSON.frete.toFixed(2));

                SetarValorCartoes();
            }
        }); 
    });

    $("#Pagar2Cartoes").click(function () {
        $("#divCartao2").toggle();
    });

    $("#btnFinalizarPedido").unbind().click(function () {
        $("#FormPedido").submit(); 
    });

    function AdicionarValorTotal(add) {
        var valor = $("#ValorTotal").val();

        valor = valor.toString().replace(',', '.');
        add = add.toString().replace(',', '.');

        valor = (parseFloat(valor) + parseFloat(add));

        valor = parseFloat(valor).toFixed(2).toString().replace('.', ',');

        $("#ValorTotal").val(valor);
        $("#spanValorTotal").text("R$ " + valor);
    }

    function SubtrairValorTotal(sub) {
        var valor = $("#ValorTotal").val();

        valor = valor.toString().replace(',', '.');
        sub = sub.toString().replace(',', '.');

        valor = (parseFloat(valor) - parseFloat(sub));

        valor = parseFloat(valor).toFixed(2).toString().replace('.', ',');

        valor = parseFloat(valor) < 0 ? 0 : valor;

        $("#ValorTotal").val(valor);
        $("#spanValorTotal").text("R$ " + valor);
    }

    $("#Pagar2Cartoes").click(function () {
        if ($("#Pagar2Cartoes").prop('checked')) {
            $("#Pagar2Cartoes").prop('checked', true);
        } else {
            $("#Pagar2Cartoes").prop('checked', false);
        }
    });

    $(".pay").on('focusout', function () {
        var thisVal = parseFloat($(this).val().toString().replace(',','.'));
        var valor = parseFloat($("#ValorTotal").val().toString().replace(',', '.'));

        console.log('thisVal', thisVal)
        console.log('valor', valor)

        var resto = parseFloat(valor - thisVal);

        if ($("#Pagar2Cartoes").prop('checked') &&
            ($("#IdCartao2 :selected").val() > 0 || !$("#valorNovoCartao").attr('disabled') == 'disabled')) {
            if ($("#valorNovoCartao").attr('disabled') == 'disabled') {
                $("#valorCartao1").val(resto);
            } else {
                $("#valorNovoCartao").val(resto);
            }
        } else {
            if ($("#valorNovoCartao").attr('disabled') == 'disabled') {
                $("#valorCartao1").val(resto);
            } else {
                $("#valorNovoCartao").val(resto);
            }
        }
    });

    function SetarValorCartoes() {
        if ($("#Pagar2Cartoes").prop('checked') &&
            ($("#IdCartao2 :selected").val() > 0 || !$("#valorNovoCartao").attr('disabled') == 'disabled')) {
            SetarValorDoisCartoes();
        } else {
            SetarValorUmCartao();
        }
    }

    function SetarValorUmCartao() {
        var valor = $("#ValorTotal").val();

        if ($("#valorNovoCartao").attr('disabled') == 'disabled') {
            $("#valorCartao1").val(valor);
        } else {
            $("#valorNovoCartao").val(valor);
        }
    }

    function SetarValorDoisCartoes() {
        var valor = $("#ValorTotal").val();

        if ($("#valorNovoCartao").attr('disabled') == 'disabled') {

            $("#valorCartao1").val(parseFloat(valor) / 2);
            $("#valorCartao2").val(parseFloat(valor) / 2);
            
        }else {

            $("#valorCartao1").val(parseFloat(valor) / 2);
            $("#valorNovoCartao").val(parseFloat(valor) / 2);
        }
    }
});