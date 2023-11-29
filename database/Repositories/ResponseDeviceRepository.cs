using core.Entities;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class ResponseDeviceRepository : Repository<ResponseDevice>, IResponseDeviceRepository
{
    public ResponseDeviceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ResponseDevice?>> GetAll()
    {
        return await Set.Where(w => w.Id >= 0).ToListAsync();
    }
}