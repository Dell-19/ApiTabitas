var dataTable;
$(document).ready(function () {
    loadDataTable();
    $('#tblTerminado_filter').hide(); // Oculta la barra de búsqueda manualmente
});

function loadDataTable() {
    dataTable = $("#tblTerminado").DataTable({
        "ajax": {
            "url": "/Terminados/GetTodosTerminado",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idTErminado", "width": "10%" },
            { "data": "modelo", "width": "20%" },
            { "data": "encargado", "width": "10%" },
            { "data": "cantidadEntregada"},
            { "data": "saldo", "width": "30%"},
            { "data": "motivoFaltante" },
            {
                "data": "fechaEntrega", "width": "30%", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "area", "width": "30%" },
            {
                "data": "idTErminado",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntrega ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaEntrega ? "Editado" : "Editar";
                    return `<div class="text-center">
                        <a href="/Terminados/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
                            <i class="fas fa-edit"></i> ${btnText}
                        </a>
                    </div>`;
                },
                "width": "20%"
            }
        ],
        "searching": false,  // Desactiva la barra de búsqueda
        "paging": false,
        "info": false
    });
}