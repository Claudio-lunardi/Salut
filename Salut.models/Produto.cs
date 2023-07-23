using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salut.models
{
    public class Produto
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int NotaFiscalID { get; set; }

        [Display(Name = "Produto")]
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Quantidade")]
        [Required]
        public int Quantidade { get; set; }

        [NotMapped]
        public NotaFiscal? NotaFiscal { get; set; } 

    }
}
