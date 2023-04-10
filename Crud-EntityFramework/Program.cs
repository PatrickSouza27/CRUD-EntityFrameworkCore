using Crud_EntityFramework.Data;
using Crud_EntityFramework.Data.ControllerEntity;
using Crud_EntityFramework.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CrudEntity
{
    class Program
    {
        public static DataContext DataContexts;
        public static int opcase;
        public static readonly string Opcs = "1 - Listar\n2 - Adicionar\n3 - Update\n4 - Excluir\n5 - Sair\nOpção:";
        public static readonly string MenuOpcs = "1 - Categoria\n2 - Curso\n3 - Sair\nOpção: ";
        static int ValidarEntrada(string regex, string msg)
        {
            string opc;
            while (!Regex.IsMatch(opc = Console.ReadLine(), regex))
            {
                Console.WriteLine("Valor Invalido!");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine(msg);
            }
            return int.Parse(opc);
        }
        static void ListarCategoriasOuCursos()
        {
            if (opcase == 1)
            {
                var categorias = CategoriaController.ListarCategorias(DataContexts);
                categorias.ForEach(x => { Console.WriteLine(x); });
            }
            else
            {
                var cursos = CursoController.ListarCurso(DataContexts);
                cursos.ForEach(x => { Console.WriteLine(x); });
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void OpcEntrada()
        {
            using var data = new DataContext();
            DataContexts = data;
            int opc;
            do
            {
                Console.WriteLine(Opcs);
                opc = ValidarEntrada("^[12345]$", Opcs);
                switch (opc)
                {
                    case 1:
                        ListarCategoriasOuCursos();
                        Console.Clear();
                        break;
                    case 2:
                        AdicionarCursoOuCategoria();
                        Console.Clear();
                        break;
                    case 3:
                        UpdateCursoOuCategoria();
                        Console.Clear();
                        break;
                    case 4:
                        ExcluirCursoOuCategoria();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        break;
                }
                data.SaveChanges();
            } while (opc != 5);
        }
        static void AdicionarCursoOuCategoria()
        {
            if (opcase == 1)
            {
                Console.WriteLine("Digite o Nome da Categoria que deseja Adicionar: ");
                new Categoria(Console.ReadLine()).SalvarCategoria(DataContexts);
            }
            else
            {
                Console.WriteLine("Digite o Nome do Curso que deseja Adicionar: ");
                string nameCurso = Console.ReadLine();
                Console.WriteLine("Digite a Categoria: ");
                string nameCategoria = Console.ReadLine();
                if (DataContexts.Categorias.Where(x => x.Name == nameCategoria).Select(x => x.Id).FirstOrDefault() > 0)
                {
                    new Curso(nameCurso, DataContexts.Categorias.Where(x => x.Name == nameCategoria).FirstOrDefault()).AdicionarCurso(DataContexts);
                }
                else
                {
                    new Curso(nameCurso, new Categoria(nameCategoria)).AdicionarCurso(DataContexts);
                }
            }
        }
        static void UpdateCursoOuCategoria()
        {
            if (opcase == 1)
            {
                Console.WriteLine("Qual Categoria você deseja atualizar, (Id) ?");
                var categoria = DataContexts.Categorias.AsNoTracking().Where(x=> x.Id == int.Parse(Console.ReadLine())).FirstOrDefault();
                Console.WriteLine("Nome: ");
                categoria?.Update(DataContexts, Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Digite o Id do Curso que Deseja atualizar:");
                int id = int.Parse(Console.ReadLine());
                var cursoUpdate = DataContexts.Cursos.Where(x=> x.Id == id).FirstOrDefault();
                Console.WriteLine("Digite o novo nome do Curso: ");
                string nomeCurso = Console.ReadLine();
                Console.WriteLine("Digite a Categoria Vinculada a esse Curso:");
                cursoUpdate.UpdateCurso(nomeCurso, Console.ReadLine());               
            }
        }
        static void ExcluirCursoOuCategoria()
        {
            if (opcase == 1)
            {
                Console.WriteLine("Digite o Id da categoria que deseja excluir: ");
                int id = int.Parse(Console.ReadLine());
                var vinculado = DataContexts.Cursos.Where(x => x.CategoriaId == id).ToList();
                vinculado.ForEach(x => { CursoController.ExcluirCurso(DataContexts, x); });
                CategoriaController.ExcluirCategoria(DataContexts, DataContexts.Categorias.Where(x => x.Id == id).FirstOrDefault());
            }
            else
            {
                Console.WriteLine("Digite o Id do Curso que deseja excluir: ");
                CursoController.ExcluirCurso(DataContexts, DataContexts.Cursos.Where(x => x.Id == int.Parse(Console.ReadLine())).FirstOrDefault());
            }
        }

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(MenuOpcs);
                switch (ValidarEntrada("^[123]$", MenuOpcs))
                {
                    case 1:
                        Console.Clear();
                        opcase = 1;
                        OpcEntrada();
                        break;
                    case 2:
                        Console.Clear();
                        opcase = 2;
                        OpcEntrada();
                        break;
                    case 3:
                        exit = !exit;
                        break;
                }
            }
        }

    }
}
