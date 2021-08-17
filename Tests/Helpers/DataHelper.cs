using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;

namespace Tests.Helpers
{
    public static class DataHelper
    {
        public static User GetUser(string name)
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordSalt, out var passwordHash);

            return new User()
            {
                UserId = 1,
                Address = "test",
                BirthDate = new DateTime(1988, 01, 01),
                CitizenId = 12345678910,
                Email = "test@test.com",
                FullName = string.Format("{0} {1} {2}", name, name, name),
                Gender = 1,
                MobilePhones = "05339262726",
                Notes = "test",
                RecordDate = DateTime.Now,
                PasswordHash = passwordSalt,
                PasswordSalt = passwordHash,
                Status = true,
                AuthenticationProviderType = "Person",
                UpdateContactDate = DateTime.Now
            };
        }

        public static List<User> GetUserList()
        {
            HashingHelper.CreatePasswordHash("123456", out var passwordSalt, out var passwordHash);
            var list = new List<User>();

            for (var i = 1; i <= 5; i++)
            {
                var user = new User()
                {
                    UserId = i,
                    Address = "test" + i,
                    BirthDate = new DateTime(1988, 01, 01),
                    CitizenId = 123456789101,
                    Email = "test@test.com",
                    FullName = string.Format("name {0} name {1} name {2}", i, i, i),
                    Gender = 1,
                    MobilePhones = "123456789",
                    Notes = "test",
                    RecordDate = DateTime.Now,
                    PasswordHash = passwordSalt,
                    PasswordSalt = passwordHash,
                    Status = true,
                    AuthenticationProviderType = "User",
                    UpdateContactDate = DateTime.Now
                };
                list.Add(user);
            }

            return list;
        }
    }
}