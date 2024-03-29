﻿using System;
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
            SHIPPING = 5,
            DELIVERED = 6,
            DELIVERED_FAIL = 7,
            REFUND = 8,
            PAYING = 9,
            NEED_REFUND = 10
        }

        public enum TransactionStatus
        {
            DELETED = 0,
            PENDING = 1,
            SUCCESS = 2,
            FAIL = 3
        }
        public enum BadgeStatus
        {
            DELETED = 0,
            ACTIVE = 1,
            PENDING = 2
        }

        public enum AccomplishmentStatus
        {
            DEACTIVE = 0,
            ACTIVE = 1,
            DRAFTED = 2,
            PENDING = 3
        }

        public enum PriceNote
        {
            DEACTIVE = 0,
        }
        public enum BadgeCondition
        {
            DEACTIVE = 0,
            ACTIVE = 1
        }
    }
}

