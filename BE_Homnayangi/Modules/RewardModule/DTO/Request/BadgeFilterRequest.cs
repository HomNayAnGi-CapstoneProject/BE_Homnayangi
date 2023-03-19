using System;
using System.ComponentModel.DataAnnotations;
using Library.Models.Enum;
using Library.PagedList;

namespace BE_Homnayangi.Modules.BadgeModule.DTO.Request
{
    public class BadgeFilterRequest : PagedRequest
    {
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? fromDate { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? toDate { get; set; }
    }
}

