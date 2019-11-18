

$(document).ready(function () {

    $('.btnExcluirTelefone').click(function () {

        var idTel = $(this).attr('data-id');
        var dddTel = $(this).attr('data-ddd');
        var numeroTel = $(this).attr('data-numero');
        $('#labelTelefoneExcluir').html('(' + dddTel + ')' + numeroTel);
        $('#hiddenIdTel').val(idTel);
        $('#modalExcluirTelefone').modal('show');
    });
    
    $('#btnSalvarSenha').click(function () {
        
        $.ajax({
            dataType: "json",
            method: 'POST',
            url: "/Clientes/ValidarTrocaSenha/",
            data: { 'senhaAtual': $("#inputSenhaAtual").val(), 'novaSenha': $('#inputNovaSenha').val(), 'confirmSenha': $('#inputConfirmarSenha').val() },
            complete: function (data) {

                if (data.responseJSON.ResultadoSenhaAtual == true) {
                    $("#labelSenhaAtualErrada").hide();

                    if (data.responseJSON.ResultadoNovaSenha == true) {
                        
                        $("#labelNovaSenhaFraca").hide();

                        if (data.responseJSON.ResultadoConfirmSenha == true) {
                            $("#labelConfirmacaoErrada").hide();
                            $('#FormNovaSenha').submit();
                        } else {
                            $("#labelConfirmacaoErrada").show();
                        }


                    } else {
                        $("#labelNovaSenhaFraca").show();
                        $("#labelConfirmacaoErrada").hide();
                    }

                } else {
                    $("#labelNovaSenhaFraca").hide();
                    $("#labelConfirmacaoErrada").hide();
                    $("#labelSenhaAtualErrada").show();
                }
            }
        }); 
    });

    $('.deletarNotificacao').click(function () {
        var id = $(this).attr('data-id');

        $.ajax({
            dataType: "json",
            method: 'POST',
            url: "/Clientes/RemoverNotificacao/",
            data: { 'idNotificacao': id },
            complete: function (data) {

            }
        }); 
    });
});