using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.TypeModule.DTO
{
    public class CreateTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

    }
}
