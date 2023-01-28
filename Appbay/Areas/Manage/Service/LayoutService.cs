using Appbay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Appbay.Areas.Manage.Service
{
    public class LayoutService
    {
        public LayoutService(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserManager<AppUser> _userManager { get; }
        public IHttpContextAccessor _httpContextAccessor { get; }

        public async Task<AppUser> GetUser()
        {
            string name = _httpContextAccessor.HttpContext.User.Identity.Name;
            AppUser appUser=await _userManager.FindByNameAsync(name);
            return appUser;

        }
    }
}
