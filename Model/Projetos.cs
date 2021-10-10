using ORM;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Projetos : Service
    {
        [TableAttribute(UsarNoBancoDeDados = true, ChavePrimaria = true, UsarParaBuscar = false)]
        public int IdProjeto { get; set; }
        [TableAttribute(UsarNoBancoDeDados =true)]
        public string NomeProjeto { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public DateTime DataInicio { get; set; }
        [TableAttribute(UsarNoBancoDeDados = true)]
        public DateTime DataFim { get; set; }
        public double Completo { get; set; }
        public bool Atrasado { get; set; }

        public new List<Projetos> GetAll()
        {
            var projetos = new List<Projetos>();
            foreach (var ibase in base.GetAll())
            {
                projetos.Add((Projetos)ibase);
            }

            return projetos;
        }
    }
}
