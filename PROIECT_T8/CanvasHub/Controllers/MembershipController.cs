//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using CanvasHub.Models;
//using CanvasHub.Services;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;

//namespace CanvasHub.Controllers
//{
//    [Authorize(Roles = "Admin")] // Ensure only admins can access this controller
//    public class MembershipController : Controller
//    {
//        private readonly IMembershipService _membershipService;

//        public MembershipController(IMembershipService membershipService)
//        {
//            _membershipService = membershipService;
//        }

//        // Add action methods for adding members and admins here
//        // For example:

//        public async Task<IActionResult> AddMember(string userId)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user != null)
//            {
//                var success = await _membershipService.AddMemberAsync(user);
//                if (success)
//                {
//                    // Redirect or return success message
//                }
//                else
//                {
//                    // Handle failure
//                }
//            }
//            else
//            {
//                // Handle user not found
//            }
//        }

//        public async Task<IActionResult> AddAdmin(string userId)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user != null)
//            {
//                var success = await _membershipService.AddAdminAsync(user);
//                if (success)
//                {
//                    // Redirect or return success message
//                }
//                else
//                {
//                    // Handle failure
//                }
//            }
//            else
//            {
//                // Handle user not found
//            }
//        }
//    }
//}
