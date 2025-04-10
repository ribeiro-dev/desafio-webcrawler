# WebCrawler

Esse foi um projeto desenvolvido para um desafio de backend. Para não deixar o código tão complexo, decidi deixar algumas lógicas harcoded

Um exemplo disso foi a lógica de posição das colunas buscadas. Poderia deixar o crawler menos sugestivo a erros em caso de alteração no layout da tabela

Em contrapartida, adicionei o enum, para facilitar a manutenção em caso de necessidade, onde basta alterar o valor do index de cada coluna e o problema já seria resolvido

Também optei por utilizar o SQLite como banco de dados, para facilitar a utilizacao do projeto. Evitando a necessidade de configurar um banco na máquina.

## Instalação

1. **Clone o repositório**

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```

2. **Restaure os pacotes**
```bash
dotnet restore
```

3. **Certifique-se de que você tenha os seguintes pacotes instalados:**
- Dapper
- HtmlAgilityPack
- Microsoft.Data.Sqlite
- Selenium.WebDriver
- Selenium.Chrome.Webdriver

4. **Compile o projeto**
```bash
dotnet build
```

5. **Execute**
```bash
dotnet run
```
