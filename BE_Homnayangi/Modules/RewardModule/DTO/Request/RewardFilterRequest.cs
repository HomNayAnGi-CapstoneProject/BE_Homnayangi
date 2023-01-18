using System;
using System.ComponentModel.DataAnnotations;
using Library.Models.Enum;
using Library.PagedList;

namespace BE_Homnayangi.Modules.RewardModule.DTO.Request
{
    public class RewardFilterRequest : PagedRequest
    {
        [Range(0,99, ErrorMessage = "Range from 0 to 99")]
        public int conditionType { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? fromDate { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? toDate { get; set; }

        public RewardFilterRequest():base()
        {
            conditionType = (int) RewardEnum.ConditionType.REACTION;
        }
    }
}

