using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalizaAmigos.Models
{
    [Table("tb_amigos")]
    public class Amigo
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int amigoID { get; set;}
      
        [StringLength(30), Required(ErrorMessage = "O Nome é obrigatório")]
        public string nome { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }
    }
}
