using api.Data.Entities;
using System;

namespace api.Data.Constants
{
    public class Roles
    {
        public const string AdminName = "admin";

        public static RolesEntity Admin
        {
            get => new RolesEntity()
            {
                Id = 1,
                Name = AdminName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
