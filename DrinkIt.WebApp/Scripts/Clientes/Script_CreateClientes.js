//Script para a tela create de clientes

$('#clienteCpf').mask('000.000.000-00', { reverse: true });
$('#clienteCep').mask('00000-000', { reverse: true });
$('#clienteNumCartao').mask('0000-0000-0000-0000', { reverse: true });
$('#clienteCodSeg').mask('000', { reverse: true });
$('#clienteMesVal').mask('00', { reverse: true });
$('#clienteAnoVal').mask('0000', { reverse: true });

$(document).on("focus", ".dddtel", function () {
    $(this).mask("00");
});

$(document).on("focus", ".numtel", function () {
    $(this).mask("00000-0000");
});

function Add() {
    //var counter = $('#hiddenCounter').val();
    //counter++;
    //$('#hiddenCounter').val(counter);
    $("#tbTelefones tbody").append(
        "<tr>" +
        '<td><input type="text" maxlength="2" class="form-control dddtel" name="LstDDD" /></td>' +
        '<td><input type="text" maxlength="9" class="form-control numtel" name="LstTelefone" /></td>' +

        '<td>' +
        '<select class= "form-control" name = "TiposTelefone" >' +
        '<option value="1">Residencial</option>' +
        '<option value="2">Celular</option>' +
        '<option value="3">Comercial</option>' +
        '</select>' +
        '</td >' +

        '<td style="text-align:center"><button type="button" class="btnDeleteTel btn btn-sm btn-danger"><span class="glyphicon glyphicon-remove"></span> Remover</button></td>' +
        "</tr>");

    $(".btnDeleteTel").bind("click", Delete);
}

/*
 $('#inputPesquisarBebidas').bind("keypress", function (e) {
                if (e.which === 13) {
                    BuscarTexto();
                }
            });
 * */

function Delete() {
    //var counter = $('#hiddenCounter').val();
    //if (counter > 0) {
    var par = $(this).parent().parent(); //tr
    par.remove();
    //    counter = counter - 1;
    //    $('#hiddenCounter').val(counter);
    //}    

}

$("#btnAddTel").click(function () {
    Add();
});

//$('#btnCadastrar').click(function () {


//    $('#FormCreateBebida').submit();
//});