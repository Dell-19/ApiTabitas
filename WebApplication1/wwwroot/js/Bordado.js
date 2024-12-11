$(document).ready(function () {
    dataTable = $("#tblBordado").DataTable({
        "ajax": {
            "url": "/Bordados/GetTodosBordado",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idBordado", "width": "10%" },
            { "data": "modelo", "width": "20%" },
            { "data": "encargado", "width": "10%" },
            { "data": "fechaCorte", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            { "data": "fechaRecepcion", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            {
                "data": "fechaEntrega", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "cantidadRecibida", "width": "30%" },
            { "data": "incidencias", "width": "30%"},
            { "data": "area", "width": "30%" },
            {
                "data": "idBordado",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntrega ? "btn-secondary" : "btn-success"; // Cambia color
                    let btnText = row.fechaEntrega ? "Editado" : "Editar"; // Cambia texto
                    return `<div class="text-center">
                        <a href="/Bordados/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
                            <i class="fas fa-edit"></i> ${btnText}
                        </a>
                    </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "search": "Buscar:",
        },
        "dom": '<"top"f>rt<"bottom"ip><"clear">',
        "searching": true,
        "scrollX": true,
        "paging": false,
        "info": false,
    });
    $('#tblBordado_filter').detach().appendTo('.d-flex');
});