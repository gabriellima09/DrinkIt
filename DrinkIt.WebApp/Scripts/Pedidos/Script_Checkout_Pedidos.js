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
});