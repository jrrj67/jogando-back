using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace api.Data.Validators
{
    public class UsersValidator : AbstractValidator<UsersRequest>
    {
        private readonly IUsersService<UsersResponse, UsersRequest> _usersService;

        private readonly IHttpContextAccessor _httpContexAccessor;

        public UsersValidator(IUsersService<UsersResponse, UsersRequest> usersService, IHttpContextAccessor httpContexAccessor)
        {
            _usersService = usersService;

            _httpContexAccessor = httpContexAccessor;

            int userId = GetUserIdFromPath(_httpContexAccessor.HttpContext.Request.Path.Value);

            RuleFor(field => field.Name)
                .MinimumLength(2)
                .MaximumLength(200)
                .NotNull();

            RuleFor(field => field.Password)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(field => field.Email)
                .EmailAddress()
                .NotNull()
                .MaximumLength(100)
                .Must(email => _usersService.IsUniqueEmail(email, userId))
                .WithMessage("Email already exists.");

            RuleFor(field => field.RoleId)
                .NotNull();
        }

        public int GetUserIdFromPath(string path)
        {
            int userId;
            int.TryParse(path.Replace("/api/users/", ""), out userId);
            return userId;
        }
    }
}
