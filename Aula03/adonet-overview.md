# `ADO.NET` Visão Geral

O objetivo, do `ADO.NET` é prover uma forma consistente para acessso a `data sources` diversos, como `SQL Server`, `XML`, fontes de dados expostas atráves de `OLE DB` e `ODBC`. Através deste provedor de acesso podemos conectar, recuperar e atualizar dados nestas fontes dados conectadas ao `ADO.NET`.

De forma geral `ADO.NET` possui componentes para acesso a dados e componentes que fazem manipulação dos dados, ou seja, atráves de `.NET Frameworks data providers` o `ADO.NET` consegue conectar a uma database, executar comandos e recuperar resultados. Uma vez realizado um comando que recupera dados, estes dados são colocados em um `ADO.NET Data Set` que pode ser manipulado diretamente, combinado com outras fontes de dados ou passado para outras camadas.

## `.NET Framework` *Data Providers*

Os componentes que compoem um *data provider* são explicitamente desenhados para realizar manipulação de dados de forma rápida, *forward-only* e acesso somente leitura a dados. Dentre estes componentes o primeiro que realiza a conexão com uma fonte de dados é a classe `Connection`. O componente `Command` por sua vez habilita acesso a  dados, através de uma `Connection`, para retornar dados, modifica-los e executar *stored procedures*. Um vez que dados foram lidos e precisam ser retornados, o componente `DataReader` entre em ação para entregar estes dados através de um *stream* de alta performance. Por fim, existe um componente responsável, `DataAdapter` para fazer uma "ponte" para os componentes de `DataSet`, fazendo a conciliação entre o data source e `DataSet`.

## DataSet

O objetivo de um `DataSet` é acessar dados independente de qualquer fonte de dados. Assim, ele pode ser utilizado com multiplas e diferentes fontes de dados. O `DataSet` é composto por uma coleção de `DataTable` que consistem em linhas (*rows*) e colunas (*columns*) dos dados. Além destes objetos, uma `DataTable` possui outras representações para dados relacionais, como *primary key*, *foreing key* e etc.

Em resumo um `DataSet` seria uma representação de uma database em memória, que pode ser conciliada com *data sources* multiplos, através de um `DataAdapter`. Veja abaixo os componentes de um `DataSet`.

![DataSet diagram](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/media/ado-1-bpuedev11.png)

## Escolhendo entre `DataReader` ou `DataSet`

Considere os seguintes motivos para usar `DataSet` na sua aplicação:
- Realizar cache local para manipular os dados dentro da aplicação;
- Interagir com dados de forma dinâmica como por exemplo através de *binding* com um controle *Windows Forms/WPF*;
- Realizar processamento nos dados sem a necessidade de uma conexão aberta com o *data source*
  
O uso de `DataReader` é recomendo para uso de leitura de dados com alta performance e reduzindo o uso de memoria, que um `DataSet` iria consumir. De forma, geral recomenda-se o uso de `DataReader` para leitura de dados e apenas em casos citados acima o uso de um `DataSet`.

Dois pontos importantes na comparação entre os componentes é a necessidade do `DataReader` ter uma `Connection` aberta com o data source para realizar a leitura. Por fim, mesmo utilizando um `DataSet`, o uso de `DataReader` acontece por baixo do capo pois o `DataAdapter` que vai alimentar o `DataSet` utiliza um `DataReader` para fazer o trabalho.