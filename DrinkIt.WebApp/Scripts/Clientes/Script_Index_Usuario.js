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