$(function () {
    var l = abp.localization.getResource('Personal');

    var dataTable = $('#Personal').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true, // Permitir búsqueda
            scrollX: true,
            ajax: function (data, callback, settings) {
                abp.ajax({
                    url: 'https://localhost:44343/api/app/personal', // Ajusta la ruta si es necesario
                    type: 'GET'
                }).done(function (result) {
                    console.log("Datos recibidos de la API:", result); // Verifica la estructura en la consola
                    console.log("Datos enviados a DataTables:", result.items);
                    callback({ data: result.items }); // Extraer solo `items`
                }).fail(function (xhr, status, error) {
                    console.error("Error en la petición AJAX:", error);
                });
            },
            columnDefs: [
                {
                    title: l('Id'),
                    data: "id"
                },
                {
                    title: l('Dep-Id'),
                    data: "departamentoId"
                },
                {
                    title: l('Cat-Id'),
                    data: "categoriaId"
                },
                {
                    title: l('Nombre'),
                    data: "nombre"
                },
                {
                    title: l('Apellidos'),
                    data: "apellidos"
                },
                {
                    title: l('DNI'),
                    data: "dni"
                },
                {
                    title: l('Teléfono'),
                    data: "telefono"
                },
                {
                    title: l('Dirección'),
                    data: "direccion"
                },
                {
                    title: l('Correo Electrónico'),
                    data: "correoElectronico"
                },
                {
                    title: l('Fecha de Nacimiento'),
                    data: "fechaNacimiento",
                    render: function (data) {
                        if (!data) return "-"; // Si la fecha es null, muestra "-"
                        return luxon.DateTime
                            .fromISO(data)
                            .toLocaleString(luxon.DateTime.DATE_SHORT);
                    }
                },
                {
                    title: l('Fecha Alta'),
                    data: "fechaAlta",
                    render: function (data) {
                        if (!data) return "-"; // Si la fecha es null, muestra "-"
                        return luxon.DateTime
                            .fromISO(data)
                            .toLocaleString(luxon.DateTime.DATE_SHORT);
                    }
                },
                {
                    title: l('Fecha Baja'),
                    data: "fechaBaja",
                    render: function (data) {
                        if (!data) return "-"; // Si la fecha es null, muestra "-"
                        return luxon.DateTime
                            .fromISO(data)
                            .toLocaleString(luxon.DateTime.DATE_SHORT);
                    }
                },
                {   // ✅ Mueve este bloque dentro del array `columnDefs`
                    title: l('Acciones'),
                    orderable: false,
                    render: function (data, type, row) {
                        return `
                            <button type="button" class="btn btn-primary btn-sm edit-personal" data-id="${row.id}">
                                <i class="fa fa-edit"></i> ${l('Edit')}
                            </button>
                            <button type="button" class="btn btn-danger btn-sm delete-personal" data-id="${row.id}">
                                <i class="fa fa-trash"></i> ${l('Delete')}
                            </button>
                        `;
                    }
                }
            ]
        })
    );

    // Evento para editar un registro
    $(document).on('click', '.edit-personal', function () {
        var id = $(this).data('id');
        abp.modals.editPersonal.open({ id: id });
    });

    // Evento para eliminar un registro
    $(document).on('click', '.delete-personal', function () {
        var id = $(this).data('id');
        abp.message.confirm(
            l('AreYouSureToDelete'),
            function (confirmed) {
                if (confirmed) {
                    abp.ajax({
                        url: `https://localhost:44343/api/app/personal/${id}`,
                        type: 'DELETE'
                    }).done(function () {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        dataTable.ajax.reload(); // Recargar la tabla después de eliminar
                    }).fail(function (xhr, status, error) {
                        console.error("Error al eliminar:", error);
                    });
                }
            }
        );
    });
});

