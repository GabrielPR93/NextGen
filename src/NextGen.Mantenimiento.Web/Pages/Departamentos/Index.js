$(function () {
    var l = abp.localization.getResource('Mantenimiento');
    var createModal = new abp.ModalManager(abp.appPath + 'Departamentos/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Departamentos/EditModal');

    var dataTable = $('#DepartamentosTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: function (data, callback, settings) {
                abp.ajax({
                    url: '/api/app/departamento',
                    type: 'GET'
                }).done(function (result) {
                    callback({ data: result.items }); // Extraer solo `items`
                }).fail(function (xhr, status, error) {
                    console.error("Error en la petición AJAX:", error);
                });
            },

            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('Mantenimiento.Departamento.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Mantenimiento.Departamento.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DepartamentoDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        NextGen.Mantenimiento.Departamento.Departamento
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('ID'),
                    data: "id"
                },
                {
                    title: l('Nombre'),
                    data: "nombre",
                },
                {
                    title: l('Nombre Abreviado'),
                    data: "nombreAbreviado",
                }

            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewDepartamentoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
