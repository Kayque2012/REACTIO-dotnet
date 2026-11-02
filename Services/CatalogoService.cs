using Reactio.Models;
using Reactio.Repositories;

namespace Reactio.Services;

public class CatalogoService
{
    private readonly MaterialRepository repository;

    public CatalogoService(MaterialRepository repository)
    {
        this.repository = repository;
    }

    public MaterialApoio Cadastrar(string titulo, string tema, TipoMaterial tipo,
        string descricao, string fonte, string url)
    {
        var material = new MaterialApoio
        {
            Titulo = titulo.Trim(),
            Tema = tema.Trim(),
            Tipo = tipo,
            Descricao = descricao.Trim(),
            Fonte = fonte.Trim(),
            Url = url.Trim()
        };
        material.Validar();

        var materiais = repository.Carregar();
        // Regra de negócio: não repetir títulos no catálogo, ignorando maiúsculas e espaços externos.
        if (materiais.Any(m => string.Equals(m.Titulo.Trim(), material.Titulo, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Já existe um material com esse título. Consulte o catálogo antes de cadastrar.");

        material.Id = materiais.Count == 0 ? 1 : checked(materiais.Max(m => m.Id) + 1);
        materiais.Add(material);
        repository.Salvar(materiais);
        return material;
    }

    public List<MaterialApoio> Listar()
    {
        return repository.Carregar().OrderBy(m => m.Titulo, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public List<MaterialApoio> Buscar(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo) || termo.Trim().Length > 120)
            throw new ArgumentException("Informe um termo de pesquisa com até 120 caracteres.");

        termo = termo.Trim();
        return Listar().Where(m => m.Titulo.Contains(termo, StringComparison.OrdinalIgnoreCase)
            || m.Tema.Contains(termo, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
