$(document).ready(function () {
    dataTable = $("#tblCorte").DataTable({
        "ajax": {
            "url": "/Cortes/GetTodosCorte",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idCorte", "width": "10%" },
            { "data": "modelo", "width": "20%" },
            { "data": "encargado", "width": "10%" },
            { "data": "entregaCorte", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY') } },
            { "data": "cantidadCortadas", "width": "30%" },
            {
                "data": "rutaImagen",
                "render": function (data) {
                    if (data) {
                        return `
                            <a href="#" data-bs-toggle="modal" data-bs-target="#imageModal" data-bs-image="${data}">
                                <img src="${data}" alt="Imagen" style="width: 50px; height: 50px; object-fit: cover;">
                            </a>`;
                    } else {
                        return "Sin imagen";
                    }
                }
            },
            { "data": "piezasSolicitadas", "width": "30%" },
            { "data": "fechaAVentas", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY')} },
            {
                "data": "fechaEntrega", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "incidencias", "width": "30%"},
            { "data": "area", "width": "30%" },
            {
                "data": "idCorte",
                "render": function (data, type, row) {
                    // Comprobar si el registro tiene fecha de entrega
                    let btnClass = row.fechaEntrega ? "btn-secondary" : "btn-success";
                    let btnText = row.fechaEntrega ? "Editado" : "Editar";
                    return `<div class="text-center">
                        <a href="/Cortes/Edit/${data}" class="btn ${btnClass} text-white" style="cursor: pointer;">
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
    $('#tblCorte_filter').detach().appendTo('.d-flex');

    $(document).on("click", '[data-bs-toggle="modal"]', function () {
        const imageUrl = $(this).data("bs-image");
        $("#modalImage").attr("src", imageUrl);
    });
});