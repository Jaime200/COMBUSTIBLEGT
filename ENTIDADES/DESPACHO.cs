using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ENTIDADES
{

    [Table("DESPACHO")]
    public class DESPACHO
    {
        [Key]
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Id")]
        public int ID_DESPACHO { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [ForeignKey("VEHICULO")]
        [Display(Name = "Placa")]
        public string PLACA { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [ForeignKey("BOMBA")]
        [Display(Name = "Bomba despacho")]
        public int ID_BOMBA { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [ForeignKey("TIPO_COMBUSTIBLE")]
        [Display(Name = "Combustible")]
        public int ID_TIPO_COMBUSTIBLE { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Cantidad de Galones")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CANTIDAD_GALONES { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Precio de Galones")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PRECIO_GALON { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fecha")]
        
        public DateTime FECHA_DESPACHO { get; set; }

        public virtual VEHICULO VEHICULO { get; set; }
        public virtual BOMBA BOMBA { get; set; }
        public virtual TIPO_COMBUSTIBLE TIPO_COMBUSTIBLE { get; set; }

    }
}
