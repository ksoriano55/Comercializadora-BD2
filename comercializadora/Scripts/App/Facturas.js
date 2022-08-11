
var contador = 0;
/////Añadir nuevo insumo a la compra//////////////////////////////////////
$("#AgregarFactura").click(function () {
    let producto = $('#productName').val($("#ProductoId option:selected").text());
    let NombreProducto = $('#productName').val();
    let insumo = $('#insumoName').val($("#InsumoId option:selected").text());
    let NombreInsumo = $('#insumoName').val();
    let productoId = $('#ProductoId').val();
    let insumoId = $('#InsumoId').val();
    var cantidad = $('#Cantidad').val();
    var descripcion = productoId ? NombreProducto : NombreInsumo;
    var codigo = productoId ? productoId : insumoId;

    console.log("productoId", productoId)
    function GetFacturaDetalle() {
        var compraDetalle = {
            productoId: productoId,
            InsumoId: insumoId,
            Cantidad: cantidad,
            FacturaDetalleId: contador
        }
        return compraDetalle
    };

    var detalleList = GetFacturaDetalle();

    $.ajax({
        url: "/Facturas/InsertFacturaDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ facturaDetalle: detalleList }),
    })
        .done(function (data) {
            contador = contador + 1;
            copiar = "<tr data-id=" + contador + ">";
            copiar += "<td id = 'idProducto'>" + codigo + "</td>";
            copiar += "<td id = 'Descripcion'>" + descripcion + "</td>";
            copiar += "<td id = 'cantidad'>" + cantidad + "</td>";
            copiar += "<td>" + '<button id="QuitarProducto" class="btn btn-danger btn-xs eliminar" type="button" title="Quitar"><i class="fa fa-trash-o fa-lg"></i></button>' + "</td>";
            copiar += "</tr>";
            $('#TableFacturas').append(copiar);
            $('#Cantidad').val('0');
        });

})