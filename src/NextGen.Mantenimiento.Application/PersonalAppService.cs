using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Permissions;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;


namespace NextGen.Mantenimiento
{
    public class PersonalAppService : CrudAppService<Entities.Personal, PersonalDto, int, PagedAndSortedResultRequestDto, CreateUpdatePersonalDto>, IPersonalAppService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        public PersonalAppService(IRepository<Entities.Personal, int> repository, IDepartamentoRepository departamentoRepository) : base(repository)
        {
            _departamentoRepository = departamentoRepository;
            GetPolicyName = MantenimientoPermissions.Personal.Default;
            GetListPolicyName = MantenimientoPermissions.Personal.Default;
            CreatePolicyName = MantenimientoPermissions.Personal.Create;
            UpdatePolicyName = MantenimientoPermissions.Personal.Edit;
            DeletePolicyName = MantenimientoPermissions.Personal.Delete;
        }


        public override async Task<PersonalDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from personal in queryable
                        join departamento in await _departamentoRepository.GetQueryableAsync() on personal.DepartamentoId equals departamento.Id
                        where personal.Id == id
                        select new { personal, departamento };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Entities.Personal), id);
            }

            var personalDto = ObjectMapper.Map<Entities.Personal, PersonalDto>(queryResult.personal);
            personalDto.NombreDepartamento = queryResult.departamento.Nombre;
            return personalDto;
        }
        //public override async Task<PagedResultDto<PersonalDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        //{
        //    //Get the IQueryable<Personal> from the repository
        //    var queryable = await Repository.GetQueryableAsync();

        //    //Prepare a query to join books and authors
        //    var query = from personal in queryable
        //                join departamento in await _departamentoRepository.GetQueryableAsync() on personal.DepartamentoId equals departamento.Id
        //                select new { personal, departamento };

        //    //Paging
        //    query = query
        //        .OrderBy(NormalizeSorting(input.Sorting))
        //        .Skip(input.SkipCount)
        //        .Take(input.MaxResultCount);

        //    //Execute the query and get a list
        //    var queryResult = await AsyncExecuter.ToListAsync(query);

        //    //Convert the query result to a list of PersonalDto objects
        //    var personalDtos = queryResult.Select(x =>
        //    {
        //        var personalDto = ObjectMapper.Map<Entities.Personal, PersonalDto>(x.personal);
        //        personalDto.NombreDepartamento = x.departamento.Nombre;
        //        return personalDto;
        //    }).ToList();

        //    //Get the total count with another query
        //    var totalCount = await Repository.GetCountAsync();

        //    return new PagedResultDto<PersonalDto>(
        //        totalCount,
        //        personalDtos
        //    );
        //}

        public override async Task<PagedResultDto<PersonalDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var departamentoQueryable = await _departamentoRepository.GetQueryableAsync();
            var queryable = await Repository.GetQueryableAsync();

            var query = from personal in queryable
                        join departamento in departamentoQueryable
                        on personal.DepartamentoId equals departamento.Id into dep
                        from departamento in dep.DefaultIfEmpty() // Evita errores si no hay departamento
                        select new { personal, departamento };

            // Aplicar paginación y ordenamiento
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            // Convertir a DTOs
            var personalDtos = queryResult.Select(x =>
            {
                var personalDto = ObjectMapper.Map<Entities.Personal, PersonalDto>(x.personal);
                personalDto.NombreDepartamento = x.departamento != null ? x.departamento.Nombre : "Sin Departamento"; // Evita null
                return personalDto;
            }).ToList();

            var totalCount = await Repository.GetCountAsync();
            return new PagedResultDto<PersonalDto>(totalCount, personalDtos);
        }


        public async Task<ListResultDto<DepartamentoLookupDto>> GetDepartamentoLookupAsync()
        {
            var departamentos = await _departamentoRepository.GetListAsync();

            return new ListResultDto<DepartamentoLookupDto>(
                ObjectMapper.Map<List<Departamento.Departamento>, List<DepartamentoLookupDto>>(departamentos)
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
                    "Departamento.Nombre",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"personal.{sorting}";
        }
    }
}

