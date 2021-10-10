using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    public interface IService
    {
        int Key { get; }
        void Save();
        void Delete();
        List<IService> GetAll();
        List<IService> Busca();
    }
}
