using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class CalculoProjetos
    {

        public List<ResponseProjetos> ObterObjetoResposta()
        {
           return RetornarResponseProjetos();
        }

        private List<ResponseProjetos> RetornarResponseProjetos()
        {
            var projetos = new Projetos().GetAll();
            var atividades = new Atividades().GetAll();
            var Response = new List<ResponseProjetos>();

            foreach (var proj in projetos)
            {

                var qtdAtividadeFinalizada = atividades
                    .Where(ativ => ativ.IdProjeto == proj.IdProjeto)
                    .Where(ativ => ativ.Finalizada).Count();

                var qtdAtividadesTotal = atividades
                    .Where(ativ => ativ.IdProjeto == proj.IdProjeto)
                    .Count();

                var projetoFinalizado = atividades
                    .Where(ativ => ativ.IdProjeto == proj.IdProjeto)
                    .Where(ativ => ativ.DataFim > proj.DataFim).Count() > 0;

                var porc = CalcularPorcentagem(qtdAtividadeFinalizada, qtdAtividadesTotal);

                proj.Atrasado = projetoFinalizado;
                proj.Completo = porc;

                Response.Add(new ResponseProjetos()
                {
                    Projetos = proj,
                    Atividades = atividades.Where(ativ => ativ.IdProjeto == proj.IdProjeto).ToList()
                });
            }

            return Response;

        }

        private double CalcularPorcentagem(int qtdAtivFinalizadas, int qtdAtivTotal)
        {
            return (((double)qtdAtivFinalizadas / (double) qtdAtivTotal) * 100) ;
        }
    }
}
