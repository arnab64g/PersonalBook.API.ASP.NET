using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;
using PersonalBook.API.Model;

namespace PersonalBook.API.Services
{
    public interface IUserService
    {
        public Task<SignUpResult> RegistrationAsync(UserBase newUser);
        public Task<LoginResult> LoginAsync(Login login);
        public Task<ApplicationUser> GetApplicationUserAsync(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly UserDbContext userDbContext;

        public UserService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<ApplicationUser?> GetApplicationUserAsync(Guid id)
        {
            ApplicationUser? applicationUser = await userDbContext.ApplicationUsers.Where(d => d.Id==id).FirstAsync();

            return applicationUser;
        }

        public async Task<SignUpResult> RegistrationAsync(UserBase newUser)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = newUser.FirstName?.Trim(),
                LastName = newUser.LastName?.Trim(),
                Email = newUser.Email?.Trim(),
                Username = newUser.Username?.Trim(),
                Address = newUser.Address?.Trim(),
                PhoneNumber = newUser.PhoneNumber?.Trim(),
                Password = newUser.Password?.Trim()
            };
            SignUpResult signUpResult = new SignUpResult
            {
                Successed = 0
            };

            if (await userDbContext.ApplicationUsers.AnyAsync(d => d.Username == applicationUser.Username))
            {
                signUpResult.Errors.Add(ErrorMessage.UsernameExist);
            }
            
            if (await userDbContext.ApplicationUsers.AnyAsync(d => d.Email == applicationUser.Email))
            {
                signUpResult.Errors.Add(ErrorMessage.EmailAddressUsed);
            }

            if (await userDbContext.ApplicationUsers.AnyAsync(d => d.PhoneNumber == applicationUser.PhoneNumber))
            {
                signUpResult.Errors.Add(ErrorMessage.PhoneNumberUsed);
            }

            if (applicationUser.Password?.Length < 6 || applicationUser.Password == null)
            {
                signUpResult.Errors.Add(ErrorMessage.ShortPassword);
            }

            if (signUpResult.Errors.Count == 0)
            {
                applicationUser.Id = Guid.NewGuid();
                applicationUser.Password = PasswordHash.HashPassword(applicationUser?.Password);
                applicationUser.Created = DateTime.Now;
                applicationUser.Role = "User";
                applicationUser.Created = DateTime.Now;
                 
                await userDbContext.ApplicationUsers.AddAsync(applicationUser);
                int res = await userDbContext.SaveChangesAsync();

                if (res != 0)
                {
                    signUpResult.Successed = 1;
                }
            }

            return signUpResult;
        }

        public async Task<LoginResult> LoginAsync(Login login)
        {
            var user = await userDbContext.ApplicationUsers.Where(d => d.Username == login.Username || d.Email == login.Username || d.PhoneNumber == login.Username).FirstOrDefaultAsync();
            LoginResult loginResult = new LoginResult
            {
                Successed = 0,
            };

            if (user == null)
            {
                loginResult.Message = ErrorMessage.UserNotExist; 
                
                return loginResult;
            }
            else
            {
                if (PasswordHash.VerifyPassword(login.Password, user.Password))
                {
                    AngularAuthService angularAuthService = new AngularAuthService();
                    user.Token = angularAuthService.CreateJWT(user);
                    loginResult.Successed = 1;
                    loginResult.Message = "Loged In";
                    loginResult.Token = user.Token;

                    return loginResult;
                }
                else
                {
                    loginResult.Message = ErrorMessage.InvalidPassword;

                    return loginResult;
                }
            }
        }
    }
}
