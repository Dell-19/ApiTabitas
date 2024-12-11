$(document).ready(function () {
    dataTable = $("#tblCorteLaser").DataTable({
        "ajax": {
            "url": "/CorteLaseres/GetTodosCorteLaser",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idCorteLaser", "width": "10%" },
            { "data": "modelo", "width": "15%" },
            { "data": "cantidadCortadas", "width": "15%" },
            { "data": "encargado", "width": "15%" },
            {
                "data": "fechaRecepcion",
                "width": "15%",
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": "fechaEntrega",
                "width": "15%",
                "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "recibe", "width": "20%" },
            { "data": "incidencias", "width": "30%" },
            { "data": "area", "width": "15%" },
            {
                "data": "idCorteLaser",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntrega ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaEntrega ? "Editado" : "Editar"; 
                    return `<div class="text-center">
                        <a href="/CorteLaseres/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
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

    $('#tblCorteLaser_filter').detach().appendTo('.d-flex');
});
