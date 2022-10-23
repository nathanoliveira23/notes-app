using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Repository;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [Authorize]
    public class BaseController : ControllerBase
    {
        public BaseController(IUserRepository userRepository) => _userRepository = userRepository;
        protected readonly IUserRepository _userRepository;
        protected User ReadToken()
        {
            string userId = User.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(claim => claim.Value).FirstOrDefault();

             if(string.IsNullOrEmpty(userId))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserId(int.Parse(userId));
            }
        }
    }
}