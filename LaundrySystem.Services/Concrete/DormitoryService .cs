using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Exceptions;

namespace LaundrySystem.Services.Concrete
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IRepositoryManager _repositoryManager;


        public DormitoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<Dormitory>> GetAllDormitoriesAsync()
        {
            var dormitories = await _repositoryManager.Dormitory.GetAllAsync()
                ?? throw new NotFoundException($"No Dormitory found.");

            return dormitories;
        }


        public async Task<Dormitory> GetDormitoryByIdAsync(int dormitoryId)
        {
            var dormitory = await _repositoryManager.Dormitory.GetByIdAsync(dormitoryId)
                ?? throw new NotFoundException($"Dormitory with ID '{dormitoryId}' not found.");
            

            return dormitory;
        }


        public async Task CreateDormitoryAsync(Dormitory dormitory)
        {
            // İş Mantığı: Burada aynı isimde başka bir yurt olup olmadığı kontrol edilebilir.
            // Örneğin: var existingDorm = await _repositoryManager.Dormitory.GetByNameAsync(dormitory.DormitoryName);
            // if (existingDorm is not null) throw new Exception("Dormitory with this name already exists.");

            await _repositoryManager.Dormitory.CreateAsync(dormitory);
        }


        public async Task UpdateDormitoryAsync(int dormitoryId, Dormitory dormitoryForUpdate)
        {
            var dormitory = await _repositoryManager.Dormitory.GetByIdAsync(dormitoryId)
                ?? throw new NotFoundException($"Dormitory with ID '{dormitoryId}' not found.");

            dormitory.DormitoryName = dormitoryForUpdate.DormitoryName;
            dormitory.City = dormitoryForUpdate.City;
            dormitory.Address = dormitoryForUpdate.Address;

            await _repositoryManager.Dormitory.UpdateAsync(dormitory);
        }


        public async Task DeleteDormitoryAsync(int dormitoryId)
        {
            var dormitory = await _repositoryManager.Dormitory.GetByIdAsync(dormitoryId)
                ?? throw new NotFoundException($"Dormitory with ID '{dormitoryId}' not found.");

            await _repositoryManager.Dormitory.DeleteAsync(dormitory.DormitoryId);
        }
    }
}
