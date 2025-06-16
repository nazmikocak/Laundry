using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class MachineRepository : RepositoryBase<Machine>, IMachineRepository
    {
        public MachineRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<Machine>> GetAllAsync()
        {
            throw new NotImplementedException("sp_GetAllMachines stored procedure is not defined.");
        }



        public async Task<IEnumerable<Machine>> GetMachinesByLaundryAsync(int laundryId)
        {
            var machines = new List<Machine>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetMachinesByLaundry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@LaundryId", laundryId);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                machines.Add(MachineMapper.MapToMachine(reader));
            }

            return machines;
        }



        public override async Task<Machine?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetMachineById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@MachineId", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MachineMapper.MapToMachine(reader);
            }

            return null;
        }



        public override async Task CreateAsync(Machine entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_CreateMachine", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            MachineMapper.AddMachineParameters(command, entity);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task UpdateAsync(Machine entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_UpdateMachine", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@MachineId", entity.MachineId);
            MachineMapper.AddMachineParameters(command, entity);

            await command.ExecuteNonQueryAsync();
        }



        public async Task UpdateStatusAsync(int machineId, MachineStatus status)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_UpdateMachineStatus", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@MachineId", machineId);
            command.Parameters.AddWithValue("@Status", status.ToString());

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_DeleteMachine", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@MachineId", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}