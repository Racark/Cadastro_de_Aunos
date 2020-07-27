using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastro_Alunos_e_Materias.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[] Foto { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Foto")]
        public HttpPostedFileBase ImageUpload { get; set; }

        public List<Materia> aulas;
    }
}