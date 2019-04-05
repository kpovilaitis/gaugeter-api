namespace CarGaugesApi.Models
{
    public class UserDevice
    {
        public UserDevice() { }

        public string UserId { get; set; }
        public User User { get; set; }

        public string DeviceAddress { get; set; }
        public Device Device { get; set; }
    }
}
