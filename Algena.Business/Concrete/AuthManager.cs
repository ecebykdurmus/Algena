using Algena.Business.Abstract;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore;
using Algena.Entities.Concrete;
using Algena.Entities.Dtos.CustomerDtos;
using Algena.Entities.Dtos.LoginDtos;
using Algena.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace Algena.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        //Tüm kullanıcı işlemlerini yaptığım Service Manager'im burası olacak.
        private readonly UserManager<AppUser> _userManager; //Kullanıcı işlemi;
        private readonly RoleManager<AppRole> _roleManager; //Role atamak için;
        private readonly SignInManager<AppUser> _signInManager; //Giriş çıkış işlemlerine yardımcı olmak için;
        private readonly IUnitOfWork _unitOfWork;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role)
        {
            AppRole appRole = _roleManager.Roles.FirstOrDefault(x => x.Name == role);
            if (appRole is null) 
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
            return await _userManager.AddToRoleAsync(appUser,role);
        }

        //Customer'i üye yapmak için;
        public async Task<IdentityResult> CustomerRegisterAsync(CustomerAddDto customerDto)
        {
            //AppUser tablosuna ekledik.
            AppUser appUser = new AppUser()
            {
                Email = customerDto.Email,
                PhoneNumber = customerDto.Phone,
                UserName = customerDto.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, customerDto.Password);

            //Customer tablosune ekledik
            Customer customer = new Customer()
                {
                    Id = appUser.Id,
                    Address = customerDto.Address,
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    City = customerDto.City,
                    Country = customerDto.Country,
                    Fax = customerDto.Fax,
                    PostalCode = customerDto.PostalCode,
                    Password = customerDto.Password,
                    Phone = customerDto.Phone,
                    Email = customerDto.Email
                };

                await _unitOfWork.CustomerDal.AddAsync(customer);
                await _unitOfWork.SaveAsync();

                if(result.Succeeded)
                {
                    AddToRoleAsync(appUser, "customer");
                    await _unitOfWork.SaveAsync();
                }
                return result;
        }

        public async Task<IdentityResult> SellerRegisterAsync(SellerAddDto sellerDto)
        {
            AppUser appUser = new AppUser()
            {
                Email = sellerDto.Email,
                PhoneNumber = sellerDto.Phone,
                UserName = sellerDto.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, sellerDto.Password);

            Seller seller = new Seller()
            {
                Id = appUser.Id,
                FirstName = sellerDto.FirstName,
                LastName = sellerDto.LastName,
                CompanyName = sellerDto.CompanyName,
                Email = sellerDto.Email,
                Password = sellerDto.Password,
                Phone = sellerDto.Phone,
                PostalCode = sellerDto.PostalCode,
                Address = sellerDto.Address,
                City = sellerDto.City,
                Country = sellerDto.Country,
            };

            await _unitOfWork.SellerDal.AddAsync(seller);
            await _unitOfWork.SaveAsync();

            if (result.Succeeded)
            {
                AddToRoleAsync(appUser, "seller");
                await _unitOfWork.SaveAsync();
            }
            return result;
        }
        public async Task<SignInResult> LoginAsync(LoginDto loginDto)
        {
            AppUser appUser;
            if (loginDto.UserName.Contains("@"))
            {
                appUser = _userManager.Users.FirstOrDefault(x => x.Email == loginDto.UserName);
            }
            else
            {
                appUser = _userManager.Users.FirstOrDefault(x => x.UserName == loginDto.UserName);
            }
            return appUser is not null ? await
            _signInManager.PasswordSignInAsync(appUser,loginDto.Password, true, false) : null;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> PasswordResetAsync(string userName, string newPassword)
        {
            string token = null;

            AppUser user = await GetUserAsync(userName);
            IdentityResult result = await _userManager.RemovePasswordAsync(user);
            if(result.Succeeded)
            {
                token = await _userManager.GeneratePasswordResetTokenAsync(user); 
            }
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityResult> UpdatePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            AppUser user = await GetUserAsync(userName);
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<AppUser> GetUserAsync(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => userName.Contains("@") ? x.Email == userName : x.UserName == userName);
        }

        public async Task<IdentityResult> RemoveUserAsync(string userName)
        {
            IdentityResult result = null;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    AppUser user = await GetUserAsync(userName);

                    //Gelen Role göre Customer veya Seller silinecek.

                    Seller seller = await _unitOfWork.SellerDal.GetAsync(x => x.Id == user.Id);

                    result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        await _unitOfWork.SellerDal.DeleteAsync(seller);
                        await _unitOfWork.SaveAsync();
                    }

                    ts.Complete();
                }
                catch (Exception)
                {
                    ts.Dispose();
                }
            }
            return result;
        }

        public async Task<List<string>> GetRolesAsync(AppUser user)
        {
            List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();
            return roles;
        }

        public async Task<string> CreateTokenAsync(LoginDto loginDto)
        {
            string token = null;
            AppUser user = await _userManager.FindByEmailAsync(loginDto.UserName);
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, user.LockoutEnabled);
            if (user is not null && result.Succeeded)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserName",user.UserName),
                    new Claim("Email",user.Email)
                };
                List<string> roles = GetRolesAsync(user).Result;
                foreach (string item in roles)
                {
                    claims.Add(new Claim("Role", item));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                var key = Encoding.UTF8.GetBytes("keykullaniyorumburadaasdasdfhashgdvasd");
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                {
                    //Sağlayıcı
                    Audience = "localhost",
                    //Kullanıcı 
                    Issuer = "localhost",
                    // Claimsler 
                    Subject = claimsIdentity,
                    // Geçerlilik süresi
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken jwtToken = handler.CreateToken(descriptor);

                token = handler.WriteToken(jwtToken);
            }
            return token;
        }
    }
}
