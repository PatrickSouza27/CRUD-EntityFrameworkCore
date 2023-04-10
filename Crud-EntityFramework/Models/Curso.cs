using Crud_EntityFramework.Data;
using Crud_EntityFramework.Data.ControllerEntity;
using CrudEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_EntityFramework.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Name", TypeName = "NVARCHAR")]
        public string Name { get; set; }

        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Curso() { }
        public Curso(string name, Categoria categoria)
        {
            Name = name;
            Categoria = categoria;
        }
        public override string ToString()
         => "| \t" + Id + " - " + Name + " - " + Categoria.Name;


        public void AdicionarCurso(DataContext datacontext) => CursoController.AdicionarCurso(datacontext, this);

        public void UpdateCurso(string name, string categoria)
        {
            Name = name;
            if(Program.DataContexts.Categorias.AsNoTracking().Where(x=> x.Name == categoria).Select(x=> x.Id).FirstOrDefault() > 0)
            {
                Categoria = Program.DataContexts.Categorias.AsNoTracking().Where(x => x.Name == categoria).FirstOrDefault();
            }
            else
            {
                 new Categoria(categoria).SalvarCategoria(Program.DataContexts);
                 Categoria = Program.DataContexts.Categorias.AsNoTracking().Where(x => x.Name == categoria).FirstOrDefault();
            }
            CursoController.UpdateCurso(Program.DataContexts, this);
        }

    }
}
