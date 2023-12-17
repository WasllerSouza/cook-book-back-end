using CookBook.Domain.Entity;
using CookBook.Domain.Repository.UsuarioRepository;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.RepositoryAccess.Repository;

public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly CookBookContext _context;

    public UsuarioRepository(CookBookContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetById(long id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
    }

    public async Task<Usuario> GetUserByEmail(string email)
    {
       return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => email.Equals(usuario.Email));
    }

    public async Task Insert(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> IsAlreadyARegisteredUser(string email)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .AnyAsync(usuario => usuario.Email.Equals(email));
    }

    public async Task<Usuario> SingIn(string email, string password)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => email.Equals(usuario.Email) && password.Equals(usuario.Senha));
    }

    public void Update(Usuario usuario)
    {
        usuario.preUpdate();
        _context.Usuarios.Update(usuario);
    }
}
