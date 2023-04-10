using Crud_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_EntityFramework.Data.ControllerEntity
{
    public static class CursoController
    {
        public static void ExcluirCurso(DataContext context, Curso curso)
            => context.Cursos.Remove(curso);
        public static List<Curso> ListarCurso(DataContext context)
            => context.Cursos.AsNoTracking().Include(x=> x.Categoria).ToList();
        public static void UpdateCurso(DataContext context, Curso curso)
            => context.Cursos.Update(curso);
        public static void AdicionarCurso(DataContext context, Curso curso)
            => context.Cursos.Add(curso);
    }
}
