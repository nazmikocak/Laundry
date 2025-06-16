using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IMachineService
    {
        Task<IEnumerable<Machine>> GetMachinesByLaundryAsync(int laundryId);

        Task<Machine> GetMachineByIdAsync(int machineId);

        Task CreateMachineAsync(Machine machine);

        Task UpdateMachineAsync(int machineId, Machine machineForUpdate);

        Task UpdateMachineStatusAsync(int machineId, MachineStatus status);

        Task DeleteMachineAsync(int machineId);
    }
}