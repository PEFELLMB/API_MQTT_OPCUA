using core.Entities;
using core.Entities.Core;

namespace core.Interfaces;

public interface IResponseDeviceRepository : IRepository<ResponseDevice>
{
    Task<IEnumerable<ResponseDevice?>> GetAll();
}