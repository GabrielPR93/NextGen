using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace NextGen.Mantenimiento.Categoria
{
    public class CategoriaAlreadyExistsException : BusinessException
    {
        public CategoriaAlreadyExistsException(string nombre): base(MantenimientoDomainErrorCodes.CategoriaAlreadyExists)
        {
            WithData("nombre", nombre);

        }
    }
}
