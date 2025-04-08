$(function () {
    validarBotonesChecking();
    var l = abp.localization.getResource('Mantenimiento');

    var dataTable = $('#CheckingTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: function (data, callback, settings) {
                abp.ajax({
                    url: '/api/app/checking',
                    type: 'GET',
                    data: {
                        maxResultCount: data.length,
                        skipCount: data.start,
                        sorting: data.order.length > 0 ? data.columns[data.order[0].column].data + " " + data.order[0].dir : null,
                        filter: data.search.value !== undefined && data.search.value !== null ? data.search.value : " "
                    }
                }).done(function (result) {
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
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Mantenimiento.Checking.Delete'),
                                action: function (data) {
                                    abp.message.confirm(
                                        l('CheckingDeletionConfirmationMessage', data.record.nombre),
                                        l('AreYouSure'),
                                        function (confirmed) {
                                            if (confirmed) {
                                                abp.ajax({
                                                    url: '/api/app/checking/' + data.record.id,
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
                    title: l('Nombre Usuario'),
                    data: "nombreUsuario"
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
                    title: l('Fecha de Entrada'),
                    data: "horaEntrada",
                    render: function (data, type, row) {
                        return new Date(data).toLocaleDateString([], { day: '2-digit', month: '2-digit', year: 'numeric' }).split('T')[0];
                    }
                },
                {
                    title: l('Hora de Entrada'),
                    data: "horaEntrada",
                    render: function (data, type, row) {
                        var fecha = new Date(data);
                        return fecha.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' });
                    }
                },
                {
                    title: l('Hora de Salida'),
                    data: "horaSalida",
                    render: function (data, type, row) {
                        try {
                            if (data) {
                                var fecha = new Date(data);
                                return fecha.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' });
                            }
                        } catch (e) {
                            console.warn('Error al formatear horaSalida:', data);
                        }
                        return '-';
                    }
                },
                {
                    title: l('Hora de Creacion'),
                    data: "horaCreacion"
                },

            ]
        })
    );

    $('#NewCheckingButton').click(function (e) {
        e.preventDefault();

        $('#NewCheckingButton').prop('disabled', true);
        $('#ExitCheckingButton').prop('disabled', false);

        var now = new Date();

        abp.ajax({
            url: '/api/app/checking',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                horaEntrada: now.toISOString(),
                horaSalida: null,
                horaCreacion: now.toISOString(),
                nombreUsuario: abp.currentUser.userName,
                nombre: abp.currentUser.name,
                apellidos: abp.currentUser.surName || ''
            })
        }).done(function () {
            abp.notify.success(l('EntrySuccessfullyRecorded'));
            dataTable.ajax.reload();
        }).fail(function (xhr, status, error) {
            if (xhr.responseJSON && xhr.responseJSON.error) {
                abp.notify.error(xhr.responseJSON.error.message || 'Error al crear el checking');
            } else {
                abp.notify.error('Error desconocido: ' + status + ' ' + error);
            }
        });

    });

    // Botón para salir

    $('#ExitCheckingButton').click(function (e) {
        e.preventDefault();

        var now = new Date();

        // Primero, obtener el último registro del usuario actual
        abp.ajax({
            url: '/api/app/checking',
            type: 'GET',
            data: {
                filter: abp.currentUser.userName
            }
        }).done(function (result) {
            var ultimoRegistro = result.items.find(x => x.horaSalida == null && x.nombreUsuario === abp.currentUser.userName);

            if (!ultimoRegistro) {
                abp.notify.warn("No hay entrada activa para registrar la salida.");
                return;
            }

            // Actualizar el registro con la hora de salida
            abp.ajax({
                url: '/api/app/checking/' + ultimoRegistro.id,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify({
                    ...ultimoRegistro,
                    horaSalida: now.toISOString()
                })
            }).done(function () {
                abp.notify.success(l('OutSuccessfullyRecorded'));
                dataTable.ajax.reload();
                validarBotonesChecking();
            }).fail(function () {
                abp.notify.error("Error al registrar la salida.");
            });

        }).fail(function () {
            abp.notify.error("Error al recuperar los registros.");
        });
    });

    function validarBotonesChecking() {
        abp.ajax({
            url: '/api/app/checking/last-open-checking',
            type: 'GET'
        }).done(function (result) {
            if (result && result.horaSalida === null) {
                $('#NewCheckingButton').prop('disabled', true);
                $('#ExitCheckingButton').prop('disabled', false).data('checkingId', result.id);
            } else {
                $('#NewCheckingButton').prop('disabled', false);
                $('#ExitCheckingButton').prop('disabled', true).removeData('checkingId');
            }
        }).fail(function () {
            abp.notify.error("No se pudo validar si hay un checking abierto.");
        });
    }




});