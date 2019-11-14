//Script para a index de administração  

$('#inputBuscaCliente').on('keypress', function (e) {
    if (e.which === 13) {

        BuscarTexto();

    }
});

function BuscarTexto() {
    var texto = $('#inputBuscaCliente').val();

    $.ajax({
        dataType: "html",
        type: "POST",
        url: "/Clientes/PvCliente",
        data: { textoBusca: texto },
        success: function (data) {
            $("#divPvClientes").html(data);
        }
    });
}

function InativarBebida() {
    var motivo = $('#txMotivo').val();

    if (motivo != '') {
        $('#msgErroMotivo').hide();
        $('#FormInativarBebida').submit();
    } else {
        $('#msgErroMotivo').show();
    }
}

function AtivarBebida() {
    var motivo = $('#txMotivoAtivar').val();

    if (motivo != '') {
        $('#msgErroMotivoAtivar').hide();
        $('#FormAtivarBebida').submit();
    } else {
        $('#msgErroMotivoAtivar').show();
    }
}

function EntradaEstoque() {
    /*QtdeEntrada, FornecedorEntrada, VlrCustoEntrada, DataEntrada*/
    $('.msgErroEntrada').hide();
    var OK = true;

    if ($('#QtdeEntrada').val() == '') {
        OK = false;
        $('#msgErroQtde').show();
    }

    if ($('#FornecedorEntrada').val() == '') {
        OK = false;
        $('#msgErroFornecedor').show();
    }

    if ($('#VlrCustoEntrada').val() == '') {
        OK = false;
        $('#msgErroValor').show();
    }

    if ($('#DataEntrada').val() == '') {
        OK = false;
        $('#msgErroData').show();
    }

    if (OK) {
        $('#FormEntradaEstoque').submit();
    }


}

function BaixaEstoque() {
    if ($('#QtdeBaixaEstoque').val() != '') {
        $('#msgErroBaixaEstoque').hide();
        $('#FormBaixaEstoque').submit();
    } else {
        $('#msgErroBaixaEstoque').show();
    }
}

function AbrirModalEntradaEstoque(IdBebida, DescBebida, Qtde) {

    $('#modalEntradaEstoque').modal('show');
    $('#hiddenIdBebida').val(IdBebida);
    $('#ddCodigoEnt').html(IdBebida);
    $('#ddDescEnt').html(DescBebida);
    $('#ddQtdeEnt').html(Qtde);
}

function AbrirModalBaixaEstoque(idBebida, DescBebida, Qtde) {
    $('#modalBaixaEstoque').modal('show');
    $('#hiddenIdBebidaBaixa').val(idBebida);
    $('#ddCodigoBaixa').html(idBebida);
    $('#ddDescBaixa').html(DescBebida);
    $('#ddQtdeBaixa').html(Qtde); 
}