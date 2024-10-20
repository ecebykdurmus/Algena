using Algena.Entities.Dtos.LoginDtos;
using Algena.Entities.Dtos.SellerDtos;
using Algena.Entities.Dtos.CustomerDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algena.Entities.Concrete;

namespace Algena.Business.Abstract
{
    public interface IAuthService
    {
        Task<List<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult> PasswordResetAsync(string userName, string newPassword);
        Task<IdentityResult> UpdatePasswordAsync(string userName, string currentPassword, string newPassword);
        Task<SignInResult> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> SellerRegisterAsync(SellerAddDto sellerDto);
        Task<IdentityResult> CustomerRegisterAsync(CustomerAddDto customerDto);
        Task SignOutAsync();
        Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);
        Task<AppUser> GetUserAsync(string userName);
        Task<IdentityResult> RemoveUserAsync(string userName);

        Task<string> CreateTokenAsync(LoginDto loginDto);

    }
}
