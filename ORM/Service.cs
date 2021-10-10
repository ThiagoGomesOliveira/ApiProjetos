using ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace ORM
{
    public class Service : IService
    {
        [JsonIgnore]
        public int Key
        {
            get
            {
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    TableAttribute pOpcoesBase = (TableAttribute)pi.GetCustomAttribute(typeof(TableAttribute));
                    if (pOpcoesBase != null && pOpcoesBase.ChavePrimaria)
                    {
                        return Convert.ToInt32(pi.GetValue(this));
                    }
                }
                return 0;
            }
        }

        public List<IService> Busca()
        {
            var list = new List<IService>();
            using (SqlConnection connection = new SqlConnection(
               ConnectionOrm.ConnectionString))
            {
                List<string> where = new List<string>();
                string chavePrimaria = string.Empty;
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    TableAttribute pOpcoesBase = (TableAttribute)pi.GetCustomAttribute(typeof(TableAttribute));
                    if (pOpcoesBase != null)
                    {
                        if (pOpcoesBase.ChavePrimaria)
                        {
                            chavePrimaria = pi.Name;
                        }

                        if (pOpcoesBase.UsarParaBuscar)
                        {
                            var valor = pi.GetValue(this);
                            if (valor != null)
                            {
                                where.Add(pi.Name + " = '" + valor + "'");
                            }
                        }
                    }
                }

                string queryString = "select * from " + this.GetType().Name + "s where " + chavePrimaria + " is not null";
                if (where.Count > 0)
                {
                    queryString += " and " + string.Join(" and ", where.ToArray());
                }

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = (IService)Activator.CreateInstance(this.GetType());
                    SetProperty(ref obj, reader);
                    list.Add(obj);
                }
            }
            return list;
        }

        public void Delete()
        {
            using (SqlConnection connection = new SqlConnection(
              ConnectionOrm.ConnectionString))
            {
                string queryString = "delete from " + this.GetType().Name + " where id = " + this.Key + "; ";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<IService> GetAll()
        {
            var list = new List<IService>();
            using (SqlConnection connection = new SqlConnection(
               ConnectionOrm.ConnectionString))
            {
                string queryString = "select * from " + this.GetType().Name;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = (IService)Activator.CreateInstance(this.GetType());
                    SetProperty(ref obj, reader);
                    list.Add(obj);
                }
            }
            return list;
        }

        public void Save()
        {
            using (SqlConnection connection = new SqlConnection(
             ConnectionOrm.ConnectionString))
            {
                List<string> campos = new List<string>();
                List<string> valores = new List<string>();

                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    TableAttribute pOpcoesBase = (TableAttribute)pi.GetCustomAttribute(typeof(TableAttribute));
                    if (pOpcoesBase != null && pOpcoesBase.UsarNoBancoDeDados && !pOpcoesBase.AutoIncrementar)
                    {
                        if (this.Key == 0)
                        {
                            if (!pOpcoesBase.ChavePrimaria)
                            {
                                campos.Add(pi.Name);

                                if (pi.PropertyType.Name == "Double")
                                {
                                    valores.Add("'" + pi.GetValue(this).ToString().Replace(".", "").Replace(",", ".") + "'");
                                }
                                else
                                {
                                    valores.Add("'" + pi.GetValue(this) + "'");
                                }
                            }
                        }
                        else
                        {
                            if (!pOpcoesBase.ChavePrimaria)
                            {
                                valores.Add(" " + pi.Name + " = '" + pi.GetValue(this) + "'");
                            }
                        }
                    }
                }

                string queryString = string.Empty;

                if (this.Key == 0)
                {
                    queryString = "insert into " + this.GetType().Name + " (" + string.Join(", ", campos.ToArray()) + ")values(" + string.Join(", ", valores.ToArray()) + ");";
                }
                else
                {
                    queryString = "update " + this.GetType().Name + "  set " + string.Join(", ", valores.ToArray()) + " where id = " + this.Key + ";";
                }
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private void SetProperty(ref IService obj, SqlDataReader reader)
        {
            foreach (PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                TableAttribute pOpcoesBase = (TableAttribute)pi.GetCustomAttribute(typeof(TableAttribute));
                if (pOpcoesBase != null && pOpcoesBase.UsarNoBancoDeDados)
                {
                    pi.SetValue(obj, reader[pi.Name]);
                }
            }
        }
        private string TipoPropriedade(PropertyInfo pi)
        {
            switch (pi.PropertyType.Name)
            {
                case "Int32":
                    return "int";
                case "Int64":
                    return "bigint";
                case "Double":
                    return "decimal(9, 2)";
                case "Single":
                    return "float";
                case "DateTime":
                    return "datetime";
                case "Boolean":
                    return "tinyint";
                default:
                    return "varchar(255)";
            }
        }
    }
}
