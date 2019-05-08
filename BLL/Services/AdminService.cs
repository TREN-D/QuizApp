using AutoMapper;
using BLL.Interfaces;
using BLL.Models.AdminModels;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> CreateAsync(CreateAdminModel createAdminModel, CancellationToken cancellationToken)
        {
            Admin admin = null;
            var existingAdmin = await _adminRepository.GetByEmailAsync(createAdminModel.Email, cancellationToken);
            if(existingAdmin == null)
            {
                admin = Mapper.Map<CreateAdminModel, Admin>(createAdminModel);
                await _adminRepository.AddAsync(admin, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<AdminModel> GetByCredentials(string email, string password, CancellationToken cancellationToken)
        {
            AdminModel adminModel = null;
            var existedAdmin = await _adminRepository.GetByEmailAsync(email, cancellationToken);
            if(existedAdmin != null)
            {
                adminModel = existedAdmin.Password.Equals(password) ? Mapper.Map<Admin, AdminModel>(existedAdmin) : null;
            }
            return adminModel;
        }
    }
}
