$(function () {
    var l = abp.localization.getResource('Personal');

    // Crear instancias de modales
    var createModal = new abp.ModalManager('/Personal/CreateModal');
    var editModal = new abp.ModalManager('/Personal/EditModal');

    var dataTable = $('#Personal').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: function (data, callback, settings) {
                abp.ajax({
                    url: '/api/app/personal', // Ruta corregida
                    type: 'GET'
                }).done(function (result) {
                    console.log("Datos recibidos de la API:", result);
                    callback({ data: result.items }); // Extraer solo `items`
                }).fail(function (xhr, status, error) {
                    console.error("Error en la petición AJAX:", error);
                });
            },
            columnDefs: [
                {

                    title: l('Actions'),
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                action: function (data) {
                                    abp.message.confirm(
                                        l('PersonalDeletionConfirmationMessage', data.record.nombre)
                                    ).then(function (isConfirmed) {
                                        if (!isConfirmed) return; // Si el usuario cancela, no hacemos nada

                                        // Evita múltiples llamadas eliminando eventos previos
                                        $(this).off('click');

                                        abp.ajax({
                                            url: `/api/app/personal/${data.record.id}`,
                                            type: 'DELETE'
                                        }).done(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        }).fail(function (xhr, status, error) {
                                            console.error("Error al eliminar:", error);
                                        });
                                    });
                                }
                            }

                        ]
                    }
                },
                { title: l('Id'), data: "id" },
                { title: l('Dep-Id'), data: "departamentoId" },
                { title: l('Cat-Id'), data: "categoriaId" },
                { title: l('Nombre'), data: "nombre" },
                { title: l('Apellidos'), data: "apellidos" },
                { title: l('DNI'), data: "dni" },
                { title: l('Teléfono'), data: "telefono" },
                { title: l('Dirección'), data: "direccion" },
                { title: l('Correo Electrónico'), data: "correoElectronico" },
                {
                    title: l('Fecha de Nacimiento'),
                    data: "fechaNacimiento",
                    render: function (data) {
                        return data ? luxon.DateTime.fromISO(data).toLocaleString(luxon.DateTime.DATE_SHORT) : "-";
                    }
                },
                {
                    title: l('Fecha Alta'),
                    data: "fechaAlta",
                    render: function (data) {
                        return data ? luxon.DateTime.fromISO(data).toLocaleString(luxon.DateTime.DATE_SHORT) : "-";
                    }
                },
                {
                    title: l('Fecha Baja'),
                    data: "fechaBaja",
                    render: function (data) {
                        return data ? luxon.DateTime.fromISO(data).toLocaleString(luxon.DateTime.DATE_SHORT) : "-";
                    }
                }
            ]
        })
    );

    // Recargar la tabla después de crear o editar
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewPersonalButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
