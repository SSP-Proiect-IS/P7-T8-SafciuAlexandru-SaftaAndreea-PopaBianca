using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CanvasHub.Models;

namespace CanvasHub.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly UserManager<User> _userManager;
        //private readonly IEmailService _emailService; // Assume an Email Service is implemented separately

        public MembershipService(UserManager<User> userManager/*, IEmailService emailService*/)
        {
            _userManager = userManager;
            //_emailService = emailService;
        }

        public async Task<bool> AddMemberAsync(User user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Member");
            if (result.Succeeded)
            {
                //await _emailService.SendEmailAsync(user.Email, "Membership Status Update", "You have been promoted to a member.");
                //// Logic to send inbox message
                //return true;
            }
            return false;
        }

        public async Task<bool> AddAdminAsync(User user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                //await _emailService.SendEmailAsync(user.Email, "Membership Status Update", "You have been promoted to an admin.");
                //// Logic to send inbox message
                //return true;
            }
            return false;
        }
    }
}
