using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository.DevicesRepo
{
    public interface IDevicesRepository
    {
        Task<Device> GetDevice(string bluetoothAddress);

        Task<EntityState> CreateDevice(Device device);

        Task<EntityState> AddDeviceToUser(string userId, Device device);
    }
}
