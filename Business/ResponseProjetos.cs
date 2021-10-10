using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ResponseProjetos
    {
        public Projetos Projetos { get; set; }
        public List<Atividades> Atividades { get; set; }


        public List<ResponseProjetos> ListarProjetosAndamentos()
        {
            return new CalculoProjetos().ObterObjetoResposta();
        }

    }
}
