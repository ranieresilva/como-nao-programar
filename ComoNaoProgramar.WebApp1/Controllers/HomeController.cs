using ComoNaoProgramar.WebApp1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ComoNaoProgramar.WebApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly decimal[] descontosINSS = { 0.08m, 0.09m, 0.11m, 608.44m };
        private readonly decimal[] faixaINSS = { 1659.38m, 2765.66m, 5531.31m };
        private readonly decimal[] descontosIR = { 0.075m, 0.15m, 0.225m, 0.275m };
        private readonly decimal[] faixaIR = { 1903.99m, 2826.66m, 3751.06m, 4664.69m };
        private readonly decimal[] adicionalSalarioFamilia = { 44.09m, 31.07m };
        private readonly decimal[] faixaSalarioFamilia = { 859.88m, 1292.43m };

        public IActionResult Index(CalculoFolhaViewModel model)
        {
            var valor = model.SalarioBruto;
            var desconto = 0m;
            var adicional = 0m;
            var mensagens = "";

            if (valor > 0)
            {
                if (model.SalarioBruto <= faixaINSS[0])
                {
                    desconto = model.SalarioBruto * descontosINSS[0];
                    valor -= desconto;
                    mensagens += "<li>INSS 8% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto > faixaINSS[0] && model.SalarioBruto <= faixaINSS[1])
                {
                    desconto = model.SalarioBruto * descontosINSS[1];
                    valor -= desconto;
                    mensagens += "<li>INSS 9% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto > faixaINSS[1] && model.SalarioBruto <= faixaINSS[2])
                {
                    desconto = model.SalarioBruto * descontosINSS[2];
                    valor -= desconto;
                    mensagens += "<li>INSS 11% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto >= faixaINSS[2])
                {
                    desconto = descontosINSS[3];
                    valor -= desconto;
                    mensagens += "<li>INSS TETO = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto >= faixaIR[0] && model.SalarioBruto < faixaIR[1])
                {
                    desconto = model.SalarioBruto * descontosIR[0];
                    valor -= desconto;
                    mensagens += "<li>IR 7,5% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto >= faixaIR[1] && model.SalarioBruto < faixaIR[2])
                {
                    desconto = model.SalarioBruto * descontosIR[1];
                    valor -= desconto;
                    mensagens += "<li>IR 15% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto >= faixaIR[2] && model.SalarioBruto < faixaIR[3])
                {
                    desconto = model.SalarioBruto * descontosIR[2];
                    valor -= desconto;
                    mensagens += "<li>IR 22,5% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto >= faixaIR[3])
                {
                    desconto = model.SalarioBruto * descontosIR[3];
                    valor -= desconto;
                    mensagens += "<li>IR 27,5% = " + desconto.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto <= faixaSalarioFamilia[0])
                {
                    adicional = adicionalSalarioFamilia[0];
                    valor += adicional;
                    mensagens += "<li>Salário Família = " + adicional.ToString("F2") + "</li>";
                }

                if (model.SalarioBruto <= faixaSalarioFamilia[1])
                {
                    adicional = adicionalSalarioFamilia[1];
                    valor += adicional;
                    mensagens += "<li>Salário Família = " + adicional.ToString("F2") + "</li>";
                }
            }

            model.SalarioLiquido = valor;

            ViewBag.Mensagens = mensagens;

            return View("Index", model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}