using System.Collections.Generic;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Dto;

namespace Gaugeter.Api.Jobs.Models.Dto
{
    public class JobDto
    {
        public int Id { get; set; }

        public DeviceDto Device { get; set; }

        public Enums.JOB_STATE State { get; set; }
        
        public long DateCreated { get; set; }
        
        public long DateUpdated { get; set; }
        
        public IEnumerable<TelemDataDto> TelemData { get; set; }

        public JobDto() { }
    }
}