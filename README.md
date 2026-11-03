# REACTIO - Catálogo de materiais de apoio

**Advanced Business Development with .NET | Sprint 1 | 2TDSPB**

| Integrante | RM |
| --- | --- |
| Kayque | 567980 |
| Eric | 567398 |
| Gabriel | 567903 |

## Solução e escopo

O REACTIO propõe microaprendizagem sobre tecnologia nuclear para todas as faixas etárias, inspirada em lições curtas como as do Duolingo. Nesta sprint, a PoC .NET é um console para cadastrar e consultar artigos, vídeos e guias de apoio.

**Como o .NET se aplica à solução do grupo:** C# e .NET permitem organizar materiais que complementam a aprendizagem, validar cadastros, evitar títulos repetidos e manter os registros em JSON. Enquanto a API Java cuida das afirmações e respostas ao quiz, esta PoC permite consultar recursos para aprofundar os temas. São funcionalidades complementares, sem integração nesta etapa.

## Executar

Requisito: [SDK .NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) instalado. Não exige banco externo ou pacote NuGet adicional. No terminal, entre na pasta que contém `Reactio.Console.csproj`:

```bash
dotnet build Reactio.Console.csproj -c Release
dotnet run --project Reactio.Console.csproj
```

Também é possível abrir o `.csproj` no Rider ou Visual Studio com suporte ao .NET 10.

## Menu e regras

- **1:** cadastrar título, tema, tipo (1 artigo, 2 vídeo, 3 guia), descrição, fonte e URL HTTPS.
- **2:** listar todos os materiais, ordenados por título.
- **3:** buscar por trecho do título ou tema, ignorando maiúsculas/minúsculas.
- **4:** filtrar materiais por tipo (artigo, vídeo ou guia).
- **0:** sair.

A entidade **MaterialApoio** possui esses atributos e um ID gerado. Os campos são obrigatórios e têm limites de tamanho; o tipo e o endereço são validados. **Regra de negócio:** bloquear títulos repetidos, ignorando maiúsculas/minúsculas e espaços externos. A busca também ignora acentos. Erros de entrada e arquivo são tratados com `try/catch`, mantendo o menu aberto.

## Estrutura e dados

`Program.cs`: menu e erros. `Models/`: entidade e validações. `Services/`: cadastro, consultas e regra de negócio. `Repositories/`: leitura e gravação com `System.Text.Json`.

O catálogo começa vazio. O primeiro cadastro cria `dados/materiais.json`. Execute novamente na mesma pasta para manter os registros. Use uma instância por vez. JSON inválido não é sobrescrito por uma lista vazia.

Há três referências públicas em `exemplos/materiais.json`. Para usá-las, com o programa fechado, copie o arquivo para a pasta `dados` sem sobrescrever cadastros próprios. Também é possível cadastrar pelo menu. Os links dos exemplos estão em inglês e podem exigir mediação de um educador ou responsável.

## Verificação e entrega

Veja [docs/CONFERENCIA.md](docs/CONFERENCIA.md) para requisitos, validações e resultados. Para testar manualmente: cadastre, liste, pesquise, tente repetir o título, teste uma URL inválida e reinicie para conferir a persistência.

Publique este conteúdo em **repositório público no GitHub com commits reais de todos os integrantes** e envie o link no portal FIAP. Não basta enviar o ZIP. O `.gitignore` exclui `bin/`, `obj/` e os dados locais; os exemplos podem ser publicados. A seção .NET não exige vídeo, Swagger ou aplicação web nesta sprint.
