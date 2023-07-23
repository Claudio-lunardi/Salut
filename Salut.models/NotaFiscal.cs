using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salut.models
{
    public class NotaFiscal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(18)]
        [Display(Name ="CNPJ")]
        [Required]
        public string CNPJ { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Canal da compra do produto")]
        public string CanalcompraProduto { get; set; }

        [Display(Name = "Data da compra")]
        [Required]
        public DateTime DataCompra { get; set; }

        [StringLength(50)]
        [Display(Name = "Número do cupom fiscal")]
        [Required]
        public string NumeroCupomFiscal { get; set; }

        [Required]
        public Guid GuidArquivoName { get; set; }

        [NotMapped]
        public IFormFile ArquivoFiscal { get; set; }

        [NotMapped]
        public List<Produto> Produto { get; set; }
   

    }
}
