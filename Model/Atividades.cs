using ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Atividades : Service
    {
        [TableAttribute(UsarNoBancoDeDados = true, ChavePrimaria = true, UsarParaBuscar = false)]
        public int IdAtividade { get; set; }
        [TableAttribute(UsarNoBancoDeDados =true)]
        public string NomeAtividade { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public DateTime DataInicio { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public DateTime DataFim { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public bool Finalizada { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public int IdProjeto { get; set; }

        public new List<Atividades> GetAll()
        {
            var atividades = new List<Atividades>();
            foreach (var ibase in base.GetAll())
            {
                atividades.Add((Atividades)ibase);
            }

            return atividades;
        }
    }
}
