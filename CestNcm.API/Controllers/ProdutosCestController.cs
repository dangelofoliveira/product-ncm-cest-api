using CestNcm.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CestNcm.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace CestNcm.API.Controllers;

[ApiController]
[Route("api/produtos")]
[Authorize]
public class ProdutosCestController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    #region Endpoints Públicos

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        if (_context.ProdutosCest == null) { return NotFound("Contexto de produtos CEST não encontrado."); }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Dados inválidos",
                Detail = "O modelo de dados não é válido.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var produtos = await _context.ProdutosCest.ToListAsync();

        if (!produtos.Any()) { return NotFound("Nenhum produto encontrado."); }

        return Ok(produtos);
    }

    [Authorize]
    [HttpGet("{cest}")]
    public async Task<IActionResult> GetPorCest(string cest)
    {
        if (string.IsNullOrWhiteSpace(cest))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Campo obrigatório ausente",
                Detail = "Cest não pode ser vazio ou nulo.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var cestFormatado = FormatarCest(cest);

        var produtos = await _context.ProdutosCest
            .Where(p => p.Cest == cestFormatado || EF.Functions.ILike(p.Cest, $"%{cestFormatado}%"))
            .ToListAsync();

        if (!produtos.Any()) { return NotFound($"Nenhum produto encontrado com CEST {cest}"); }

        return Ok(produtos);
    }

    [Authorize]
    [HttpGet("ncm/{ncm}")]
    public async Task<IActionResult> GetPorNcm(string ncm)
    {
        if (string.IsNullOrWhiteSpace(ncm))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Campo obrigatório ausente",
                Detail = "NCM não pode ser vazio ou nulo.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var ncmFormatado = FormatarNcm(ncm);

        var produtos = await _context.ProdutosCest
            .Where(p => p.Ncm == ncmFormatado || EF.Functions.ILike(p.Ncm, $"%{ncmFormatado}%"))
            .ToListAsync();

        if (!produtos.Any()) { return NotFound($"Nenhum produto encontrado com NCM {ncm}"); }

        return Ok(produtos);
    }

    [Authorize]
    [HttpGet("secao/{secao}")]
    public async Task<IActionResult> GetPorSecao(string secao)
    {
        if (string.IsNullOrWhiteSpace(secao))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Campo obrigatório ausente",
                Detail = "Secão não pode ser vazia ou nula.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var secaoFormatada = FormatarSecao(secao);

        var produtos = await _context.ProdutosCest
            .Where(p => p.Secao == secaoFormatada || EF.Functions.ILike(p.Secao, $"%{secaoFormatada}%"))
            .ToListAsync();

        if (!produtos.Any()) { return NotFound($"Nenhum produto encontrado na seção {secao}"); }

        return Ok(produtos);
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> BuscarPorFiltros(
    [FromQuery] string? descricao,
    [FromQuery] string? ncm,
    [FromQuery] string? secao,
    [FromQuery] string? cest)
    {
        if (string.IsNullOrWhiteSpace(descricao) && string.IsNullOrWhiteSpace(ncm) && string.IsNullOrWhiteSpace(secao) && string.IsNullOrWhiteSpace(cest))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Campo obrigatório ausente",
                Detail = "Pelo menos um filtro deve ser fornecido.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var query = _context.ProdutosCest.AsQueryable();

        var ncmFormatado = FormatarNcm(ncm);
        var secaoFormatada = FormatarSecao(secao);
        var cestFormatado = FormatarCest(cest);

        if (!string.IsNullOrEmpty(descricao)) { query = query.Where(p => EF.Functions.ILike(p.Descricao, $"%{descricao}%")); }

        if (!string.IsNullOrEmpty(ncm)) { query = query.Where(p => p.Ncm == ncmFormatado || EF.Functions.ILike(p.Ncm, $"%{ncmFormatado}%")); }

        if (!string.IsNullOrEmpty(secao)) { query = query.Where(p => p.Secao == secaoFormatada || EF.Functions.ILike(p.Secao, $"%{secaoFormatada}%")); }

        if (!string.IsNullOrEmpty(cest)) { query = query.Where(p => p.Cest == cestFormatado || EF.Functions.ILike(p.Cest, $"%{cestFormatado}%")); }

        var produtos = await query.ToListAsync();

        if (!produtos.Any()) { return NotFound("Nenhum produto encontrado com os filtros aplicados."); }

        return Ok(produtos);
    }

    #endregion

    #region Endpoints Protegidos

    // [Authorize]
    // [HttpGet("seguro")]
    // public IActionResult SoParaLogados()
    // {
    //     return Ok("Você está autenticado com sucesso!");
    // }

    #endregion

    #region Formatação de Dados

    private string? FormatarSecao(string? secao)
    {
        return string.IsNullOrWhiteSpace(secao) ? null : secao.Trim().ToLower();
    }

    private string? FormatarCest(string? cest)
    {
        if (string.IsNullOrWhiteSpace(cest)) { return null; }

        // Remove tudo que não for número
        var apenasNumeros = new string(cest.Where(char.IsDigit).ToArray());

        // Se tiver 7 dígitos, formata como CEST padrão
        if (apenasNumeros.Length == 7) { return $"{apenasNumeros.Substring(0, 2)}.{apenasNumeros.Substring(2, 3)}.{apenasNumeros.Substring(5, 2)}"; }

        // Se não for 7 dígitos, devolve original
        return cest;
    }

    private string? FormatarNcm(string? ncm)
    {
        if (string.IsNullOrWhiteSpace(ncm)) { return null; }

        // Remove tudo que não for número
        var apenasNumeros = new string(ncm.Where(char.IsDigit).ToArray());

        // Se tiver 8 dígitos, formata como NCM padrão
        if (apenasNumeros.Length == 8) { return $"{apenasNumeros.Substring(0, 4)}.{apenasNumeros.Substring(4, 2)}.{apenasNumeros.Substring(6, 2)}"; }

        // Se não for 8 dígitos, devolve original
        return ncm;
    }

    #endregion
}
// Este controller fornece endpoints para consultar produtos por CEST, NCM, seção e outros filtros.
// Ele também inclui um endpoint protegido que requer autenticação para acessar.