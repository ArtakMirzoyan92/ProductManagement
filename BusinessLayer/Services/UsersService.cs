using AutoMapper;
using Azure;
using BusinessLayer.IProviders;
using BusinessLayer.IServices;
using BusinessLayer.Models.Auth;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;

namespace BusinessLayer.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository userRepository,
                            IPasswordHasher passwordHasher,
                            IJwtProvider jwtProvider,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<UserResponse> AddUserAsync(UserRegisterRequest userDto)
        {

            User newUser = _mapper.Map<User>(userDto);

            newUser.Id = Guid.NewGuid();
            newUser.PasswordHash = _passwordHasher.GenerateHash(userDto.PasswordHash);

            UserResponse response = _mapper.Map<UserResponse>(await _userRepository.AddUserAsync(newUser));

            return response;
        }

        public async Task<string> GetByEmailAsync(string userEmail, string password)
        {
            string token = string.Empty;

            User user = await _userRepository.GetByEmailAsync(userEmail);

            if (user != null && _passwordHasher.VarifyPassword(password, user?.PasswordHash))
            {
               token = _jwtProvider.GenerateToken(user);
            }

            return token;
        }
    }
}
