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
                                    visible: abp.auth.isGranted('Mantenimiento.Departamento.Delete'),
           
                                    action: function (data) {
                                        abp.message.confirm(
                                            l('DepartamentoDeletionConfirmationMessage', data.record.nombre),
                                            l('AreYouSure'),
                                            function (confirmed) {
                                                if (confirmed) {
                                                    abp.ajax({
                                                        url: '/api/app/departamento/' + data.record.id,
                                                        type: 'DELETE'
                                                    }).done(function () {
                                                        abp.notify.info(l('SuccessfullyDeleted'));
                                                        dataTable.ajax.reload();
                                                    }).fail(function (xhr, status, error) {
                                                        console.error("Error al eliminar:", error);
                                                        abp.notify.error(l('DeletionFailed'));
                                                    });
                                                }
                                            }
                                        );
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
