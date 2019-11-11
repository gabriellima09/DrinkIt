

$(document).ready(function () {

    
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

});