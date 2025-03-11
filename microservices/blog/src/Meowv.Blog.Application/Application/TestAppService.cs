using Volo.Abp.Application.Services;

namespace Meowv.Blog.Application
{
    public class TestAppService : ApplicationService
    {
        public string Get()
        {
            return "Hello";
        }
    }
}
