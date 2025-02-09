﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Personal
{
    public class PersonalDto : AuditedEntityDto<int>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Direccion { get; set; }

        public string CorreoElectronico { get; set; }

        public DateTime FechaAlta { get; set; }

    }
}
