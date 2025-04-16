using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public class TipoAcreditacionesManager : DomainService
    {

        private readonly ITipoAcreditacionesRepository _tipoAcreditacionesRepository;
        public TipoAcreditacionesManager(ITipoAcreditacionesRepository tipoAcreditacionesRepository)
        {
            _tipoAcreditacionesRepository = tipoAcreditacionesRepository;
        }
        public async Task<TipoAcreditaciones> CreateAsync(int id, string nombre, int? nivel, DateTime? duracion, string? descripcion)
        {
            Check.NotNullOrWhiteSpace(nombre, nameof(nombre));
            var existingTipoAcreditacion = await _tipoAcreditacionesRepository.FindByNameAsync(nombre);
            if (existingTipoAcreditacion != null)
            {
                throw new TipoAcreditacionAlreadyExistsException(nombre);
            }
            return new TipoAcreditaciones(id, nombre, nivel, duracion, descripcion);
        }
        public async Task ChangeNameAsync(TipoAcreditaciones tipoAcreditacion, string nombre)
        {
            var existingTipoAcreditacion = await _tipoAcreditacionesRepository.FindByNameAsync(nombre);
            if (existingTipoAcreditacion != null && existingTipoAcreditacion.Id != tipoAcreditacion.Id)
            {
                throw new TipoAcreditacionAlreadyExistsException(nombre);
            }
            tipoAcreditacion.ChangeName(nombre);
        }
    }
}
