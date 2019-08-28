//Script para a tela de create de bebidas
function Add() {
    var counter = $('#hiddenCounter').val();

    counter++;
    $('#hiddenCounter').val(counter);
    $("#tbIngredientes tbody").append(
        "<tr>" +
        '<td><input type="text" class="form-control" name="LstIngredientes" /></td>' +
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
    /*var rows = document.getElementById('tbIngredientes').getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;
    for (var i = 0; i < rows; i++) {
        alert("entrou no for> " + i);
        document.getElementById('tbIngredientes').getElementsByTagName("tbody")[0].getElementsByTagName("tr")[i].getElementsByTagName("td")[0].getElementsByTagName("input").setAttribute("name", "Ingredientes[" + i + "].Descricao");
        document.getElementById('tbIngredientes').getElementsByTagName("tbody")[0].getElementsByTagName("tr")[i].getElementsByTagName("td")[1].getElementsByTagName("input").setAttribute("name", "Ingredientes[" + i + "].Qtde");
        alert("B");
        //var descricao = tr.; //tr.children("td:nth-child(1)").children("input[type=text]");
    }
    alert("A");*/
    $('#FormCreateBebida').submit();
});