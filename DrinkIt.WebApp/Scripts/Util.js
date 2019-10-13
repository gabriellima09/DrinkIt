$(document).ready(function () {
    $("#btnCarrinho").click(function () {
        window.location.href = '/Carrinho/Index/';
    });

    $("#btnFiltros").click(function () {
        $("#divFiltros").toggle(500);
    });
});