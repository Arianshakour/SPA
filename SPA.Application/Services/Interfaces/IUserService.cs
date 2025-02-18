using SPA.Domain.Dtoes;
using SPA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetAll(string searchValue, int page, int pageSize, int sort, int isDeleted);
        Task AddUser(CreateUserDto userDto);
        Task<DetailsUserDto> GetUser(int id);
        Task<EditUserDto> GetUserForEdit(int id);
        Task EditUser(EditUserDto userDto);
        Task<DeleteUserDto> GetUserForDelete(int id);
        Task DeleteUser(DeleteUserDto userDto);
        Task RestoreUser(int id);
        Task DeletePermanent(int id);
        Task<MemoryStream> GenerateExcelFile(int isDeleted);
    }
}
