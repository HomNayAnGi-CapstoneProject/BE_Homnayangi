using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Request
{
    public class CreateNewCronJobTimeConfig
    {
        [Range(0, 60,
             ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Minute { get; set; }
        [Range(0, 24,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Hour { get; set; }
        [Range(1, 31,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Day { get; set; }
        [Range(1, 12,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Month { get; set; }
        public int? TargetObject { get; set; }
    }
}
