using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace NextGen.Mantenimiento.Empresa
{
    public class EmpresaAlreadyExistsException : BusinessException
    {
        public EmpresaAlreadyExistsException(string nombre) : base(MantenimientoDomainErrorCodes.EmpresaAlreadyExists)
        {
            WithData("nombre", nombre);

        }
    }
}
