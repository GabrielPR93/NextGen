$(function () {

    var l = abp.localization.getResource('Mantenimiento');
    var createModal = new abp.ModalManager(abp.appPath + 'Categoria/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Categoria/EditModal');

   var dataTable = $('#CategoriasTable').DataTable(
       abp.libs.datatables.normalizeConfiguration({
        serverSide: true,
        paging: true,
        order: [[1, "asc"]],
        searching: true,
        scrollX: true,
        ajax: function (data, callback, settings) {
            abp.ajax({
                url: '/api/app/categoria',
                type: 'GET',
                data: {
                    maxResultCount: data.length,
                    skipCount: data.start,
                    sorting: data.order.length > 0 ? data.columns[data.order[0].column].data + " " + data.order[0].dir : null,
                    filter: data.search.value !== undefined && data.search.value !== null ? data.search.value : " "
                }
            }).done(function (result) {
                callback({ recordsTotal: result.totalCount, recordsFiltered: result.totalCount, data: result.items });
                console.log('Respuesta simple:', result); // Verifica si recibes los datos
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
                            visible: abp.auth.isGranted('Mantenimiento.Categoria.Edit'),
                            action: function (data) {
                                editModal.open({ id: data.record.id });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('Mantenimiento.Categoria.Delete'),
                            action: function (data) {
                                abp.message.confirm(
                                    l('CategoriaDeletionConfirmationMessage', data.record.nombre),
                                    l('AreYouSure'),
                                    function (confirmed) {
                                        if (confirmed) {
                                            abp.ajax({
                                                url: '/api/app/categoria/' + data.record.id,
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
                title: l('NombreCategoria'),
                data: "nombre"
            },
            {
                title: l('Descripcion'),
                data: "descripcion"
            }
        ]
    })
);


    createModal.onResult(function () {
        abp.notify.success(l('SuccessfullyCreated'));
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        abp.notify.success(l('SuccessfullyEdited'));
        dataTable.ajax.reload();
    });

    $('#NewCategoriaButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

});