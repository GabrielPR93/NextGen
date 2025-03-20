using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using NextGen.Mantenimiento.Domain;
using Volo.Abp;

namespace NextGen.Mantenimiento.Categoria
{
    public class CategoriaManager: DomainService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaManager(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<Categoria> CreateAsync(int id, string nombre, string descripcion)
        {
            Check.NotNullOrWhiteSpace(nombre, nameof(nombre));
            

            nombre = nombre.ToUpperInvariant();

            var existingCategoria = await _categoriaRepository.FindByNameAsync(nombre);
            if (existingCategoria != null)
            {
                throw new CategoriaAlreadyExistsException(nombre);
            }
            return new Categoria(id, nombre, descripcion);
        }
        public async Task ChangeNameAsync(Categoria categoria, string nombre)
        {
            var existingCategoria = await _categoriaRepository.FindByNameAsync(nombre);
            if (existingCategoria != null && existingCategoria.Id != categoria.Id)
            {
                throw new CategoriaAlreadyExistsException(nombre);
            }
            categoria.ChangeName(nombre);
        }

    }
}
