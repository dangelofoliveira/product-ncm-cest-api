using System.Text.Json;
using CestNcm.Domain.Entities;
using CestNcm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CestNcm.DataImporter.Loaders;

public class JsonImporter(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task ImportFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Arquivo não encontrado: {filePath}");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("📖 Lendo arquivo JSON...");

        var jsonContent = await File.ReadAllTextAsync(filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var dados = JsonSerializer.Deserialize<Dictionary<string, List<ProdutoCest>>>(jsonContent, options);

        if (dados is null || dados.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ Nenhum dado encontrado no JSON.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("📦 Inserindo dados no banco...");

        int total = 0, ignorados = 0;

        foreach (var secao in dados)
        {
            foreach (var produto in secao.Value)
            {
                total++;

                if (string.IsNullOrWhiteSpace(produto.Cest) || string.IsNullOrWhiteSpace(produto.Ncm) || string.IsNullOrWhiteSpace(produto.Descricao))
                {
                    ignorados++;
                    Console.WriteLine($"⚠️ Ignorado: CEST='{produto.Cest}', NCM='{produto.Ncm}', Descrição='{produto.Descricao}'");
                    continue;
                }

                produto.Secao = secao.Key;
                _context.ProdutosCest.Add(produto);
            }
        }

        await _context.SaveChangesAsync();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ Importação concluída com sucesso!");
        Console.WriteLine($"✔️ Inseridos: {total - ignorados} | ❌ Ignorados: {ignorados}");
        Console.ResetColor();
    }
}
// This code is part of the CestNcm project, which is licensed under the MIT License.
// See the LICENSE file in the project root for more information.