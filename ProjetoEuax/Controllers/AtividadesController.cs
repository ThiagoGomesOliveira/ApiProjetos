using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEuax.Controllers
{
    public class AtividadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ListarAtividades")]
        public List<Atividades> ListarAtividades()
        {
            return new Atividades().GetAll();
        }
        [HttpPost("CadastrarAtividades")]
        public void CadastrarAtividades([FromBody] Atividades atividade)
        {
            atividade.Save();
        }
        [HttpPut("AlterarAtividade")]
        public void AlterarAtividade([FromBody] Atividades atividade)
        {
            atividade.Save();
        }
    }
}
