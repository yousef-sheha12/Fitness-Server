using Fitness.Helpers;
using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Auth;

namespace Fitness.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public UserService(IUserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            var token = _jwtHelper.GenerateToken(user);
            return new AuthResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token,
                ProfileImage = user.ProfileImage
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userRepository.GetByEmailAsync(registerDto.Email) != null)
                return null;

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Phone = registerDto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
            var token = _jwtHelper.GenerateToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token,
                ProfileImage = user.ProfileImage
            };
        }

        public async Task<User?> GetByIdAsync(int id) =>
            await _userRepository.GetByIdAsync(id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _userRepository.GetByEmailAsync(email);

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Name = user.Name ?? existing.Name;
            existing.Phone = user.Phone ?? existing.Phone;
            existing.UpdatedAt = DateTime.UtcNow;
            return await _userRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _userRepository.DeleteAsync(id);

        public async Task<bool> VerifyOtpAsync(string email, string code)
        {
            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
