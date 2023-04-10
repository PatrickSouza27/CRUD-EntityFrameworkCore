using Crud_EntityFramework.Data;
using Crud_EntityFramework.Data.ControllerEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_EntityFramework.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Name", TypeName = "NVARCHAR")]
        public string Name { get; set; }

        public Categoria() { }
        public Categoria(string name)
        => Name = name;

        
        public override string ToString()
            => "|\t " + Id + " - "+ Name + " \t |";

        public void SalvarCategoria(DataContext context) => CategoriaController.AdicionarCategorias(context, this); 

        public void Update(DataContext conection, string name)
        {
            Name = name;
            CategoriaController.UpdateCategorias(conection, this);
        }
    }
}
