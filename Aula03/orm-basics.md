# ORM 

> Mapeamento objeto-relacional (ou ORM, do inglês: Object-relational mapping) é uma técnica de desenvolvimento utilizada para reduzir a impedância da programação orientada aos objetos utilizando bancos de dados relacionais. As tabelas do banco de dados são representadas através de classes e os registros de cada tabela são representados como instâncias das classes correspondentes.
[wikipédia](https://pt.wikipedia.org/wiki/Mapeamento_objeto-relacional)

Por exemplo podemos fazer um mapeamento objeto - relacional, manualmente como ilustrado abaixo:

```c#
public sealed class Category
{
    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id {get; set;}
    public string Name {get; set;}
}

public IEnumerable<Category> GetAll()
{
    using (SqlConnection connection = new SqlConnection(stringConnection))
    {
        SqlCommand command = new SqlCommand(
          "SELECT CategoryID, CategoryName FROM Categories;",
          connection);
        connection.Open();

        SqlDataReader reader = command.ExecuteReader();
        List<Category> categories = new List<Category>();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                categories.Add(
                    new Category(
                        reader.GetInt32(0),
                        reader.GetString(1)));
            }
        }        
        reader.Close();
        return categories;
    }
}
```

Um ponto forte das ferramentas de ORM existentes é aumentar a produtividade dos desenvolvedores, abstraindo tarefas manuais de "tradução" na leitura/escrita de dados. Então frameworks como [EF](https://github.com/dotnet/efcore) e [NHibernate](https://nhibernate.info/) se destacam por abstrair quase que totalmente o uso de `SQL` pelos desenvolvedores. Mas, acabam pecando no que tange performance e ai frameworks como [Dapper](https://github.com/DapperLib/Dapper) se destacam.