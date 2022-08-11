
$(document).ready(function () {

///---------------------------
    $("#FincaId").change(function () {
        $("#LoteId").empty();
        $.getJSON('/Productos/getLotes', { idFinca: $('#FincaId').val() }, function (data) {
            $.each(data, function () {
                $('#LoteId').append('<option value=' +
                    this.Value + '>' + this.Text + '</option>');
            });
        });
    });

    $("#ProveedorId").change(function () {
        $("#CompraId").empty();
        $.getJSON('/Pagos/getCompras', { proveedorId: $('#ProveedorId').val() }, function (data) {
            $.each(data, function () {
                $('#CompraId').append('<option value=' +
                    this.Value + '>' + this.Text + '</option>');
            });
        });
    });
});
