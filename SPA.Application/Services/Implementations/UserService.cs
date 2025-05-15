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
using static SPA.Domain.Dtoes.DeleteUserDto;
using static SPA.Domain.Dtoes.DetailsUserDto;
using static SPA.Domain.Dtoes.EditUserDto;

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
            if (userDto.PhoneNumbers != null && userDto.PhoneNumbers.Any())
            {
                foreach (var item in userDto.PhoneNumbers)
                {
                    var pn = new PhoneBook()
                    {
                        Phonebook = item,
                        UserId = user.Id
                    };
                    _userRepository.AddPhone(pn);
                    await _userRepository.Save();
                }
            }
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
                PhoneNumbers = user.phones.Select(x => new PNDtoForDetail
                {
                    Id = x.PhonebookId,
                    Number = x.Phonebook
                }).ToList()
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
                Brithday = user.Brithday,
                PhoneNumbers = user.phones.Select(x => new PNDto
                {
                    Id = x.PhonebookId,
                    Number = x.Phonebook
                }).ToList()
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
            var x = userDto.PhoneNumbers;
            //_userRepository.UpdateUser(user);
            //in khat zir mige ke onaei ke az form hazf kardio az database hazf kone
            var toRemove = user.phones.Where(x => !userDto.PhoneNumbers.Any(pn => pn.Id == x.PhonebookId)).ToList();
            foreach (var i in toRemove)
            {
                _userRepository.DeletePhone(i);
            }
            foreach (var item in userDto.PhoneNumbers)
            {
                if (item.Id.HasValue)
                {
                    //edit
                    var existing = _userRepository.GetPhoneBookById(item.Id.Value);
                    existing.Phonebook = item.Number;
                }
                else
                {
                    //add
                    var n = new PhoneBook()
                    {
                        Phonebook = item.Number,
                        UserId = user.Id
                    };
                    _userRepository.AddPhone(n);
                }
            }
            await _userRepository.Save();
        }

        public async Task<DeleteUserDto> GetUserForDelete(int id)
        {
            var user = await _userRepository.GetById(id);
            return new DeleteUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family,
                PhoneNumbers = user.phones.Select(x => new PNDtoForDelete
                {
                    Id = x.PhonebookId,
                    Number = x.Phonebook
                }).ToList()
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
            var phoneOfUser = user.phones.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in phoneOfUser)
            {
                _userRepository.DeletePhone(i);
            }
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
