using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Gaugeter.Api.Authentication.Services.UserInfoAccessor;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Models.Dto;
using Gaugeter.Api.Jobs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Jobs.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly IJobsService _jobsService;
        private readonly IMapper _mapper;
        private readonly IUserInfoAccessorService _userInfoAccessor;
        
        public JobsController(IJobsService jobsService, IMapper mapper, IUserInfoAccessorService userInfoAccessor)
        {
            _jobsService = jobsService;
            _mapper = mapper;
            _userInfoAccessor = userInfoAccessor;
        }
        
        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody][Required] JobDto jobDto)
        {
            var job = _mapper.Map<JobDto, Job>(jobDto);

            job.UserId = _userInfoAccessor.GetUserId();
            
            var jobId = await _jobsService.Upsert(job);
            
            if (jobId == null)
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            
            return Ok(new { id = job});
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery][Required] int jobId)
        {
            if (await _jobsService.Delete(jobId) == EntityState.Deleted)
                return Ok();

            return NoContent();
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int jobId)
        {
            var job = await _jobsService.Get(jobId);

            if (job == null)
                return NoContent();

            return Ok(_mapper.Map<Job, JobDto>(job));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByDate([FromQuery][Required] long start, long end)
        {
            var jobs = await _jobsService.GetByDate(start, end, _userInfoAccessor.GetUserId());
    
            if (jobs == null)
                return NoContent();

            return Ok(_mapper.Map<IEnumerable<Job>, IEnumerable<JobDto>>(jobs));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetLast()
        {
            var job = await _jobsService.GetLast(_userInfoAccessor.GetUserId());
    
            if (job == null)
                return NoContent();

            return Ok(_mapper.Map<Job, JobDto>(job));
        }
    }
}