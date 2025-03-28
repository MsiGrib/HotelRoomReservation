﻿using DataModel.DataBase;
using Microsoft.EntityFrameworkCore;

namespace IdentityMService.EntityGateWay
{
    public class IdentityDBContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<UserProfileDTO> UsersProfile { get; set; }

        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BindingTable(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityClrType = entityType.ClrType;
                var entityInterface = entityClrType.GetInterface("IEntity`1");

                if (entityInterface != null)
                {
                    var idProperty = entityType.FindProperty("Id");
                    if (idProperty != null)
                        ConfigureIdProperty(modelBuilder, entityClrType, idProperty.ClrType);
                }
            }
        }

        private void BindingTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDTO>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfileDTO>(p => p.UserId)
                .IsRequired();
        }

        private void ConfigureIdProperty(ModelBuilder modelBuilder, Type entityType, Type idType)
        {
            if (idType == typeof(long))
            {
                modelBuilder.Entity(entityType)
                    .Property("Id")
                    .ValueGeneratedOnAdd();
            }
            else if (idType == typeof(Guid))
            {
                modelBuilder.Entity(entityType)
                    .Property("Id")
                    .HasDefaultValueSql("gen_random_uuid()");
            }
        }
    }
}
