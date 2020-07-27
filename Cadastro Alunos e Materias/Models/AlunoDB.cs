using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Cadastro_Alunos_e_Materias.Models
{
    public class AlunoDB : DbContext
    {
        public static SqlConnection connect = new SqlConnection();
        public DbSet<Aluno> Alunos { get; set; }

        public static void Connect()
        {
            connect.ConnectionString = @"Data Source=LAPTOP-1K5IFBKQ\SQLEXPRESS;Initial Catalog=Alunos;Integrated Security=True";
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public static void Desconnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
    }
}