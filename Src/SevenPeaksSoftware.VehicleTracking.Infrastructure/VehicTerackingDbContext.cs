
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.Enums;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;


namespace SevenPeaksSoftware.VehicleTracking.Infrastructure
{
    public class VehicleTrackingDbContext : DbContext
    {
        public VehicleTrackingDbContext(DbContextOptions<VehicleTrackingDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<VehicleTrackModel> VehicleTracks { get; set; }

        /// <summary>
        /// change cluster index to Id and IsDeleted
        /// because of its usage in queries
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region User

            modelBuilder.Entity<UserModel>()
                .HasKey(k => k.UserId)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.UserId).UseSqlServerIdentityColumn();

            modelBuilder.Entity<UserModel>()
                .HasIndex(i => new { i.UserId, i.IsDeleted })
                .ForSqlServerIsClustered();


            modelBuilder.Entity<UserModel>()
                .Property(p => p.FirstName).IsRequired();

            modelBuilder.Entity<UserModel>()
                .Property(p => p.FirstName).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .FirstNameMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.LastName).IsRequired();

            modelBuilder.Entity<UserModel>()
                .Property(p => p.LastName).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .LastNameMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Username).IsRequired();

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Username).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .UsernameMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Password).IsRequired();

          

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Password).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .HashPasswordNumberMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.RefreshToken).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .HashPasswordNumberMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Email).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .EmailMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .Property(p => p.MobileNumber).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .MobileNumberMaxCharLength);

            modelBuilder.Entity<UserModel>()
                .HasMany(e => e.UserRoleList)
                .WithOne(e => e.UserInfo)
                .HasForeignKey(k => k.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            #endregion


            #region Role

            modelBuilder.Entity<RoleModel>()
                .HasKey(k => k.RoleId)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<RoleModel>()
                .Property(p => p.RoleId).UseSqlServerIdentityColumn();
                
            modelBuilder.Entity<RoleModel>()
                .HasIndex(i => new { i.RoleId, i.IsDeleted })
                .ForSqlServerIsClustered();

            modelBuilder.Entity<RoleModel>()
                .Property(p => p.RoleName).IsRequired();

            modelBuilder.Entity<RoleModel>()
                .Property(p => p.RoleName).
                HasMaxLength((int)ModelRestrictionsEnum.RoleRestrictionsEnum
                    .RoleNameMaxCharLength);

            modelBuilder.Entity<RoleModel>()
                .HasMany(e => e.UserRoleList)
                .WithOne(e => e.RoleInfo)
                .HasForeignKey(k => k.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);



            #endregion


            #region UserRole

            modelBuilder.Entity<UserRoleModel>()
                .HasKey(k => k.UserRoleId)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<UserRoleModel>()
                .Property(p => p.UserRoleId).UseSqlServerIdentityColumn();



            #endregion


            #region Vehicle

            modelBuilder.Entity<VehicleModel>()
                .HasKey(k => k.VehicleId)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.VehicleId).UseSqlServerIdentityColumn();

            modelBuilder.Entity<VehicleModel>()
                .HasIndex(i => new { i.VehicleId, i.IsDeleted })
                .ForSqlServerIsClustered();


            modelBuilder.Entity<VehicleModel>()
                .HasMany(e => e.VehicleTrackList)
                .WithOne(e => e.VehicleInfo)
                .HasForeignKey(k => k.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.VehicleRegistrationNumber).IsRequired();

            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.VehicleRegistrationNumber).
                HasMaxLength((int)ModelRestrictionsEnum.VehicleRestrictionsEnum
                    .VehicleRegistrationNumberMaxCharLength);

            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.Password).IsRequired();

            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.Password).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .HashPasswordNumberMaxCharLength);


            modelBuilder.Entity<VehicleModel>()
                .Property(p => p.RefreshToken).
                HasMaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum
                    .RefreshTokenMaxCharLength);



            #endregion


            #region VehicleTrack

            modelBuilder.Entity<VehicleTrackModel>()
                .HasKey(k => k.VehicleTrackId)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<VehicleTrackModel>()
                .Property(p => p.VehicleTrackId).UseSqlServerIdentityColumn();


            #endregion


        }
    }
}
