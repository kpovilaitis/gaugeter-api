using System;
using System.Collections.Generic;
using AutoMapper;
using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Authentication.Models.Dto;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Devices.Models.Dto;
using Gaugeter.Api.Helpers.HashGenerator;
using Gaugeter.Api.Jobs.Models.Data;
using Gaugeter.Api.Jobs.Models.Dto;
using Gaugeter.Api.Users.Controllers;
using Gaugeter.Api.Users.Models.Data;
using Gaugeter.Api.Users.Models.Dto;
using Gaugeter.Api.Users.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Gaugeter.Api.Tests.Users
{
    public class UsersControllerTest
    {   
        public static List<object[]> CreateUserList = new List<object[]> {
            new object[] { new UserDto
            {
                UserId = "Ingrida2", 
                Password = "password", 
                Description = "good gal2", 
                Devices = null,
                MeasurementSystem = Enums.MEASUREMENT_SYSTEM.Metric
            }, typeof(CreatedAtActionResult) }
        };

        public static List<object[]> UpdateUserList = new List<object[]> {
            new object[] { new UserDto{
                UserId = "Ingrida2", 
                Password = "password", 
                Description = "good gal2", 
                Devices = null,
                MeasurementSystem = Enums.MEASUREMENT_SYSTEM.Metric
            }, typeof(CreatedAtActionResult) },
            new object[] { new UserDto{
                UserId = "blablabla", 
                Password = "password", 
                Description = "good gal2", 
                Devices = null,
                MeasurementSystem = Enums.MEASUREMENT_SYSTEM.Metric
            }, typeof(BadRequestObjectResult) }
        };

        private readonly UsersController _usersController;

        public UsersControllerTest()
        {
            var userRepository = new UsersRepositoryTest();
            var userService = new UsersService(userRepository, new HashGenerator());
            
            _usersController = new UsersController(userService, new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>()
                        .IncludeAllDerived()
                        .AfterMap((src, dest) => { dest.Password = null; });

                    cfg.CreateMap<Login, LoginDto>();

                    cfg.CreateMap<DeviceDto, Device>(MemberList.Source);

                    cfg.CreateMap<JobDto, Job>(MemberList.Source);
                    cfg.CreateMap<TelemDataDto, TelemData>(MemberList.Source);
                })));
        }

        [Fact]
        public async void CanGetAllUsers()
        {
            var test5 = await _usersController.GetAll();
            
            Assert.True(test5.GetType() == typeof(OkObjectResult), $"Wrong type. {test5.GetType()} is not equal to {typeof(OkObjectResult)}");
        }

        [Theory]
        [InlineData("Ingrida2", typeof(OkObjectResult))]
        [InlineData("aaaa", typeof(NoContentResult))]
        public async void CanGetUser(string id, Type type)
        {
            var test1 = await _usersController.Get(id);
            
            Assert.True(test1.GetType() == type, $"Wrong type. {test1.GetType()} is not equal to {type}");
        }

        [Theory]
        [MemberData(nameof(CreateUserList))]
        public async void CanCreateUser(UserDto user, Type type)
        {
            var test2 = await _usersController.Create(user);
            
            Assert.True(test2.GetType() == type, $"Wrong type. {test2.GetType()} is not equal to {type}");
        }
        
        [Theory]
        [InlineData("Ingrida2", typeof(OkResult))]
        [InlineData("aaaa", typeof(NoContentResult))]
        public async void CanDeleteUser(string id, Type type)
        {
            var test2 = await _usersController.Delete(id);
            
            Assert.True(test2.GetType() == type, $"Wrong type. {test2.GetType()} is not equal to {type}");
        }

        [Theory]
        [MemberData(nameof(UpdateUserList))]
        public async void CanUpdateUser(UserDto user, Type type)
        {
            var test2 = await _usersController.Update(user);
            
            Assert.True(test2.GetType() == type, $"Wrong type. {test2.GetType()} is not equal to {type}");
        }
    }
}
