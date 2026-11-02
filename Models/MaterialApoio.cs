using System.ComponentModel.DataAnnotations;

namespace Reactio.Models;

public enum TipoMaterial
{
    Artigo = 1,
    Video = 2,
    Guia = 3
}

public class MaterialApoio
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o título.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O título deve ter de 3 a 120 caracteres.")]
    public string Titulo { get; set; } = "";

    [Required(ErrorMessage = "Informe o tema.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "O tema deve ter de 3 a 80 caracteres.")]
    public string Tema { get; set; } = "";

    public TipoMaterial Tipo { get; set; }

    [Required(ErrorMessage = "Informe a descrição.")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter de 10 a 500 caracteres.")]
    public string Descricao { get; set; } = "";

    [Required(ErrorMessage = "Informe a fonte responsável pelo material.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "A fonte deve ter de 2 a 100 caracteres.")]
    public string Fonte { get; set; } = "";

    [Required(ErrorMessage = "Informe o endereço do material.")]
    [StringLength(500, ErrorMessage = "O endereço deve ter no máximo 500 caracteres.")]
    public string Url { get; set; } = "";

    public void Validar()
    {
        Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);

        if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Tema)
            || string.IsNullOrWhiteSpace(Descricao) || string.IsNullOrWhiteSpace(Fonte))
            throw new ValidationException("Os campos de texto não podem conter apenas espaços.");

        if (!Enum.IsDefined(Tipo))
            throw new ValidationException("Tipo inválido. Use 1 (artigo), 2 (vídeo) ou 3 (guia).");

        if (!Uri.TryCreate(Url, UriKind.Absolute, out var endereco)
            || endereco.Scheme != Uri.UriSchemeHttps || string.IsNullOrWhiteSpace(endereco.Host))
            throw new ValidationException("Informe uma URL HTTPS válida, como https://www.gov.br/.");
    }
}
