//Script para exibição de bebidas na página inicial do sistema

$('#inputPesquisarBebidas').on('keypress', function (e) {
    if (e.which === 13) {

        BuscarTexto();

    }
});

$("#btnAplicarFiltros").click(function myfunction() {
    Buscar();
});

function Toggle() {
    $("#divFiltros").toggle(500);
}

function Buscar() {
    var idGaseificada = $('#selectGaseificada').val();
    var idTeor = $('#selectTeor').val();
    var idValor = $('#selectValor').val();
    var idTipo = $('#selectTipo').val();

    $.ajax({
        dataType: "html",
        type: "POST",
        url: "/Bebidas/PvDashBebidas",
        data: { idGas: idGaseificada, idTeor: idTeor, idValor: idValor, idTipo: idTipo },
        success: function (data) {
            $("#divDashBebidas").html(data);
            $("#btnFiltros").bind("click", Toggle);
            $("#btnAplicarFiltros").bind("click", Buscar);
        }
    });
}

function BuscarTexto() {
    var texto = $('#inputPesquisarBebidas').val();

    $.ajax({
        dataType: "html",
        type: "POST",
        url: "/Bebidas/PvDashBebidas",
        data: { textoBusca: texto },
        success: function (data) {
            $("#divDashBebidas").html(data);
            $("#btnFiltros").bind("click", Toggle);
            $("#btnAplicarFiltros").bind("click", Buscar);
            $('#inputPesquisarBebidas').bind("keypress", function (e) {
                if (e.which === 13) {
                    BuscarTexto();
                }
            });
        }
    });
}