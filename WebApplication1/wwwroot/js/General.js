$(document).ready(function () {
    dataTable = $("#tblGeneral").DataTable({
        "ajax": {
            "url": "/Generales/GetTodosGeneral",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "idGeneral", "width": "10%" },
            { "data": "modelo", "width": "20%" },
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
            { "data": "descripcion", "width": "30%" },
            { "data": "rangoTallas", "width": "30%" },
            { "data": "temporadas", "width": "30%" },
            { "data": "pt", "width": "30%" },
            { "data": "numeroOrden", "width": "30%" },
            { "data": "cantidadRequerida", "width": "30%" },
            { "data": "fechaRecepcion", "width": "30%", "render": function (data) { return moment(data).format('DD/MM/YYYY')}},
            {
                "data": "fechaEntrega", "width": "30%", "render": function (data) {
                    return data ? moment(data).format('DD/MM/YYYY') : "Sin fecha";
                }
            },
            { "data": "procesoActual", "width": "30%" },
            {
                "data": "idGeneral",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Generales/Edit/${data}" class="btn btn-success text-white" style="cursor: pointer;">
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
    $('#tblGeneral_filter').detach().appendTo('.d-flex');

    $(document).on("click", '[data-bs-toggle="modal"]', function () {
        const imageUrl = $(this).data("bs-image");
        $("#modalImage").attr("src", imageUrl);
    });
});