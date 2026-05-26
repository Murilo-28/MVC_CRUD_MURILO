using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("IdCliente")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Column("IdProduto")]
        [Display(Name = "Produto")]
        public int IdProduto { get; set; }

        [Column("Quantidade")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Column("Preco")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        // Navegação
        [ForeignKey("IdCliente")]
        public Client? Cliente { get; set; }

        [ForeignKey("IdProduto")]
        public Produto? Produto { get; set; }
    }
}