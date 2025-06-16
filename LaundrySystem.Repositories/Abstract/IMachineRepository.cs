using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;

namespace LaundrySystem.Repositories.Abstract
{
    public interface IMachineRepository : IRepositoryBase<Machine>
    {
        Task<IEnumerable<Machine>> GetMachinesByLaundryAsync(int laundryId);

        Task UpdateStatusAsync(int machineId, MachineStatus status);
    }
}