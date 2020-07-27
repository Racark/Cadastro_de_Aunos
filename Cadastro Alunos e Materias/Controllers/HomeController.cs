using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Cadastro_Alunos_e_Materias.Models;

namespace Cadastro_Alunos_e_Materias.Controllers
{
    public class HomeController : Controller
    {

        private AlunoDB db = new AlunoDB();

        public List<Aluno> GetAlunos()
        {
            try
            {
                var command = new SqlCommand();
                command.Connection = AlunoDB.connect;
                command.CommandText = "SELECT * FROM Alunos.dbo.Alunos ";

                AlunoDB.Connect();

                var reader = command.ExecuteReader();
                List<Aluno> _listaAluno = new List<Aluno>();

                while (reader.Read())
                {
                    var aluno = new Aluno();

                    aluno.Id = Convert.ToInt32(reader["id"]);
                    aluno.Nome = reader["nm_Nome"].ToString();
                    aluno.Email = reader["ds_Email"].ToString();
                    aluno.Foto = (byte[])reader["img_Imagem"];



                    _listaAluno.Add(aluno);
                }

                return _listaAluno;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                AlunoDB.Desconnect();
            }

        }
        public ActionResult Index()
        {
            return View(GetAlunos());
        }

    }
}