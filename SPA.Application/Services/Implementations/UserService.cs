using ClosedXML.Excel;
using SPA.Application.Services.Interfaces;
using SPA.Domain.Common;
using SPA.Domain.Dtoes;
using SPA.Domain.Entities;
using SPA.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserViewModel> GetAll(string searchValue, int page, int pageSize, int sort, int isDeleted)
        {
            return await _userRepository.GetAll(searchValue, page, pageSize, sort, isDeleted);
        }
        public async Task AddUser(CreateUserDto userDto)
        {
            var user = new User()
            {
                Name = userDto.Name,
                Family = userDto.Family,
                Age = userDto.Age,
                Address = userDto.Address,
                Brithday = userDto.Brithday,
            };
            await _userRepository.AddUser(user);
            await _userRepository.Save();
        }

        public async Task<DetailsUserDto> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            return new DetailsUserDto
            {
                Address = user.Address,
                Name = user.Name,
                Family = user.Family,
                Age = user.Age,
                Brithday = user.Brithday,
            };
        }

        public async Task<EditUserDto> GetUserForEdit(int id)
        {
            var user = await _userRepository.GetById(id);
            return new EditUserDto
            {
                Id = user.Id,
                Address = user.Address,
                Name = user.Name,
                Family = user.Family,
                Age = user.Age,
                Brithday = user.Brithday
            };
        }

        public async Task EditUser(EditUserDto userDto)
        {
            var user = await _userRepository.GetById(userDto.Id);
            user.Address = userDto.Address;
            user.Name = userDto.Name;
            user.Family = userDto.Family;
            user.Age = userDto.Age;
            user.Brithday = userDto.Brithday;
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
        }

        public async Task<DeleteUserDto> GetUserForDelete(int id)
        {
            var user = await _userRepository.GetById(id);
            return new DeleteUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family
            };
        }

        public async Task DeleteUser(DeleteUserDto userDto)
        {
            var user = await _userRepository.GetById(userDto.Id);
            user.Dlt = true;
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
        }

        public async Task RestoreUser(int id)
        {
            var user = await _userRepository.GetById(id);
            user.Dlt = false;
            _userRepository.UpdateUser(user);
            await _userRepository.Save();
        }

        public async Task DeletePermanent(int id)
        {
            var user = await _userRepository.GetById(id);
            _userRepository.DeleteUser(user);
            await _userRepository.Save();
        }

        public async Task<MemoryStream> GenerateExcelFile(int isDeleted)
        {
            var Result = await _userRepository.GetAllForExcel(isDeleted);
            var users = Result.UserList;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");

                // اضافه کردن عنوان ستون‌ها
                worksheet.Cell(1, 1).Value = "نام";
                worksheet.Cell(1, 2).Value = "نام خانوادگی";
                worksheet.Cell(1, 3).Value = "سن";
                worksheet.Cell(1, 4).Value = "تاریخ تولد";
                worksheet.Cell(1, 5).Value = "آدرس";

                // پر کردن داده‌ها
                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];
                    worksheet.Cell(i + 2, 1).Value = user.Name;
                    worksheet.Cell(i + 2, 2).Value = user.Family;
                    worksheet.Cell(i + 2, 3).Value = user.Age;
                    worksheet.Cell(i + 2, 4).Value = DateTimeConverter.MiladiToShamsi(user.Brithday);
                    worksheet.Cell(i + 2, 5).Value = user.Address;
                }
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream;
                }
            }
        }
    }
}
