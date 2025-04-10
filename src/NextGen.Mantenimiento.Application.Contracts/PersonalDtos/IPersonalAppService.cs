﻿using NextGen.Mantenimiento.Categoria;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.PersonalDtos
{
    public interface IPersonalAppService : ICrudAppService<PersonalDto,int, FilteredPagedAndSortedResultRequestDto, CreateUpdatePersonalDto>
    {

        Task<ListResultDto<DepartamentoLookupDto>> GetDepartamentoLookupAsync();
        Task<ListResultDto<CategoriaLookupDto>> GetCategoriaLookupAsync();



    }
}
