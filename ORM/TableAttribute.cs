using System;

namespace ORM
{
    public class TableAttribute : Attribute
    {
        public bool UsarNoBancoDeDados { get; set; }
        public bool UsarParaBuscar { get; set; }
        public bool ChavePrimaria { get; set; }
        public bool AutoIncrementar { get; set; }
    }
}
