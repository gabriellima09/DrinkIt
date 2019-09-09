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

    $('#btnNovoEndereco').click(function () {
        $('#divNovoEndereco').css('display', 'block');
        $('#btnNovoEnderecoFechar').css('display', 'block');
    });

    $('#btnNovoEnderecoFechar').click(function () {
        $('#divNovoEndereco').css('display', 'none');
        $(this).css('display', 'none');
    });

    $('#btnValidarCupom').click(function () {
        $('#spanCupomValido').css('display', 'block');
    });
});