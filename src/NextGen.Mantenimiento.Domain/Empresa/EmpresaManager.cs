using NextGen.Mantenimiento.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;

namespace NextGen.Mantenimiento.Empresa
{
    public class EmpresaManager : DomainService
    {
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaManager(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public async Task<Empresa> CreateAsync(int id, string nombre, string nombreAbreviado, string direccion, string correo, byte[] logo)
        {
            Check.NotNullOrWhiteSpace(nombre, nameof(nombre));
            Check.NotNullOrWhiteSpace(nombreAbreviado, nameof(nombreAbreviado));
            Check.NotNullOrWhiteSpace(correo, nameof(correo));

            var existingEmpresa = await _empresaRepository.FindByNameAsync(nombre);
            if (existingEmpresa != null)
            {
                throw new EmpresaAlreadyExistsException(nombre);
            }
            return new Empresa(id,nombre, nombreAbreviado, direccion, correo, logo);
        }
        public async Task ChangeNameAsync(Empresa empresa, string nombre)
        {
            var existingEmpresa = await _empresaRepository.FindByNameAsync(nombre);
            if (existingEmpresa != null && existingEmpresa.Id != empresa.Id)
            {
                throw new EmpresaAlreadyExistsException(nombre);
            }
            empresa.ChangeName(nombre);
        }

    }
}
