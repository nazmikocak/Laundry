using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface ILaundryService
    {
        Task<IEnumerable<Laundry>> GetLaundriesByDormitoryAsync(int dormitoryId);

        Task<Laundry> GetLaundryByIdAsync(int laundryId);

        Task CreateLaundryAsync(Laundry laundry);

        Task UpdateLaundryAsync(int laundryId, Laundry laundryForUpdate);

        Task DeleteLaundryAsync(int laundryId);

    }
}