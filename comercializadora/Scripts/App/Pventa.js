$(document).ready(function () {
    $('#visibleInsumo').hide();
    $('#InsumoId').val(null);
    $('#visibleProducto').show();
});

function yesnoCheck() {
    esInsumo = document.getElementById('esInsumo')
    if (esInsumo.checked) {
        $('#visibleInsumo').show();
        $('#visibleProducto').hide();
        $('#ProductoID').val(null);
    }
    else {
        $('#visibleInsumo').hide();
        $('#visibleProducto').show();
        $('#InsumoId').val(null);
    }
}