using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Reactio.Models;
using Reactio.Repositories;
using Reactio.Services;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
string caminho = Path.GetFullPath(Path.Combine("dados", "materiais.json"));
var catalogo = new CatalogoService(new MaterialRepository(caminho));

Console.WriteLine("REACTIO - Catálogo de materiais de apoio");
Console.WriteLine("Artigos, vídeos e guias sobre tecnologia nuclear.");
Console.WriteLine($"Arquivo de dados: {caminho}");

while (true)
{
    Console.WriteLine("\n1 - Cadastrar material\n2 - Listar materiais\n3 - Buscar por título ou tema\n0 - Sair");
    Console.Write("Opção: ");
    string? opcao = Console.ReadLine();
    if (opcao is null || opcao.Trim() == "0")
        break;

    try
    {
        switch (opcao.Trim())
        {
            case "1":
                string titulo = Ler("Título: ");
                string tema = Ler("Tema: ");
                string tipoTexto = Ler("Tipo (1 - artigo, 2 - vídeo, 3 - guia): ");
                if (!int.TryParse(tipoTexto, out int tipo) || !Enum.IsDefined(typeof(TipoMaterial), tipo))
                    throw new ValidationException("Tipo inválido. Use 1, 2 ou 3. Cadastro cancelado.");

                string descricao = Ler("Descrição (10 a 500 caracteres): ");
                string fonte = Ler("Fonte responsável: ");
                string url = Ler("URL HTTPS: ");
                var criado = catalogo.Cadastrar(titulo, tema, (TipoMaterial)tipo, descricao, fonte, url);
                Console.WriteLine($"Material cadastrado e salvo com sucesso. ID: {criado.Id}");
                break;
            case "2":
                Mostrar(catalogo.Listar());
                break;
            case "3":
                Mostrar(catalogo.Buscar(Ler("Pesquisar título ou tema: ")));
                break;
            default:
                Console.WriteLine("Opção inválida. Escolha 1, 2, 3 ou 0.");
                break;
        }
    }
    catch (JsonException)
    {
        Console.WriteLine("Erro: o arquivo JSON está inválido. Confira ou restaure o arquivo de dados. Ele não foi sobrescrito.");
    }
    catch (InvalidDataException ex)
    {
        Console.WriteLine($"Erro no arquivo de dados: {ex.Message}");
        Console.WriteLine("Confira ou restaure o arquivo. Ele não foi sobrescrito.");
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"Erro de validação: {ex.Message}");
    }
    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
    {
        Console.WriteLine($"Não foi possível acessar ou salvar os dados: {ex.Message}");
        Console.WriteLine("Confira a pasta e o arquivo de dados antes de tentar novamente.");
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or OverflowException)
    {
        Console.WriteLine($"Não foi possível concluir: {ex.Message}");
    }
}
Console.WriteLine("REACTIO encerrado.");

static string Ler(string mensagem)
{
    Console.Write(mensagem);
    return Console.ReadLine() ?? "";
}

static void Mostrar(List<MaterialApoio> materiais)
{
    if (materiais.Count == 0)
    {
        Console.WriteLine("Nenhum material encontrado.");
        return;
    }

    foreach (var m in materiais)
    {
        Console.WriteLine($"\n[{m.Id}] {m.Titulo} | {m.Tipo} | Tema: {m.Tema}");
        Console.WriteLine(m.Descricao);
        Console.WriteLine($"Fonte: {m.Fonte}\nURL: {m.Url}");
    }
    Console.WriteLine($"\nTotal: {materiais.Count}");
}
