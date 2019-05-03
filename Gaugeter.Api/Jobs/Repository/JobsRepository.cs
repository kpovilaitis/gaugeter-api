using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Data;
using Gaugeter.Api.Jobs.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Jobs.Repository
{
    public class JobsRepository : IJobsRepository
    {
        private readonly GaugeterDbContext _context;

        public JobsRepository(GaugeterDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Upsert(Job job)
        {
            try
            {
                _context.Entry(job).State = job.Id == 0 ? EntityState.Added : EntityState.Modified;

                foreach (var telem in job.Telem)
                {
                    job.Telem.ToList().Add(telem);
                    _context.Entry(telem).State = EntityState.Added;
                }

                await _context.SaveChangesAsync();

                return job.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntityState> Delete(int jobId)
        {
            var jobEntity = await _context.Job
                .Include(j => j.Telem)
                .SingleOrDefaultAsync(j => j.Id == jobId);

            if (jobEntity == null)
                return EntityState.Unchanged;

            _context.TelemData.RemoveRange(jobEntity.Telem);    
            _context.Entry(jobEntity).State = EntityState.Deleted;
            
            var state = _context.Job.Remove(jobEntity).State;

            await _context.SaveChangesAsync();

            return state;
        }

        public async Task<Job> Get(int jobId)
        {
            return await _context.Job
                .Select(j => new Job
                {
                    Id = j.Id,
                    State = j.State,
                    UserId = j.UserId,
                    Device = j.Device,
                    Telem = _context.TelemData
                        .Where(t => t.JobId == j.Id)
                        .OrderBy(t => t.Id)
                        .Take(GaugeterConstants.MIN_JOB_TELEM_COUNT),
                    DateCreated = j.DateCreated,
                    DateUpdated = j.DateUpdated
                    
                })
                .Where(j => j.State == Enums.JOB_STATE.Completed)
                .SingleOrDefaultAsync(j => j.Id == jobId);
        }

        public async Task<IEnumerable<Job>> GetByDate(long start, long end, string userId)
        {
            return await _context.Job
                .Where(j => j.UserId == userId)
                .Where(j => j.DateCreated >= start)
                .Where(j => j.DateCreated <= end)
                .Where(j => j.State == Enums.JOB_STATE.Completed)
                .ToListAsync();
        }

        public async Task<Job> GetLast(string userId)
        {
            return await _context.Job
                .Select(j => new Job
                {
                    Id = j.Id,
                    State = j.State,
                    UserId = j.UserId,
                    Device = j.Device,
                    Telem = _context.TelemData.Where(t => t.JobId == j.Id).OrderBy(t => t.Id).Take(1000),
                    DateCreated = j.DateCreated,
                    DateUpdated = j.DateUpdated
                    
                })
                .Where(j => j.UserId == userId)
                .Where(j => j.State == Enums.JOB_STATE.Completed)
                .LastAsync(j => j.UserId == userId);
        }
    }
}