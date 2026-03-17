using System.Security.Claims;
using Florama.Application.Interfaces;

public class UsuarioLogado : IUsuarioLogado
{
    public long UsuarioId => 1; // mock por enquanto

    //private readonly IHttpContextAccessor _context;

    //public UsuarioLogado(IHttpContextAccessor context)
    //{
    //    _context = context;
    //}

    //public long UsuarioId =>
    //    long.Parse(_context.HttpContext?.User?
    //        .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
}