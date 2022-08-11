

$(document).ready(function () {
    $('#visibleInsumo').hide();
    $('#visibleProducto').show();
});

function yesnoCheck() {
    esInsumo = document.getElementById('esInsumo')
    console.log("esInsumo.checked", esInsumo.checked)
    if (esInsumo.checked) {
        $('#visibleInsumo').show();
        $('#visibleProducto').hide();
    }
    else {
        $('#visibleInsumo').hide();
        $('#visibleProducto').show();
    }
}

var contador = 0;
/////Añadir nuevo Producto al inventario//////////////////////////////////////
$("#AgregarInventario").click(function () {
    let producto = $('#productName').val($("#ProductoId option:selected").text());
    let NombreProducto = $('#productName').val();
    let insumo = $('#insumoName').val($("#InsumoId option:selected").text());
    let NombreInsumo = $('#insumoName').val();
    var bodegaId = $('#BodegaId').val();
    var productoId = $('#ProductoId').val();
    var insumoId = null;
    var cantidad = $('#Disponible').val();
    let descripcion = productoId ? NombreProducto : NombreInsumo;
    esInsumo = document.getElementById('esInsumo')
    if (esInsumo.checked) {
        insumoId = $('#InsumoId').val();
        productoId = null;
    }

    function GetInventario() {
        var inventario = {
            bodegaId: bodegaId,
            productoId: productoId,
            insumoId: insumoId,
            Disponible: cantidad,
            InventarioId: contador
        }
        return inventario
    };

    var CompraDetalleList = GetInventario();

    $.ajax({
        url: "/Inventarios/InsertInventario",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Inventario: CompraDetalleList }),
    })
        .done(function (data) {
                contador = contador + 1;
                copiar = "<tr data-id=" + contador + ">";
                copiar += "<td id = 'codigoId'>" + contador + "</td>";
                copiar += "<td id = 'Descripcion'>" + descripcion + "</td>";
                copiar += "<td id = 'cantidad'>" + $('#Disponible').val() + "</td>";
                copiar += "<td>" + '<button id="QuitarBeneficiario" class="btn btn-danger btn-xs eliminar" type="button" title="Quitar"><i class="fa fa-trash-o fa-lg"></i></button>' + "</td>";
                copiar += "</tr>";
                $('#TableInventario').append(copiar);
                $('#Disponible').val('0');
        });

})