using DevIO.UI.Site.Data;
using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.UI.Site.Controllers
{
    public class TesteCRUDController : Controller
    {
        private readonly MeuDbContext _context;

        public TesteCRUDController(MeuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Nome = "Thales",
                DataNascimento = DateTime.Now,
                Email = "teste@teste.com"
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            var aluno2 = _context.Alunos.Find(aluno.Id);
            var aluno3 = _context.Alunos.FirstOrDefault(x => x.Email == "teste@teste.com");
            var aluno4 = _context.Alunos.Where(x => x.Email == "teste@teste.com");

            aluno.Nome = "João";
            _context.Alunos.Update(aluno);
            _context.SaveChanges();

            _context.Remove(aluno);
            _context.SaveChanges();

            return View("_Layout");
        }
    }
}
