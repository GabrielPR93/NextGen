using Volo.Abp;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public class TipoAcreditacionAlreadyExistsException : BusinessException
    {
        public TipoAcreditacionAlreadyExistsException(string nombre) : base(MantenimientoDomainErrorCodes.TipoAcreditacionAlreadyExists)
        {
            WithData("nombre", nombre);

        }
    }
}
