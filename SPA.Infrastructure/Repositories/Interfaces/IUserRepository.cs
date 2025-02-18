using SPA.Domain.Entities;

namespace SPA.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserViewModel> GetAll(string searchValue, int page, int pageSize, int sort, int isDeleted);
        Task AddUser(User user);
        Task Save();
        Task<User?> GetById(int id);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<UserViewModel> GetAllForExcel(int isDeleted);
    }
}
