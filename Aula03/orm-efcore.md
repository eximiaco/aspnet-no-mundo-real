## Entity Framework Core

EF Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API.

### Installation

EF Core is available on [NuGet](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore). Install the provider package corresponding to your target database. See the [list of providers](https://docs.microsoft.com/ef/core/providers/) in the docs for additional databases.

```sh
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Cosmos
```

Use the `--version` option to specify a [preview version](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/absoluteLatest) to install.

### Basic usage

The following code demonstrates basic usage of EF Core. For a full tutorial configuring the `DbContext`, defining the model, and creating the database, see [getting started](https://docs.microsoft.com/ef/core/get-started/) in the docs.

```cs
using (var db = new BloggingContext())
{
    // Inserting data into the database
    db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
    db.SaveChanges();

    // Querying
    var blog = db.Blogs
        .OrderBy(b => b.BlogId)
        .First();

    // Updating
    blog.Url = "https://devblogs.microsoft.com/dotnet";
    blog.Posts.Add(
        new Post
        {
            Title = "Hello World",
            Content = "I wrote an app using EF Core!"
        });
    db.SaveChanges();

    // Deleting
    db.Remove(blog);
    db.SaveChanges();
}
```

[FONTE: GitHub](https://github.com/dotnet/efcore) 