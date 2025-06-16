using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Exceptions;

namespace LaundrySystem.Services.Concrete
{
    public class LaundryService : ILaundryService
    {
        private readonly IRepositoryManager _repositoryManager;


        public LaundryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<Laundry>> GetLaundriesByDormitoryAsync(int dormitoryId)
        {
            var dormitory = await _repositoryManager.Dormitory.GetByIdAsync(dormitoryId)
                ?? throw new NotFoundException($"No Laundry found.");

            var laundries = await _repositoryManager.Laundry.GetLaundriesByDormitoryAsync(dormitory.DormitoryId)
                ?? throw new NotFoundException($"No Laundry found.");

            return laundries;
        }


        public async Task<Laundry> GetLaundryByIdAsync(int laundryId)
        {
            var laundry = await _repositoryManager.Laundry.GetByIdAsync(laundryId)
                ?? throw new NotFoundException($"Laundry with ID '{laundryId}' not found.");

            return laundry;
        }


        public async Task CreateLaundryAsync(Laundry laundry)
        {
            var dormitory = await _repositoryManager.Dormitory.GetByIdAsync(laundry.DormitoryId)
                ?? throw new NotFoundException($"No Laundry found.");

            var existingLaundries = await _repositoryManager.Laundry.GetLaundriesByDormitoryAsync(laundry.DormitoryId);

            if (existingLaundries.Any(l => l.LocationDescription.Equals(laundry.LocationDescription, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BadRequestException($"A laundry with the description '{laundry.LocationDescription}' already exists in this dormitory.");
            }

            await _repositoryManager.Laundry.CreateAsync(laundry);
        }


        public async Task UpdateLaundryAsync(int laundryId, Laundry laundryForUpdate)
        {
            var laundry = await _repositoryManager.Laundry.GetByIdAsync(laundryId)
                ?? throw new NotFoundException($"Laundry with ID '{laundryId}' not found.");

            laundry.LocationDescription = laundryForUpdate.LocationDescription;
            laundry.SessionDurationMinutes = laundryForUpdate.SessionDurationMinutes;
            laundry.Status = laundryForUpdate.Status;

            await _repositoryManager.Laundry.UpdateAsync(laundry);
        }


        public async Task DeleteLaundryAsync(int laundryId)
        {
            var laundry = await GetLaundryByIdAsync(laundryId)
                ?? throw new NotFoundException($"Laundry with ID '{laundryId}' not found.");

            await _repositoryManager.Laundry.DeleteAsync(laundry.LaundryId);
        }
    }
}