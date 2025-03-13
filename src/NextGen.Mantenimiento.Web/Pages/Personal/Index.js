$(function () {
    var l = abp.localization.getResource('Mantenimiento');

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
                    type: 'GET',
                    data: {
                        maxResultCount: data.length,
                        skipCount: data.start,
                        sorting: data.order.length > 0 ? data.columns[data.order[0].column].data + " " + data.order[0].dir : null,
                        filter: data.search.value !== undefined && data.search.value !== null ? data.search.value : " "
                    }
                }).done(function (result) {
                    console.log("Datos recibidos de la API:", result);
                    callback({ recordsTotal: result.totalCount, recordsFiltered: result.totalCount, data: result.items });
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
                                visible: abp.auth.isGranted('Mantenimiento.Personal.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Mantenimiento.Personal.Delete'),
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
                { title: l('Cat-Id'), data: "categoriaId" },
                { title: l('Nombre'), data: "nombre" },
                { title: l('Apellidos'), data: "apellidos" },
                { title: l('DNI'), data: "dni" },
                { title: l('Telefono'), data: "telefono" },
                { title: l('Departamento'), data: "nombreDepartamento" },
                { title: l('Direccion'), data: "direccion" },
                { title: l('Email'), data: "correoElectronico" },
                {
                    title: l('FechaNacimiento'),
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
        abp.notify.info(l('SuccessfullyAdded'));
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        abp.notify.info(l('SuccessfullyEdited'));
        dataTable.ajax.reload();
    });

    if (!abp.auth.isGranted('Mantenimiento.Personal.Create')) {
        $('#NewPersonalButton').hide();
    }

    $('#NewPersonalButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
