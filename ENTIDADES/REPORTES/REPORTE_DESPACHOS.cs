using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES.REPORTES
{
     public class REPORTE_DESPACHOS
    {
        [Display(Name = "Fecha")]
        public DateTime? FECHA_DESPACHO { get; set; }

        [Display(Name = "Bomba despacho")]
        public int ID_BOMBA { get; set; }
    }

    public class REPORTE_DESPACHOS_SOURCE
    {
        
    }
    
}
