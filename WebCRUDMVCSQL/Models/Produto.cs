using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Column("Id")]
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Nome deve conter apenas letras")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("Preco")]
        [Display(Name = "Preco")]
        public double Preco { get; set; }

        [Column("Peso")]
        [Display(Name = "Peso")]
        public double Peso { get; set; }
    }
}