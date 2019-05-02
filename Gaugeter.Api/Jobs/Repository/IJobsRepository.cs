using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Jobs.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Jobs.Repository
{
    public interface IJobsRepository
    {
        Task<int?> Upsert(Job job);
        
        Task<EntityState> Delete(int jobId);
        
        Task<Job> Get(int jobId);
        
        Task<IEnumerable<Job>> GetByDate(long start, long end, string userId);
        
        Task<Job> GetLast(string userId);
    }
}