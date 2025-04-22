
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas;

public class AcreditacionesAsignadas : AuditedEntity<int>
{
    public int Id { get; set; }

    public int PersonalId { get; set; }

    public int? EmpresaId { get; set; }

    public int AcreditacionId { get; set; }

    public DateTime FechaEmision { get; set; }

    public DateTime? FechaCaducidad { get; set; }

    public AcreditacionesAsignadas()
    {
        
    }

    public AcreditacionesAsignadas(int personalId, int? empresaId, int acreditacionId, DateTime fechaEmision, DateTime? fechaCaducidad)
    {
        PersonalId = personalId;
        EmpresaId = empresaId;
        AcreditacionId = acreditacionId;
        FechaEmision = fechaEmision;
        FechaCaducidad = fechaCaducidad;
    }

}