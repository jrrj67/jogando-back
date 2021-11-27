using api.Data.Entities;
using System;

namespace api.Data.Constants
{
    public class Roles
    {
        public static RolesEntity Admin 
        { 
            get => new RolesEntity()
            { 
                Id = 1, 
                Name = "admin",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }; 
        }
    }
}
