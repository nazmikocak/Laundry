using LaundrySystem.Entities.Models;

namespace LaundrySystem.Repositories.Abstract
{
    public interface ILaundryRepository : IRepositoryBase<Laundry>
    {
        Task<IEnumerable<Laundry>> GetLaundriesByDormitoryAsync(int dormitoryId);
    }
}