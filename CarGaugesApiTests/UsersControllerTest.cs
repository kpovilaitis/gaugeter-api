using System.Threading.Tasks;
using CarGaugesApi.Controllers;
using CarGaugesApi.Helpers;
using CarGaugesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace CarGaugesApiTests
{
    public class UsersControllerTest
    {
        private readonly UsersController _usersController;

        public UsersControllerTest()
        {
            var userRepository = new UsersRepositoryTest();
            var userService = new UsersService(userRepository, Options.Create(new AppSettings()));
            _usersController = new UsersController(userService);
        }

        /*[Fact]
        public void ReturnGetUser()
        {
            IActionResult test5 = _usersController.GetAllUsers();

            Assert.True(test5.GetType() == typeof(OkObjectResult), $"Wrong type. {test5.GetType()} is not equal to {typeof(OkObjectResult)}");
        }*/
    }
}
