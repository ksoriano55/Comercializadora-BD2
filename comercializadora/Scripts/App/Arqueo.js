$(document).ready(function () {
    ///-----------------Carga de denominaciones

    $.getJSON('/ArqueoCajas/getDenominacion', function (data) {
        $.each(data, function () {
            $('#Denominacion').append('<option value=' +
                this.Value + '>' + this.Text + '</option>');
        });
    });
});

var contador = 0;
/////Añadir denominaciones//////////////////////////////////////
$("#AgregarEfectivo").click(function () {
    let denominacion = $('#denominacionDescripcion').val($("#Denominacion option:selected").text());
    let nombreDeno = $('#denominacionDescripcion').val();
    let cantidad = $('#cantidad').val();

    console.log("denominacion", denominacion)
    function GetEfectivoArqueo() {
        var efectivo = {
            Denominacion: denominacion,
            cantidad: cantidad,
            efectivoArqueoId: contador
        }
        return efectivo
    };

    var efectivoAr = GetEfectivoArqueo();

    $.ajax({
        url: "/ArqueoCajas/InsertEfectivoArqueo",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ efectivoArqueo: efectivoAr }),
    })
        .done(function (data) {
            contador = contador + 1;
            copiar = "<tr data-id=" + contador + ">";
            copiar += "<td id = 'denominacion'>" + $('#BodegaId').val() + "</td>";
            copiar += "<td id = 'cantidad'>" + $('#ProductoId').val() + "</td>";
            copiar += "<td id = 'Total'>" + $('#Disponible').val() + "</td>";
            copiar += "</tr>";
            $('#TableEfectivo').append(copiar);
            $('#cantidad').val('0');
        });

})