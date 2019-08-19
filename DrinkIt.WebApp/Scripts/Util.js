$(document).ready(function () {
    $("#btnCarrinho").click(function () {
        console.log('click')
        window.location.href = '/Carrinho/Index/';
    });

    $("#btnFiltros").click(function () {
        $("#divFiltros").toggle(500);
    });
});