using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class UserService
{
    ApplicationContext db;
    IMemoryCache cache;
    public UserService(ApplicationContext context, IMemoryCache memoryCache)
    {
        db = context;
        cache = memoryCache;
    }

    public async Task<User?> GetUser(int id)
    {
        cache.TryGetValue(id, out User? user);
        if (user == null)
        {
            
            user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
            

            if (user != null)
            {
                Console.WriteLine($"{user.Name} извлечен из базы данных");
                cache.Set(user.Id, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));
            }
        }
        else
        {
            Console.WriteLine($"{user.Name} извлечен из кэша");
        }
        return user;
    }
}