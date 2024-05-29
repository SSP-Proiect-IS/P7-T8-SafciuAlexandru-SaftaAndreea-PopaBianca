using System;
using System.Threading.Tasks;
using CanvasHub.Models;
using Microsoft.AspNetCore.Identity;

namespace CanvasHub.Services
{
    public interface IPaymentManagementService
    {
        Task SetSubscriptionAmountAsync(string adminId, float amount, string reason);
        Task ModifySubscriptionAmountAsync(string adminId, float newAmount, string reason);
    }

    public class PaymentManagementService : IPaymentManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly CanvasHubContext _context;

        public PaymentManagementService(UserManager<User> userManager, CanvasHubContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task SetSubscriptionAmountAsync(string adminId, float amount, string reason)
        {
            if (string.IsNullOrEmpty(adminId))
            {
                throw new ArgumentNullException(nameof(adminId), "Admin ID cannot be null or empty.");
            }

            var admin = await _userManager.FindByIdAsync(adminId);
            if (admin == null)
            {
                throw new InvalidOperationException("Admin not found.");
            }

            // Add logic to set subscription amount
            admin.Subscription = amount;

            // Save the reason for changing subscription amount
            //admin.SubscriptionChangeReason = reason;

            await _userManager.UpdateAsync(admin);
        }

        public async Task ModifySubscriptionAmountAsync(string adminId, float newAmount, string reason)
        {
            if (string.IsNullOrEmpty(adminId))
            {
                throw new ArgumentNullException(nameof(adminId), "Admin ID cannot be null or empty.");
            }

            var admin = await _userManager.FindByIdAsync(adminId);
            if (admin == null)
            {
                throw new InvalidOperationException("Admin not found.");
            }

            // Add logic to modify subscription amount
            admin.Subscription = newAmount;

            // Save the reason for changing subscription amount
            //admin.SubscriptionChangeReason = reason;

            await _userManager.UpdateAsync(admin);
        }
    }
}
