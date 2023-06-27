#### Entity Framework Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with many databases, including SQL Database (on-premises and Azure), SQLite, MySQL, PostgreSQL, and Azure Cosmos DB.
``` Microsoft.EntityFrameworkCore ```
``` Microsoft.EntityFrameworkCore.SqlServer ```
``` Microsoft.Data.SqlClient.Design ```

### Docker e SQL Server 2019 / Azure Data Studio.

* [Instalação do Docker](https://balta.io/blog/docker-instalacao-configuracao-e-primeiros-passos?utm_source=github&utm_medium=2805-repo&utm_campaign=readme).
* [Instalação do SQL Server no Docker](https://balta.io/blog/sql-server-docker?utm_source=github&utm_medium=2805-repo&utm_campaign=readme)
* [Download do Azure Data Studio](https://docs.microsoft.com/pt-br/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15)

### Controller
```C#
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
```

### Mapeamento 
```C#
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
```
