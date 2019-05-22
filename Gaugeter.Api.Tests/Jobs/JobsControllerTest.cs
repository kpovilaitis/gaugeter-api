using System.Collections.Generic;
using AutoMapper;
using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Authentication.Models.Dto;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Devices.Models.Dto;
using Gaugeter.Api.Jobs.Controllers;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Models.Dto;
using Gaugeter.Api.Jobs.Services;
using Gaugeter.Api.Users.Models.Data;
using Gaugeter.Api.Users.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Gaugeter.Api.Tests.Jobs
{
    public class JobsControllerTest
    {
        public static List<object[]> CreateUserList = new List<object[]> {
            new object[] { new JobDto
            {
                Id = 1,
                State = Enums.JOB_STATE.Ongoing,
                Device = null,
                TelemData = new List<TelemDataDto>(),
                DateCreated = 1,
                DateUpdated = 1
            }, typeof(CreatedAtActionResult) }
        };

        public static List<object[]> UpdateUserList = new List<object[]> {
            new object[] { new JobDto
            {
                Id = 1,
                State = Enums.JOB_STATE.Ongoing,
                Device = null,
                TelemData = new List<TelemDataDto>(),
                DateCreated = 2,
                DateUpdated = 2
            }, typeof(CreatedAtActionResult) },
            new object[] { new JobDto
            {
                Id = 2,
                State = Enums.JOB_STATE.Completed,
                Device = null,
                TelemData = new List<TelemDataDto>(),
                DateCreated = 2,
                DateUpdated = 2
            }, typeof(BadRequestObjectResult) }
        };
        
        private readonly JobsController _jobsController;

        public JobsControllerTest()
        {
            var jobsRepository = new JobsRepositoryTest();
            var userService = new JobsService(jobsRepository);
            
            _jobsController = new JobsController(userService, new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                    .IncludeAllDerived()
                    .AfterMap((src, dest) => { dest.Password = null; });

                cfg.CreateMap<Login, LoginDto>();

                cfg.CreateMap<DeviceDto, Device>(MemberList.Source);

                cfg.CreateMap<JobDto, Job>(MemberList.Source);
                cfg.CreateMap<TelemDataDto, TelemData>(MemberList.Source);
            })), null);
        }
    }
}