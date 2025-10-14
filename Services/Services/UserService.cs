using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService(FinanceiroContext context, HashService hashService)
{
    public async Task<User> CreateUser(string username, string password)
    {
        if (await context.Users.AnyAsync(u => u.Username == username))
        {
            throw new Exception("Username already exists");
        }

        string id = Guid.NewGuid().ToString();
        string passwordHash = hashService.Hash(password);

        var user = new User
        {
            Id = id,
            Username = username,
            PasswordHash = passwordHash,
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> Login(string username, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !hashService.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }
}
