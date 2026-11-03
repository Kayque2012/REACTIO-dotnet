using System.Text.Json;
using System.Text.Json.Serialization;
using Reactio.Models;

namespace Reactio.Repositories;

public class MaterialRepository
{
    private readonly string caminhoArquivo;
    private readonly JsonSerializerOptions opcoes = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        Converters = { new JsonStringEnumConverter(allowIntegerValues: false) }
    };

    public MaterialRepository(string caminhoArquivo)
    {
        this.caminhoArquivo = Path.GetFullPath(caminhoArquivo);
    }

    public List<MaterialApoio> Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return [];

        using var arquivo = File.OpenRead(caminhoArquivo);
        var materiais = JsonSerializer.Deserialize<List<MaterialApoio>>(arquivo, opcoes)
            ?? throw new InvalidDataException("O JSON deve conter uma lista de materiais.");

        var ids = new HashSet<int>();
        var titulos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var material in materiais)
        {
            if (material is null)
                throw new InvalidDataException("O arquivo contém um material nulo.");

            material.Validar();
            if (material.Id <= 0 || !ids.Add(material.Id) || !titulos.Add(material.Titulo.Trim()))
                throw new InvalidDataException("O arquivo contém identificador inválido ou registros duplicados.");
        }

        return materiais;
    }

    public void Salvar(List<MaterialApoio> materiais)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivo)!);
        string temporario = caminhoArquivo + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporario, JsonSerializer.Serialize(materiais, opcoes));
            File.Move(temporario, caminhoArquivo, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporario))
                File.Delete(temporario);
        }
    }
}
