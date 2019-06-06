using System.ComponentModel.DataAnnotations.Schema;

namespace Gaugeter.Api.Jobs.Models.Data
{
    public class TelemData
    {
        public int Id { get; set; }
        
        [ForeignKey("Job")]
        public int JobId { get; set; }
        
        public float OilTemperature { get; set; }
        
        public float OilPressure { get; set; }
        
        public float WaterTemperature { get; set; }
        
        public float Charge { get; set; }
        
        public long DateCreated { get; set; }
    }
}