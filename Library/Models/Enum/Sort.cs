using System;
namespace Library.Models.Enum
{
    public static class Sort
    {
        public enum SortBy
        {
            CREATEDDATE = 1,
            REACTION = 2,
            VIEW = 3
        }
        public enum SortOrder
        {
            ASC = 1,
            DESC = 2
        }
    }
}

