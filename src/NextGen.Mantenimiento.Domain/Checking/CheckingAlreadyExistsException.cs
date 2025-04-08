using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace NextGen.Mantenimiento.Checking
{
    public class CheckingAlreadyExistsException : BusinessException
    {
        public CheckingAlreadyExistsException(Guid id) : base(MantenimientoDomainErrorCodes.CheckingAlreadyExists)
        {
            WithData("Ya existe un registro de entrada sin hora de salida.", id);
        }
    }
}
