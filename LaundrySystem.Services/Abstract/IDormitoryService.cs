using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IDormitoryService
    {
        Task<IEnumerable<Dormitory>> GetAllDormitoriesAsync();

        Task<Dormitory> GetDormitoryByIdAsync(int dormitoryId);

        Task CreateDormitoryAsync(Dormitory dormitory);

        Task UpdateDormitoryAsync(int dormitoryId, Dormitory dormitoryForUpdate);

        Task DeleteDormitoryAsync(int dormitoryId);
    }
}