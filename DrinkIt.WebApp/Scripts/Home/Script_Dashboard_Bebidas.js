//Script para exibição de bebidas na página inicial do sistema



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

    $.ajax({
        dataType: "html",
        type: "POST",
        url: "/Bebidas/PvDashBebidas",
        data: { idGas: idGaseificada, idTeor: idTeor, idValor: idValor },
        success: function (data) {
            $("#divDashBebidas").html(data);
            $("#btnFiltros").bind("click", Toggle);
            $("#btnAplicarFiltros").bind("click", Buscar);
        }
    });
}