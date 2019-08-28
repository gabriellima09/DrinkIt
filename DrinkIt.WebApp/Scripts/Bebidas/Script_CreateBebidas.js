//Script para a tela de create de bebidas
function Add() {
    var counter = $('#hiddenCounter').val();

    counter++;
    $('#hiddenCounter').val(counter);
    $("#tbIngredientes tbody").append(
        "<tr>" +
        '<td><input type="text" class="form-control" name="LstIngrediente" /></td>'+
        '<td style="text-align:center"><button type="button" class="btnDelete btn btn-sm btn-danger"><span class="glyphicon glyphicon-remove"></span> Remover</button></td>' +
        "</tr>");

    $(".btnDelete").bind("click", Delete);

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

$("#btnAddIng").click(function () {
    Add();
});

$('#btnCadastrar').click(function () {
   
    $('#FormCreateBebida').submit();
});