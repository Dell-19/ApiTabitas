$(document).ready(function () {
    dataTable = $("#tblEtiqueta").DataTable({
        "ajax": {
            "url": "/Etiquetas/GetTodosEtiqueta",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idEtiquetas", "width": "10%" },
            { "data": "modelo", "width": "15%" },
            {
                "data": "fechaEntregaMaquila", "width": "15%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            {
                "data": "fechaEntregaTerminado", "width": "15%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "incidencias", "width": "20%" },
            { "data": "area", "width": "15%" },
            {
                "data": "idEtiquetas",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntregaTerminado ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaEntregaTerminado ? "Editado" : "Editar";
                    return `<div class="text-center">
                        <a href="/Etiquetas/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
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
    $('#tblEtiqueta_filter').detach().appendTo('.d-flex');
});