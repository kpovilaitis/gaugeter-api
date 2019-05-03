using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Data;

namespace Gaugeter.Api.Jobs.Models.Data
{
    public class Job
    {
        public int Id { get; set; }

        public Device Device { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }

        public Enums.JOB_STATE State { get; set; }
        
        public long DateCreated { get; set; }
        
        public long DateUpdated { get; set; }
        
        public IEnumerable<TelemData> TelemData { get; set; }

        public Job() { }
    }
}
