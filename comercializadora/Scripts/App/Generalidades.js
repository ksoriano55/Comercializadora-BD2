
$(document).ready(function () {

    $("#FincaId").change(function () {
        let finca = $('#FincaId').val()
        console.log("cambie algo", finca)
        $("#LoteId").empty();

        $.getJSON('/Productos/getLotes', { idFinca: $('#FincaId').val() }, function (data) {
            $.each(data, function () {
                $('#LoteId').append('<option value=' +
                    this.Value + '>' + this.Text + '</option>');
            });
        });
    });
});