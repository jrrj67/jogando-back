using api.Data.Repositories.Users;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.PasswordHasher;
using api.Data.Services.Token;
using AutoMapper;

namespace api.Data.Services.Login
{
    public class LoginService : ILoginService<LoginResponse, LoginRequest>
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginService(IUsersRepository repository, IMapper mapper, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            var userEntity = _repository.GetUserByEmail(loginRequest.Email);

            var token = _tokenService.GenerateToken(userEntity);

            var userResponse = _mapper.Map<UsersResponse>(userEntity);

            return new LoginResponse
            {
                User = userResponse,
                Token = token
            };
        }

        public bool VerifyEmailAndPassword(string email, string password)
        {
            var user = _repository.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }

            if (!_passwordHasher.Verify(password, user.Password))
            {
                return false;
            }

            return true;
        }
    }
}
