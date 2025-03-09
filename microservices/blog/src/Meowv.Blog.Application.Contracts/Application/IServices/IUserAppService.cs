using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface IUserAppService
{
    Task<BlogResponse> CreateUserAsync(CreateUserInput input);

    Task<BlogResponse> DeleteUserAsync(string id);

    Task<BlogResponse> UpdateUserAsync(string id, UpdateUserinput input);

    Task<BlogResponse> UpdatePasswordAsync(string id, string password);

    Task<BlogResponse> SettingAdminAsync(string id, bool isAdmin);

    Task<BlogResponse<List<UserDto>>> GetUsersAsync();

    Task<BlogResponse<UserDto>> GetUserAsync(string id);

    Task<BlogResponse<UserDto>> GetCurrentUserAsync();

    Task<UserDto> CreateUserAsync(string username, string type, string identity, string name, string avatar,
        string email);

    Task<UserDto> VerifyByAccountAsync(string username, string password);

    Task<UserDto> GetDefaultUserAsync();
}