using Crud_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_EntityFramework.Data.ControllerEntity
{
    public static class CategoriaController
    {
        public static void ExcluirCategoria(DataContext context, Categoria categoria)
            => context.Categorias.Remove(categoria);
        public static List<Categoria> ListarCategorias(DataContext context)
            => context.Categorias.AsNoTracking().ToList();
        public static void UpdateCategorias(DataContext context, Categoria categoria)
            => context.Categorias.Update(categoria);
        public static void AdicionarCategorias(DataContext context, Categoria categoria)
            => context.Categorias.Add(categoria);
    }
}
