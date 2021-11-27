# Sql Server Data Provider

Para utilizar os recursos do *provider* de acesso a dados para *Sql Server*, precisamos referenciar o *namespace*: `System.Data.SqlClient`.

## Estabelecendo uma conexão

Para conectar com um banco de dados `Sql Server` precisamos de uma [string de conexão](https://www.connectionstrings.com/microsoft-data-sqlclient/), a qual pode ser construída em tempo de execução via a classe `SqlConnectionStringBuilder`, ou uma string formatada, por exemplo:

```
Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
```

De posse da *string* de conexão, vamos utilizar a implementação `SqlConnection` do componente `Connection`. Neste ponto, podemos observar o conceito do *provider* em ação, pois por exemplo, para conectar com um banco de dados `Oracle` vamos utilizar um *provider* novo que implementa `OracleConnection`.

O código abaixo demonstra como criar e abrir uma conexão com uma base de dados `Sql Server`:

```csharp
// Assumir que connectionString seja uma string de conexão válida
using (SqlConnection connection = new SqlConnection(connectionString))  
{  
    connection.Open();  
    // Do work here.  
}  
```

## Fechando Conexões

Uma boa prática é sempre fechar a conexão após utiliza-la, para que a mesma possa voltar para o `pool` de conexões. Conexões que não são explicitamente fechadas, podem não retornar ao `pool`, onerando o `pool` e muito provavelmente previnindo que o `garbage collector` consiga liberar os objetos assocaidos ao handler desta `Connection`.

Assim é recomendado o uso do bloco `using` para garantir o `dispose` correto da `Connection`, mesmo em caso de `exception`. 

## *Connection Pooling*

Normalmente criar uma conexão com o banco de dados demanda recursos computacionais altos, pois estamos falando se vários passos que consomem e tempo e recursos. Contudo, no dia-a-dia durante a execução de uma aplicação muitas conexões idênticas serão abertas e fechadas. Então para minimizar o custo de abertura de conexões o `ADO.NET` usa uma técnica de otimização chamada *Connection pooling*.

Um componente, `pooler` fica como dono das conexões físicas, mantendo as mesmas vivas conforme configuração. Quando um usuário chama o método `Open` de uma conexão, o `pooler` vai avaliar uma conexão disponível, havendo uma no `pool` essa é retornada ao usuário ao invés de abrir uma nova. Ao chamar o método `Close`, o `pooler` se encarrega de manter essa conexão ativa no `pool` para ser re-utilizada em uma próxima chamada de `Open`.

Importante ressaltar que para cada configuração de conexão, ou `Connection String`, será criado um `pool`. E tal recurso é habilitado por *default* no `ADO.NET`, a não ser que explicitamente seja desabilitado.

### Criação de um Pool de conexões

Quando uma conexão é aberta pela primeira vez, um `pool` de conexões é criado com base na *string* de conexão. Ou seja, cada `pool` é associado a uma *string* de conexão. 

No exemplo abaixo, 3 novos objetos `SqlConnection` são criados, mas apenas 2 `pools` são necessários para gerenciar.

```c#
using (SqlConnection connection = new SqlConnection(  
  "Integrated Security=SSPI;Initial Catalog=Northwind"))  
    {  
        connection.Open();
        // Pool A is created.  
    }  
  
using (SqlConnection connection = new SqlConnection(  
  "Integrated Security=SSPI;Initial Catalog=pubs"))  
    {  
        connection.Open();
        // Pool B is created because the connection strings differ.  
    }  
  
using (SqlConnection connection = new SqlConnection(  
  "Integrated Security=SSPI;Initial Catalog=Northwind"))  
    {  
        connection.Open();
        // The connection string matches pool A.  
    }  
```

## Lendo dados via `DataReader`

Para utilizar uma instância de `DataReader` para ler dados de um data source basta criar uma instância de uma `Connection` e um `Command` para executar o método `Command.ExecuteReader`. O resultado deste método será uma instância de `DataReader`, que provê um *stream* de dados sem necessidade de realizar *cache* na memória da aplicação.

O exemplo abaixo, mostra o uso do `DataReader`:

```c#
using (SqlConnection connection = new SqlConnection(stringConnection))
    {
        SqlCommand command = new SqlCommand(
          "SELECT CategoryID, CategoryName FROM Categories;",
          connection);
        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}", 
                    reader.GetInt32(0),
                    reader.GetString(1));
            }
        }
        else
        {
            Console.WriteLine("No rows found.");
        }
        reader.Close();
    }
```

Importante observar:
- Sempre chame o `Close` do `DataReader`, pois enquanto aberto a `Connection` fica em uso exclusivo do `DataReader`;
- Usar o método `DataReader.Read` para ler uma linha por vez dos resultados;
- O `DataReader` provê uma série de métodos para facilitar o acesso aos valores das colunas.