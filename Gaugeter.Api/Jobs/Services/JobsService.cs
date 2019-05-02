using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Jobs.Services
{
    public class JobsService : IJobsService
    {
        private readonly IJobsRepository _jobsRepository;

        public JobsService(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public async Task<int?> Upsert(Job job)
        {
            return await _jobsRepository.Upsert(job);
        }

        public async Task<EntityState> Delete(int jobId)
        {
            return await _jobsRepository.Delete(jobId);
        }

        public async Task<Job> Get(int jobId)
        {
            return await _jobsRepository.Get(jobId);
        }

        public async Task<IEnumerable<Job>> GetByDate(long start, long end, string userId)
        {
            return await _jobsRepository.GetByDate(start, end, userId);
        }

        public async Task<Job> GetLast(string userId)
        {
            return await _jobsRepository.GetLast(userId);
        }
    }
}