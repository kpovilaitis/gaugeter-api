using static CarGaugesApi.Constants.Enums;

namespace CarGaugesApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public MEASUREMENT_SYSTEM MeasurementSystem { get; set; }

        public string Token { get; set; }

        public User() { }

        //public User(int id, string username, string password, string description, MEASUREMENT_SYSTEM mesSystem, string token)
        //{
        //    Id = id;
        //    Username = username;
        //    Password = password;
        //    Description = description;
        //    MeasurementSystem = mesSystem;
        //    Token = token;
        //}
    }
}
