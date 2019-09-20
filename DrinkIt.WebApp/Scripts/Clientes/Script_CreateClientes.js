//Script para a tela create de clientes

function Add() {
    //var counter = $('#hiddenCounter').val();
    //counter++;
    //$('#hiddenCounter').val(counter);
    $("#tbTelefones tbody").append(
        "<tr>" +
        '<td><input type="text" maxlength="2" class="form-control" name="LstDDD" /></td>' +
        '<td><input type="text" maxlength="9" class="form-control" name="LstTelefone" /></td>' +

        '<td>' +
        '<select class= "form-control" name = "TiposTelefone" >' +
        '<option value="0">Selecione um tipo</option>' +
        '<option value="1">Residencial</option>' +
        '<option value="2">Celular</option>' +
        '<option value="3">Comercial</option>' +
        '</select>' +
        '</td >' +

        '<td style="text-align:center"><button type="button" class="btnDeleteTel btn btn-sm btn-danger"><span class="glyphicon glyphicon-remove"></span> Remover</button></td>' +
        "</tr>");

    $(".btnDeleteTel").bind("click", Delete);

}

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