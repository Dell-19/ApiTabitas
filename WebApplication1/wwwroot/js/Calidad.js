$(document).ready(function () {
    dataTable = $("#tblCalidad").DataTable({
        "ajax": {
            "url": "/Calidades/GetTodosCalidad",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idCalidad", "width": "10%" },
            { "data": "modelo", "width": "20%" },
            { "data": "fechaDeRecepcion", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            { "data": "fechaRevision", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            { "data": "estado", "width": "30%" },
            {
                "data": "fechaEnvioMaquila", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            {
                "data": "fechaRecepcionRechazo", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "incidencia", "width": "30%" },
            { "data": "area", "width": "30%" },
            {
                "data": "idCalidad",
                "render": function (data, type, row) {
                    let btnClass = row.fechaRecepcionRechazo ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaRecepcionRechazo ? "Editado" : "Editar";
                    console.log("Fecha de Recepcion Rechazo: ", row.fechaRecepcionRechazo);  // Verifica el valor
                    return `<div class="text-center">
                        <a href="/Calidades/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
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
    $('#tblCalidad_filter').detach().appendTo('.d-flex');
});
