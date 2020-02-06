using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories.Models;

namespace EMS.HighSchool.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<bool> BulkInsert(List<User> users);
        Task<User> Get(UserFilter userFilter);
        Task<bool> Delete(long Id);
        Task<bool> ChangePassword(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly EMSContext context;
        public UserRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<UserDAO> DynamicFilter(IQueryable<UserDAO> query, UserFilter userFilter)
        {
            if (!string.IsNullOrEmpty(userFilter.Username))
            {
                query = query.Where(u => u.Username.Equals(userFilter.Username));
            }
            if (userFilter.IsAdmin.HasValue)
            {
                query = query.Where(u => u.IsAdmin.Equals(userFilter.IsAdmin.Value));
            }
            return query;
        }

        public async Task<bool> Create(User user)
        {
            UserDAO userDAO = new UserDAO
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Username = user.Username,
                Password = user.Password,
                StudentId = user.StudentId,
            };

            context.User.Add(userDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkInsert(List<User> users)
        {
            try
            {
                List<UserDAO> userDAOs = users.Select(user => new UserDAO()
                {
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Username = user.Username,
                    Password = user.Password,
                    StudentId = user.StudentId,
                }).ToList();

                context.User.AddRange(userDAOs);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(long Id)
        {
            await context.User.Where(g => g.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<User> Get(UserFilter userFilter)
        {
            IQueryable<UserDAO> users = context.User;
            UserDAO userDAO = await DynamicFilter(users, userFilter).FirstOrDefaultAsync();
            User user = null;
            if (userDAO == null) return user;
            else
            {
                user = new User()
                {
                    Id = userDAO.Id,
                    Username = userDAO.Username,
                    Password = userDAO.Password,
                    Salt = userDAO.Salt,
                    IsAdmin = userDAO.IsAdmin,
                    StudentId = userDAO.StudentId,
                };
            }

            return user;
        }

        public async Task<bool> ChangePassword(User user)
        {
            await context.User.Where(u => u.Id == user.Id).UpdateFromQueryAsync(u => new UserDAO
            {
                Password = user.Password,
                Salt = user.Salt
            });

            return true;
        }
    }
}
