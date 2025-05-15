using Microsoft.EntityFrameworkCore;
using SPA.Domain.Entities;
using SPA.Infrastructure.Context;
using SPA.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Infrastructure.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<UserViewModel> GetAll(string searchValue, int page, int pageSize, int sort, int isDeleted)
        {
            int TotalRecord = await _context.Users.Where(x => x.Dlt == false).CountAsync();
            var data = _context.Users.Where(x => x.Dlt == false).Include(x => x.phones).OrderByDescending(x => x.Id).AsQueryable();
            if (isDeleted != 0)
            {
                data = _context.Users.Include(x => x.phones).Where(x => x.Dlt == true).OrderByDescending(x => x.Id).AsQueryable();
                TotalRecord = await _context.Users.Where(x => x.Dlt == true).CountAsync();
            }
            if (sort == 22)
            {
                if (isDeleted != 0)
                {
                    data = _context.Users.Include(x => x.phones).Where(x => x.Dlt == true).AsQueryable();
                }
                else
                {
                    data = _context.Users.Include(x => x.phones).Where(x => x.Dlt == false).AsQueryable();
                }
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.Contains(searchValue) || x.Family.Contains(searchValue));
                TotalRecord = data.Count();
            }
            if (pageSize == -1)
            {
                return new UserViewModel
                {
                    UserList = await data.ToListAsync(),
                    pager = new Pager(TotalRecord, page, TotalRecord)
                };
            }
            return new UserViewModel
            {
                UserList = await data.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                pager = new Pager(TotalRecord, page, pageSize)
            };
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.Include(x => x.phones).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<UserViewModel> GetAllForExcel(int isDeleted)
        {
            var users = _context.Users.AsQueryable();
            if (isDeleted != 0)
            {
                users = users.Where(x => x.Dlt == true).OrderByDescending(x => x.Id);
            }
            else
            {
                users = users.Where(x => x.Dlt == false).OrderByDescending(x => x.Id);
            }
            return new UserViewModel
            {
                UserList = await users.ToListAsync()
            };
        }


        public void AddPhone(PhoneBook phone)
        {
            _context.PhoneBooks.Add(phone);
        }
        public void UpdatePhone(PhoneBook phone)
        {
            _context.PhoneBooks.Update(phone);
        }
        public void DeletePhone(PhoneBook phone)
        {
            _context.PhoneBooks.Remove(phone);
        }
        public PhoneBook GetPhoneBookById(int id)
        {
            var pn = _context.PhoneBooks.FirstOrDefault(x => x.PhonebookId == id);
            if (pn == null)
            {
                throw new NullReferenceException();
            }
            return pn;
        }
    }
}
