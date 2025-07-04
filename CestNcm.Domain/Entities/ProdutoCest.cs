using System.Text.Json.Serialization;

namespace CestNcm.Domain.Entities;

public class ProdutoCest
{
    public int Id { get; set; }
    public string Secao { get; set; } = null!;
    public string Cest { get; set; } = null!;
    public string Ncm { get; set; } = null!;
    public string Descricao { get; set; } = null!;

    [JsonPropertyName("mva_original")]
    public decimal? MvaOriginal { get; set; }

    [JsonPropertyName("mva_substituto")]
    public decimal? MvaSubstituto { get; set; }

    [JsonPropertyName("mva_ajustada_12")]
    public decimal? MvaAjustada12 { get; set; }

    [JsonPropertyName("mva_ajustada_4")]
    public decimal? MvaAjustada4 { get; set; }
}
// This code is part of the CestNcm project, which is licensed under the MIT License.
// See the LICENSE file in the project root for more information.