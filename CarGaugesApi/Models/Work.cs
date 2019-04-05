using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarGaugesApi.Models
{
    public class Work
    {
        public int Id { get; set; }

        [ForeignKey("Device")]
        public string DeviceAddress { get; set; }

        [Required]
        public int Duration { get; set; }

        public Work() { /* Required by EF */ }

        //public Work(int id, int duration) {
        //    DeviceId = id;
        //    Duration = duration;
        //}
    }
}
