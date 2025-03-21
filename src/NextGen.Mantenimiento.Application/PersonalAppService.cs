using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Permissions;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.Categoria;


namespace NextGen.Mantenimiento
{
    public class PersonalAppService : CrudAppService<Entities.Personal, PersonalDto, int, FilteredPagedAndSortedResultRequestDto, CreateUpdatePersonalDto>, IPersonalAppService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        private readonly ICategoriaRepository _categoriaRepository;
        public PersonalAppService(IRepository<Entities.Personal, int> repository, IDepartamentoRepository departamentoRepository, ICategoriaRepository categoriaRepository) : base(repository)
        {
            _departamentoRepository = departamentoRepository;
            _categoriaRepository = categoriaRepository;
            GetPolicyName = MantenimientoPermissions.Personal.Default;
            GetListPolicyName = MantenimientoPermissions.Personal.Default;
            CreatePolicyName = MantenimientoPermissions.Personal.Create;
            UpdatePolicyName = MantenimientoPermissions.Personal.Edit;
            DeletePolicyName = MantenimientoPermissions.Personal.Delete;
        }

        public override async Task<PersonalDto> GetAsync(int id)
        {
            
            var queryable = await Repository.GetQueryableAsync();

            var query = from personal in queryable
                        join departamento in await _departamentoRepository.GetQueryableAsync() on personal.DepartamentoId equals departamento.Id into dep
                        from departamento in dep.DefaultIfEmpty()
                        join categoria in await _categoriaRepository.GetQueryableAsync() on personal.CategoriaId equals categoria.Id into cat
                        from categoria in cat.DefaultIfEmpty()
                        where personal.Id == id
                        select new { personal, departamento, categoria };

           
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Entities.Personal), id);
            }

            var personalDto = ObjectMapper.Map<Entities.Personal, PersonalDto>(queryResult.personal);
            personalDto.NombreDepartamento = queryResult.departamento?.Nombre ?? "Sin Departamento";
            personalDto.NombreCategoria = queryResult.categoria?.Nombre ?? "Sin Categoria";
            return personalDto;
        }

        public override async Task<PagedResultDto<PersonalDto>> GetListAsync(FilteredPagedAndSortedResultRequestDto input)
        {
            var departamentoQueryable = await _departamentoRepository.GetQueryableAsync();
            var categoriaQueryable = await _categoriaRepository.GetQueryableAsync();
            var queryable = await Repository.GetQueryableAsync();

            var query = from personal in queryable
                        join departamento in departamentoQueryable
                            on personal.DepartamentoId equals departamento.Id into dep
                        from departamento in dep.DefaultIfEmpty() // Evita errores si no hay departamento

                        join categoria in categoriaQueryable
                            on personal.CategoriaId equals categoria.Id into cat
                        from categoria in cat.DefaultIfEmpty() // Evita errores si no hay categoría

                        select new { personal, departamento, categoria };

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                var filter = input.Filter.ToLower();
                query = query.Where(x =>
                    x.personal.Nombre.ToLower().Contains(filter) ||
                    x.personal.Apellidos.ToLower().Contains(filter) ||
                    x.personal.Dni.ToLower().Contains(filter) ||
                    x.departamento.Nombre.ToLower().Contains(filter) ||
                    x.categoria.Nombre.ToLower().Contains(filter) // Permitir búsqueda por categoría
                );
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var personalDtos = queryResult.Select(x =>
            {
                var personalDto = ObjectMapper.Map<Entities.Personal, PersonalDto>(x.personal);
                personalDto.NombreDepartamento = x.departamento?.Nombre ?? "Sin Departamento";
                personalDto.NombreCategoria = x.categoria?.Nombre ?? "Sin Categoría";

                return personalDto;
            }).ToList();

            return new PagedResultDto<PersonalDto>(totalCount, personalDtos);
        }



        public async Task<ListResultDto<DepartamentoLookupDto>> GetDepartamentoLookupAsync()
        {
            var departamentos = await _departamentoRepository.GetListAsync();

            return new ListResultDto<DepartamentoLookupDto>(
                ObjectMapper.Map<List<Departamento.Departamento>, List<DepartamentoLookupDto>>(departamentos)
            );
        }

        public async Task<ListResultDto<CategoriaLookupDto>> GetCategoriaLookupAsync()
        {
            var categorias = await _categoriaRepository.GetListAsync();

            return new ListResultDto<CategoriaLookupDto>(
                ObjectMapper.Map<List<Categoria.Categoria>, List<CategoriaLookupDto>>(categorias)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"personal.{nameof(Personal.PersonalDto.Nombre)}";
            }

            if (sorting.Contains("NombreDepartamento", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "NombreDepartamento",
                    "departamento.Nombre",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"personal.{sorting}";
        }

    
    }
}

