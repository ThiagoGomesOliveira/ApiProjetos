using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEuax.Controllers
{
    public class ProjetosAndamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ListarAndamentoProjetos")]
        public List<ResponseProjetos> ListarProjetosAndamentos()
        {
            return new ResponseProjetos().ListarProjetosAndamentos();
        }
    }
}
