using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaugeter.Api.Jobs.Models
{
    public class Job
    {
        public int Id { get; set; }

        [ForeignKey("Device")]
        public string DeviceAddress { get; set; }

        [Required]
        public int Duration { get; set; }

        public Job() { /* Required by EF */ }
    }
}
