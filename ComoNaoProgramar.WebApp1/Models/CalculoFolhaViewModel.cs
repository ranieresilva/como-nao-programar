using System.ComponentModel.DataAnnotations;

namespace ComoNaoProgramar.WebApp1.Models
{
    public class CalculoFolhaViewModel
    {
        [Display(Name = "Salário Bruto")]
        public decimal SalarioBruto { get; set; }

        [Display(Name = "Salário Líquido")]
        public decimal SalarioLiquido { get; set; }
    }
}