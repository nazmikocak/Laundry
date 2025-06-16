using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Exceptions;

namespace LaundrySystem.Services.Concrete
{
    public class MachineService : IMachineService
    {
        private readonly IRepositoryManager _repositoryManager;


        public MachineService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<Machine>> GetMachinesByLaundryAsync(int laundryId)
        {
            var laundry = await _repositoryManager.Laundry.GetByIdAsync(laundryId)
                ?? throw new NotFoundException($"Laundry with ID '{laundryId}' not found.");

            var machines = await _repositoryManager.Machine.GetMachinesByLaundryAsync(laundry.LaundryId)
                ?? throw new NotFoundException($"No Machine found.");

            return machines;
        }


        public async Task<Machine> GetMachineByIdAsync(int machineId)
        {
            var machine = await _repositoryManager.Machine.GetByIdAsync(machineId)
                ?? throw new NotFoundException($"Machine with ID '{machineId}' not found.");

            return machine;
        }


        public async Task CreateMachineAsync(Machine machine)
        {
            var laundry = await _repositoryManager.Laundry.GetByIdAsync(machine.LaundryId)
                ?? throw new NotFoundException($"Laundry with ID '{machine.LaundryId}' not found.");

            await _repositoryManager.Machine.CreateAsync(machine);
        }


        public async Task UpdateMachineAsync(int machineId, Machine machineForUpdate)
        {
            var machine = await _repositoryManager.Machine.GetByIdAsync(machineId)
                ?? throw new NotFoundException($"Machine with ID '{machineId}' not found.");

            machine.LaundryId = machineForUpdate.LaundryId;
            machine.MachineNumber = machineForUpdate.MachineNumber;
            machine.MachineType = machineForUpdate.MachineType;
            machine.Status = machineForUpdate.Status;

            await _repositoryManager.Machine.UpdateAsync(machine);
        }


        public async Task UpdateMachineStatusAsync(int machineId, MachineStatus status)
        {
            var machine = await _repositoryManager.Machine.GetByIdAsync(machineId)
                ?? throw new NotFoundException($"Machine with ID '{machineId}' not found.");

            await _repositoryManager.Machine.UpdateStatusAsync(machine.MachineId, status);
        }


        public async Task DeleteMachineAsync(int machineId)
        {
            var machine = await _repositoryManager.Machine.GetByIdAsync(machineId)
                ?? throw new NotFoundException($"Machine with ID '{machineId}' not found.");
                
            await _repositoryManager.Machine.DeleteAsync(machine.MachineId);
        }
    }
}
