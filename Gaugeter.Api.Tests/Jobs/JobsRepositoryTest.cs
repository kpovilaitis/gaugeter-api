using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Tests.Jobs
{
    public class JobsRepositoryTest : IJobsRepository
    {
        public Task<int?> Upsert(Job job)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntityState> Delete(int jobId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Job> Get(int jobId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetByDate(long start, long end, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Job> GetLast(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}