using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentity.Controllers
{
    //Autenticado
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IKLogger _logger;

        public HomeController(IKLogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Trace("Usuário acessou a home");

            return View();
        }
        
        public IActionResult Privacy()
        {
            throw new Exception("Erro");

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            try
            {
                throw new Exception("Algo muito ruim aconteceu");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }

            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimEscrever()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if(id == 500)
            {
                modelErro.Mensagem = "Servidor => erro500";
                modelErro.Titulo = "Ocorreu um erro";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "Não encontrado => erro404";
                modelErro.Titulo = "Ocorreu um erro";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Permissão => erro403";
                modelErro.Titulo = "Ocorreu um erro";
                modelErro.ErroCode = id;
            }
            else
            {
                return NotFound();
            }

            return View("Error", modelErro);
        }
    }
}