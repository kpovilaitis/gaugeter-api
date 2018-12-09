namespace CarGaugesApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public string Token { get; set; }

        public User (int id, string username, string password, string description, string token)
        {
            Id = id;
            Username = username;
            Password = password;
            Description = description;
            Token = token;
        }
    }
}
