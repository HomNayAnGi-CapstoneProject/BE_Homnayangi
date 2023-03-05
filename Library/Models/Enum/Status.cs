using System;
namespace Library.Models.Enum
{
    public static class Status
    {
        public enum BlogStatus
        {
            DELETED = 0,
            ACTIVE = 1,
            DRAFTED = 2,
            PENDING = 3
        }

        public enum OrderStatus
        {
            DELETED = 0,
            PENDING = 1,
            ACCEPTED = 2,
            CANCEL = 3, // by user
            DENIED = 4, // by staff
            PAID = 5,
            SHIPPING = 6,
            DELIVERED = 7,
            DELIVERED_FAIL = 8
        }

        public enum TransactionStatus
        {
            DELETED = 0,
            PENDING = 1,
            SUCCESS = 2,
            FAIL = 3
        }
    }
}

