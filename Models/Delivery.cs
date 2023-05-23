using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appPetech.Models
{
    [Table("t_delivery")]
    public class Delivery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre {get; set; }

        [Column("apepat")]
        public string ApellidoPaterno { get; set;} 
        
        [Column("apemat")]
         public string ApellidoMaterno { get; set;} 

        [Column("dni")]
        public string Dni {get; set; }

        [Column("celular")]
        public string Celular { get; set;} 
        
        [Column("vehiculo")]
         public string Vehiculo { get; set;} 
           
        [Column("placa")]
         public string Placa { get; set;} 

    }
}