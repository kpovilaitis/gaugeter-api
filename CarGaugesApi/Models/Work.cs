namespace CarGaugesApi.Models
{
    public class Work
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public int Duration { get; set; }

        public Work() { /* Required by EF */ }

        //public Work(int id, int duration) {
        //    DeviceId = id;
        //    Duration = duration;
        //}
    }
}
