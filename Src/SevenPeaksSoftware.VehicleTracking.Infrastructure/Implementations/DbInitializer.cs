using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class DbInitializer : IDbInitializer
    {
        private readonly VehicleTrackingDbContext _dbContext;

        public DbInitializer(VehicleTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            _dbContext.Database.Migrate();
        }

        public void Seed()
        {

            if (! _dbContext.UserRoles.Any())
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: "QAZwsx123",
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                _dbContext.UserRoles.AddRange(new List<UserRoleModel>
                {
                    new UserRoleModel
                    {
                        RoleInfo = new RoleModel()
                        {
                            RoleName = "Admin"
                        },
                        UserInfo = new UserModel()
                        {
                            FirstName = "autoSeedFirstName",
                            LastName = "autoSeedLastName",
                            Username = "Admin",
                            Salt = salt,
                            Password = hashedPassword
                        }
                    }
                });
                       
            }
           
            _dbContext.SaveChanges();
        }
    }
}
    
