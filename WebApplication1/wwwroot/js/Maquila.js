$(document).ready(function () {
    dataTable = $("#tblMaquila").DataTable({
        "ajax": {
            "url": "/Maquilas/GetTodosMaquila",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idMaquila", "width": "10%" },
            { "data": "modelo", "width": "20%" },
            { "data": "maquilero1", "width": "10%" },
            { "data": "maquilero2", "width": "10%" },
            { "data": "maquilero3", "width": "10%" },
            { "data": "maquilero4", "width": "10%" },
            { "data": "fechaEntregaMaq1", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            {
                "data": "fechaEntregaMaq2", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            {
                "data": "fechaEntregaMaq3", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            {
                "data": "fechaEntregaMaq4", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "fechaMaquila", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
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
                "data": "idMaquila",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntrega ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaEntrega ? "Editado" : "Editar";
                    return `<div class="text-center">
                        <a href="/Maquilas/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
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
    $('#tblMaquila_filter').detach().appendTo('.d-flex');
});