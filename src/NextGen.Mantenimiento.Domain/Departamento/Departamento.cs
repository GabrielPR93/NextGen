using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Emailing;

namespace NextGen.Mantenimiento.Departamento
{
    public class Departamento : FullAuditedAggregateRoot<int>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }

        public Departamento()
        {
            
        }

        internal Departamento(int id, string nombre, string nombreAbreviado): base(id) 
        {
            Id = id;
            SetName(nombre);
            NombreAbreviado = nombreAbreviado;
        }

        internal Departamento ChangeName(string nombre)
        {
        
            SetName(nombre);
            return this;
        }

        private void SetName(string nombre)
        {
            Nombre = Check.NotNullOrWhiteSpace(nombre, nameof(nombre), maxLength: DepartamentoConsts.MaxNameLength);
        }
    }
}
