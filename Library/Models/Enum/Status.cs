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
            CART = 1,
            PENDING = 2,
            ACCEPTED = 3,
            CANCEL = 4, // by user
            DENIED = 5, // by staff
            SHIPPING = 6,
            DELIVERED = 7,
            DELIVERED_FAIL = 8,
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

