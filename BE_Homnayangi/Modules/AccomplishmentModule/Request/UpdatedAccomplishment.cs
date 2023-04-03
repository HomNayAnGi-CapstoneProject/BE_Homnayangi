using System;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Request
{
    public class UpdatedAccomplishment : CreatedAccomplishment
    {
        public Guid AccomplishmentId { get; set; }
    }
}
