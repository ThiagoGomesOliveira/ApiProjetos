using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEuax.Controllers
{
    public class ProjetosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Cadastrar")]
        public void Cadastrar([FromBody] Projetos projeto)
        {
            projeto.Save();
        }

        [HttpGet("ListarProjetos")]
        public List<Projetos> ListarProjetos()
        {
            return new Projetos().GetAll();
        }
    }
}
