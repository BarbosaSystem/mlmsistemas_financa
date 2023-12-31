using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using app.Models.Identity;
using app.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace app.Contexto
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = ADMIN_ID;
            
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            });

            ApplicationUser appUser = new ApplicationUser
            {
                Id = ROLE_ID,
                UserName = "admin.admin",
                NormalizedUserName = "admin.admin",
                Email = "admin@mlmsistemas.com",
                FullName = "Admin MLM Sistemas",
                photoUrl = "user.png",
                SecurityStamp = Guid.NewGuid().ToString()
                
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = hasher.HashPassword(appUser, "mlmsistemas");
            modelBuilder.Entity<ApplicationUser>().HasData(
                appUser
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abcdef";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new
                    Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}