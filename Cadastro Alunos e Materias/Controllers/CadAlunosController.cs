using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cadastro_Alunos_e_Materias.Models;
using System.Data.SqlClient;
using System.IO;

namespace Cadastro_Alunos_e_Materias.Controllers
{
    public class CadAlunosController : Controller
    {
        private AlunoDB db = new AlunoDB();

        public Aluno ProcuraAluno(int id)
        {
            try
            {
                var aluno = new Aluno();
                var command = new SqlCommand();
                command.Connection = AlunoDB.connect;
                command.CommandText = "SELECT * FROM Alunos.dbo.Alunos WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);

                AlunoDB.Connect();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    aluno.Id = Convert.ToInt32(reader["id"]);
                    aluno.Nome = reader["nm_Nome"].ToString();
                    aluno.Email = reader["ds_Email"].ToString();
                    aluno.Foto = (byte[])reader["img_Imagem"];

                }

                return aluno;

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

        // GET: CadAlunos
        public ActionResult Index()
        {
            return View(GetAlunos());
        }

        // GET: CadAlunos/Details/5
        public ActionResult Details(int id)
        {
            Aluno aluno = ProcuraAluno(id);
            return View(aluno);
        }

        // GET: CadAlunos/Create
        public ActionResult Create()
        {

            var model = new Aluno();
            return View(model);
        }

        // POST: CadAlunos/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,ImageUpload")] Aluno model)
        {

            if (ModelState.IsValid)
            {
                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    model.Foto = binaryReader.ReadBytes(model.ImageUpload.ContentLength);

                try
                {
                    var command = new SqlCommand
                    {
                        Connection = AlunoDB.connect,
                        CommandText = "INSERT INTO Alunos.dbo.Alunos VALUES (@NOME, @EMAIL, @IMG)"

                    };
                    command.Parameters.AddWithValue("@NOME", model.Nome);
                    command.Parameters.AddWithValue("@EMAIL", model.Email);
                    command.Parameters.AddWithValue("@IMG", model.Foto);

                    AlunoDB.Connect();

                    command.ExecuteReader();

                    return RedirectToAction("Index");
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
            else
            {
                return View(model);
            }
        }

        // GET: CadAlunos/Edit/5
        public ActionResult Edit(int id)
        {
            Aluno aluno = ProcuraAluno(id);

            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: CadAlunos/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,ImageUpload")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                using (var binaryReader = new BinaryReader(aluno.ImageUpload.InputStream))
                    aluno.Foto = binaryReader.ReadBytes(aluno.ImageUpload.ContentLength);

                try
                {
                    var command = new SqlCommand
                    {
                        Connection = AlunoDB.connect,
                        CommandText = "UPDATE Alunos.dbo.Alunos SET nm_Nome = @NOME, ds_Email = @EMAIL, img_Imagem = @IMG WHERE Id = @ID"

                    };
                    command.Parameters.AddWithValue("@NOME", aluno.Nome);
                    command.Parameters.AddWithValue("@EMAIL", aluno.Email);
                    command.Parameters.AddWithValue("@IMG", aluno.Foto);
                    command.Parameters.AddWithValue("@ID", aluno.Id);

                    AlunoDB.Connect();

                    command.ExecuteReader();

                    return RedirectToAction("Index");
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
            else
            {
                return View(aluno);
            }
        }

        // GET: CadAlunos/Delete/5
        public ActionResult Delete(int id)
        {
            Aluno aluno = ProcuraAluno(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: CadAlunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new SqlCommand
                    {
                        Connection = AlunoDB.connect,
                        CommandText = "DELETE FROM Alunos.dbo.Alunos WHERE Id = @ID"

                    };

                    command.Parameters.AddWithValue("@ID", id);

                    AlunoDB.Connect();

                    command.ExecuteReader();

                    return RedirectToAction("Index");
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
            else
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
