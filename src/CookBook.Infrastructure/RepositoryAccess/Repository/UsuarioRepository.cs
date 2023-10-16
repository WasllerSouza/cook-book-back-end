using CookBook.Domain.Entity;
using CookBook.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.RepositoryAccess.Repository;

public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository
{
    private readonly CookBookContext _context;

    public UsuarioRepository(CookBookContext context)
    {
        _context = context;
    }
    public async Task Insert(Usuario usuario)
    {
       await _context.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> IsAlreadyARegisteredUser(string email)
    {
        return await _context.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email));
    }
}
