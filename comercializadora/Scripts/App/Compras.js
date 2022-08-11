
var contador = 0;
/////Añadir nuevo insumo a la compra//////////////////////////////////////
$("#AgregarCompra").click(function () {

    var insumoId = $('#InsumoId').val();
    var cantidad = $('#Cantidad').val();
    let insumo = $('#insumoName').val($("#InsumoId option:selected").text());
    let NombreInsumo = $('#insumoName').val();

    function GetCompraDetalle() {
        var compraDetalle = {
            insumoId: insumoId,
            Cantidad: cantidad,
            CompraDetalleId: contador
        }
        return compraDetalle
    };

    var inventarioList = GetCompraDetalle();

    $.ajax({
        url: "/Compras/InsertCompraDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ compraDetalle: inventarioList }),
    })
        .done(function (data) {
            contador = contador + 1;
            copiar = "<tr data-id=" + contador + ">";
            copiar += "<td id = 'insumoId'>" + $('#InsumoId').val() + "</td>";
            copiar += "<td id = 'Descripcion'>" + NombreInsumo + "</td>";
            copiar += "<td id = 'cantidad'>" + $('#Cantidad').val() + "</td>";
            copiar += "<td>" + '<button id="QuitarBeneficiario" class="btn btn-danger btn-xs eliminar" type="button" title="Quitar"><i class="fa fa-trash-o fa-lg"></i></button>' + "</td>";
            copiar += "</tr>";
            $('#TableCompras').append(copiar);
        });

})