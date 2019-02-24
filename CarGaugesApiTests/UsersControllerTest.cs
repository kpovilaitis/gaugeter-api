using System;
using System.Collections.Generic;
using CarGaugesApi.Controllers;
using CarGaugesApi.Helpers;
using CarGaugesApi.Models;
using CarGaugesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace CarGaugesApiTests
{
    public class UsersControllerTest
    {   
        public static List<object[]> createUserList = new List<object[]>() {
            new object[] { new User(3, "Ingrida2", "password", "good gal2", null), typeof(CreatedAtActionResult) },
            new object[] { new User(), typeof(BadRequestObjectResult) },
            new object[] { null, typeof(BadRequestObjectResult) }
        };

        public static List<object[]> updateUserList = new List<object[]>() {
            new object[] { new User(3, "Ingrida2", "password", "good gal2", null), typeof(CreatedAtActionResult) },
            new object[] { new User(300, "Ingrida2", "password", "good gal2", null), typeof(BadRequestObjectResult) },
            new object[] { new User(), typeof(BadRequestObjectResult) },
            new object[] { null, typeof(BadRequestObjectResult) }
        };

        public static List<object[]> deleteUserList = new List<object[]>() {
            new object[] { new User(3, "Ingrida2", "password", "good gal2", null), typeof(OkObjectResult) },
            new object[] { new User(300, "Ingrida2", "password", "good gal2", null), typeof(BadRequestObjectResult) },
            new object[] { new User(), typeof(BadRequestObjectResult) },
            new object[] { null, typeof(BadRequestObjectResult) }
        };

        public static List<object[]> tokenList = new List<object[]> {
            new object[] { new User (3, "Ingrida2", "password", "good gal2", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJuYmYiOjE1NDQzNjg5MDgsImV4cCI6MTU0NDk3MzcwOCwiaWF0IjoxNTQ0MzY4OTA4fQ.7Ln0q73_WaO7SyF2YXFbg_uxtdQj3nEC1nJ9CleVkMY") },
            new object[] { new User (4, "Kestutis", "password", "good boi", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJuYmYiOjE1NDQzNjg5NDUsImV4cCI6MTU0NDk3Mzc0NSwiaWF0IjoxNTQ0MzY4OTQ1fQ.tF1ThHm1337MNteT1sZ5h3Rel_JJQHWD87zcRxiacYU") }
        };

        private readonly UsersController _usersController;

        public UsersControllerTest()
        {
            var userRepository = new UsersRepositoryTest();
            var userService = new UsersService(userRepository, Options.Create(new AppSettings()));
            _usersController = new UsersController(userService);
        }

        [Fact]
        public void CanGetAllUsers()
        {
            IActionResult test5 = _usersController.GetAllUsers();
            Assert.True(test5.GetType() == typeof(OkObjectResult), $"Wrong type. {test5.GetType()} is not equal to {typeof(OkObjectResult)}");
        }

        [Theory]
        [InlineData(3, typeof(OkObjectResult))]
        [InlineData(-1, typeof(NotFoundResult))]
        public void CanGetUser(int id, Type type)
        {
            IActionResult test1 = _usersController.GetUser(id);
            Assert.True(test1.GetType() == type, $"Wrong type. {test1.GetType()} is not equal to {type}");
        }

        [Theory]
        [MemberData(nameof(createUserList))]
        public void CanCreateUser(User user, Type type)
        {
            IActionResult test2 = _usersController.CreateUser(user);
            Assert.True(test2.GetType() == type, $"Wrong type. {test2.GetType()} is not equal to {type}");
        }

        [Theory]
        [MemberData(nameof(updateUserList))]
        public void CanUpdateUser(User user, Type type)
        {
            IActionResult test2 = _usersController.UpdateUser(user);
            Assert.True(test2.GetType() == type, $"Wrong type. {test2.GetType()} is not equal to {type}");
        }

        [Theory]
        [MemberData(nameof(tokenList))]
        public void CanGenerateToken(User user)
        {
            var userAuthenticated = _usersController._usersService.Authenticate(user.Username, user.Password);
            Assert.True(user.Token == userAuthenticated.Token, $"Generated incorrect token. Expected '{user.Token}' but got '{userAuthenticated.Token}'");
        }
    }
}
