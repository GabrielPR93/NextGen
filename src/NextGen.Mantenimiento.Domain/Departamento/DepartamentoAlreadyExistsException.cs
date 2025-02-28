using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Departamento
{
    public class DepartamentoAlreadyExistsException: BusinessException
    {

        public DepartamentoAlreadyExistsException(string nombre): base(MantenimientoDomainErrorCodes.DepartamentoAlreadyExists)
        {
            WithData("nombre",nombre);
            
        }
    }
}
