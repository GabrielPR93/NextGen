using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace NextGen.Mantenimiento.Departamento
{
    public class DepartamentoManager : DomainService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoManager(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<Departamento> CreateAsync(
            int id,
            string nombre,
            string nombreAbreviado)
        {
            Check.NotNullOrWhiteSpace(nombre, nameof(nombre));
            Check.NotNullOrWhiteSpace(nombreAbreviado, nameof(nombreAbreviado));

            nombreAbreviado = nombreAbreviado.ToUpperInvariant();

            var existingDepartamento = await _departamentoRepository.FindByNameAsync(nombre);
            if (existingDepartamento != null)
            {
                throw new DepartamentoAlreadyExistsException(nombre);
            }

            return new Departamento(id, nombre, nombreAbreviado);
        }

        public async Task ChangeNameAsync(
            Departamento departamento,
            string nuevoNombre)
        {
            Check.NotNull(departamento, nameof(departamento));
            Check.NotNullOrWhiteSpace(nuevoNombre, nameof(nuevoNombre));

            var existingDepartamento = await _departamentoRepository.FindByNameAsync(nuevoNombre);
            if (existingDepartamento != null && existingDepartamento.Id != departamento.Id)
            {
                throw new DepartamentoAlreadyExistsException(nuevoNombre);
            }

            departamento.ChangeName(nuevoNombre);
        }
    }
}

