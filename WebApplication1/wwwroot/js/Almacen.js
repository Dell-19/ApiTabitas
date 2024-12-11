$(document).ready(function () {
    var dataTable = $('#tblAlmacen').DataTable({
        "ajax": {
            "url": "/Almacenes/GetTodosAlmacen",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idAlmacen", "width": "10%" },
            { "data": "modelo", "width": "15%" },
            { "data": "fechaRecepcion", "width": "15%", "render": function (data) { return moment(data).format('DD/MM/YYYY'); } },
            {
                "data": "fechaLiberacion", "width": "15%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "auxiliar", "width": "20%" },
            { "data": "fechaEntregaAvios", "width": "15%", "render": function (data) { return moment(data).format('DD/MM/YYYY'); } },
            { "data": "fechaDevolicionTelas", "width": "15%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "fechaLibeCorte", "width": "15%", "render": function (data) { return moment(data).format('DD/MM/YYYY'); } },
            { "data": "fechaCarpeta", "width": "15%", "render": function (data) { return moment(data).format('DD/MM/YYYY'); } },
            { "data": "incidencias", "width": "20%" },
            { "data": "area", "width": "15%" },
            {
                "data": "idAlmacen",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Almacenes/Edit/${data}" class="btn btn-success text-white" style="cursor: pointer;">
                            <i class="fas fa-edit"></i> Editar
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
    $('#tblAlmacen_filter').detach().appendTo('.d-flex');
});
