# Conferência da Sprint 1 - REACTIO / .NET

Referência: **2TDS Agosto - Marinha 2026 - 1º Semestre V2**, páginas 20, 21 e 22. Verificação realizada em **02/11/2026**.

## Resultado

A implementação e a documentação atendem aos requisitos técnicos da PoC console. A entrega acadêmica ainda depende de repositório público no GitHub, contribuições reais de todos os integrantes no histórico e envio do link pelo portal. Não há publicação ou histórico de commits simulado neste pacote.

## Matriz de requisitos

| Critério do PDF | Pontos | Evidência / situação |
| --- | --- | --- |
| Repositório público, commits e .gitignore | 10 | `.gitignore` presente. Publicação e commits reais de todos os integrantes pendentes. |
| README: grupo, solução e escopo | 10 | README com REACTIO, Kayque/567980, Eric/567398, Gabriel/567903, turma 2TDSPB, descrição e recorte da sprint. |
| README: arquitetura e instruções | 10 | Estrutura por pastas e comandos de build/execução. Parágrafo explícito sobre a aplicação do .NET ao REACTIO. |
| Entidade: atributos e validações | 20 | `MaterialApoio`: Id, Titulo, Tema, Tipo, Descricao, Fonte e Url, com validações. |
| Regra de negócio e erros | 15 | Títulos duplicados são rejeitados. `try/catch` mantém o programa em execução diante dos erros previstos. |
| Cadastro | 10 | Opção 1 gera ID e salva o material. |
| Listagem e busca | 15 | Opções 2 e 3: lista completa e pesquisa parcial por título ou tema. |
| Persistência | 10 | JSON salvo em `dados/materiais.json`, reutilizado entre processos. JSON é uma das opções expressamente aceitas na página 21, com a mesma pontuação do Oracle. |

A funcionalidade é complementar à API Java, conforme a página 20: Java mantém afirmações e avalia respostas ao quiz; .NET mantém um catálogo de materiais de apoio. Não há gabarito, quiz ou repetição da entidade Afirmacao neste console. Esse catálogo também corresponde à contribuição .NET prevista no PDF de Compliance.

## Limites de validação

- Título: obrigatório, 3 a 120 caracteres.
- Tema: obrigatório, 3 a 80 caracteres.
- Tipo: 1/Artigo, 2/Video ou 3/Guia.
- Descrição: obrigatória, 10 a 500 caracteres.
- Fonte: obrigatória, 2 a 100 caracteres.
- URL: HTTPS absoluta e válida, até 500 caracteres.
- Busca: termo não vazio, até 120 caracteres; não remove acentos.
- Os campos são aparados no cadastro. A regra de duplicidade ignora maiúsculas/minúsculas e espaços externos.
- Dados carregados também são validados; IDs inválidos, repetidos ou títulos duplicados tornam o arquivo inválido, sem substituí-lo silenciosamente.

## Verificações executadas

Ambiente: Windows x64, SDK .NET **10.0.403**, aplicação destinada a **net10.0**. Nenhum pacote NuGet externo utilizado.

**Build Release: aprovado, 0 erros e 0 avisos.**

**35 cenários automatizados aprovados**, executando o console real em processos com entradas de menu e arquivos isolados. Os resultados individuais estão em `resultado-testes.json`. Não se trata de uma suíte `dotnet test` incluída no projeto; o programa entregue pode ser verificado manualmente pelo roteiro abaixo.

Cobertura: catálogo vazio, menu inválido, cadastro de artigos/vídeos/guias, busca por título/tema e maiúsculas, consulta sem resultados, título duplicado, campos vazios/longos, tipos inválidos, URL inválida, persistência entre processos, JSON malformado/nulo, registro inválido, IDs duplicados e erro de gravação. Foram conferidos o carregamento dos três exemplos e a integridade do ZIP. Uma cópia extraída do pacote também foi compilada e executada.

O programa foi ajustado durante a revisão para tratar `InvalidDataException` explicitamente, preservando o menu quando o arquivo contém JSON nulo ou registros inconsistentes. A bateria completa passou após o ajuste.

## Roteiro manual

1. Abra a pasta do projeto e execute `dotnet build Reactio.Console.csproj -c Release`.
2. Execute `dotnet run --project Reactio.Console.csproj`.
3. Liste o catálogo vazio pela opção 2.
4. Cadastre um material válido pela opção 1. Confira a confirmação e o ID.
5. Liste novamente e pesquise por trecho do título e do tema.
6. Tente repetir o título com letras maiúsculas. Deve ser bloqueado.
7. Tente um tipo inválido, um campo vazio e uma URL sem HTTPS. Deve haver mensagem, sem encerrar o console.
8. Saia pela opção 0 e execute de novo na mesma pasta. O material deve permanecer.

## Formato e pendências

A seção de .NET solicita o repositório público, e não PDF, vídeo, API web, Swagger ou CRUD completo. O ZIP é apenas o pacote para extrair e publicar. Os conteúdos públicos de exemplo podem ir ao GitHub; arquivos locais `dados/`, `bin/` e `obj/` são ignorados.

- [x] PoC console funcional com uma entidade e responsabilidade complementar ao Java.
- [x] README, identificação, arquitetura, execução, validações, regra de negócio e persistência.
- [x] Compilação, testes funcionais e revisão contra as páginas 20-22.
- [ ] Repositório público acessível sem autenticação.
- [ ] Commits reais de todos os integrantes.
- [ ] Envio do link público no portal da disciplina.

Nenhuma nota é presumida: a avaliação de qualidade e dos critérios acadêmicos cabe ao professor. O catálogo usa apenas informações de materiais de apoio; não armazena usuários, idade, pontuação ou dados pessoais dos participantes.
