using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ENTIDADES
{
    [Table("VEHICULO")]
    public class VEHICULO
    {
        [Key]
        [MaxLength(10)]
        public string PLACA { get; set; }
        public int ANIO { get; set; }
        public string MODELO { get; set; }
        public string ESTADO { get; set; }

        public virtual ICollection<DESPACHO> DESPACHO { get; set; }
    }
}
