using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastro_Alunos_e_Materias.Models
{
    public class Materia
    {
        int Id { get; set; }
        string Nome { get; set; }

        int IdAula { get; set; }
    }
}