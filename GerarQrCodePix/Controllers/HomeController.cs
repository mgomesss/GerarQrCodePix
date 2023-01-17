using GerarQrCodePix.Models;
using RestSharp;
using System;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GerarQrCodePix.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string valor)
        {
            if (valor != null)
            {
                string Inf = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\Inf\\";
                string fullPath = Path.Combine(Inf, "dados.txt");

                using (StreamReader sr = new StreamReader(fullPath))
                {
                    ViewBag.nome = sr.ReadLine();
                    ViewBag.cidade = sr.ReadLine();
                    ViewBag.chave = sr.ReadLine();
                }

                ViewBag.qrcode = "https://gerarqrcodepix.com.br/api/v1?nome=" + ViewBag.nome + "&cidade=" + ViewBag.cidade + "&valor=" + valor + "&saida=" + "qr" + "&chave=" + ViewBag.chave + "";
                ViewBag.copiaecola = SalvarDados.api(ViewBag.nome, ViewBag.cidade, ViewBag.chave, valor, "br");

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dados()
        {
            SalvarDados.Salvar(Request["nome"], Request["cidade"], Request["chave"]);

            Response.Redirect(Url.Action("Index", "Home"));
            return null;
        }
    }
}