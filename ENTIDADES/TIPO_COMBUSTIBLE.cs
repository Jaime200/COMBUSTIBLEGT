using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ENTIDADES
{
    [Table("TIPO_COMBUSTIBLE")]
    public class TIPO_COMBUSTIBLE
    {
        [Key]
        public int ID_TIPO_COMBUSTIBLE { get; set; }

        [Required]
        [MaxLength(30)]
        public string DESCRIPCION { get; set; }
        public string ESTADO { get; set; }

        public virtual ICollection<DESPACHO> DESPACHO { get; set; }
    }
}
