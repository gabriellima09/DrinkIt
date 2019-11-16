// Script para create de telefones

$(document).ready(function () {
    $('#btnSalvarCreateTel').click(function () {
        var OK = true;
        var DDD = $('#dddTelefone').val();
        var Numero = $('#numeroTelefone').val();

        $('.msgErro').hide();

        if (DDD.length < 2) {
            OK = false;
            $('#msgErroDDD').show();
        }

        if (Numero.length < 9) {
            OK = false;
            $('#msgErroNumero').show();
        }

        if (OK) {
            $('#FormCreateTelefone').submit();
        }
    });
});