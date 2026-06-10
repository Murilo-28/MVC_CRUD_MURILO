using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Clientes")]
    public class Client
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Nome deve conter apenas letras")]
        [Display(Name = "Nome")]

        public string Nome { get; set; }

        [Column("Email")]
        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email deve ser @gmail.com")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Column("Telefone")]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Telefone deve conter exatamente 11 números")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Column("Idade")]
        [Required(ErrorMessage = "Idade é obrigatória")]
        [Range(0, 150, ErrorMessage = "Idade deve ser entre 0 e 100")]
        [Display(Name = "Idade")]
        public int Idade { get; set; }
    }
}
