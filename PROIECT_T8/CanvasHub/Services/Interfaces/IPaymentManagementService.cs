namespace CanvasHub.Services.Interfaces
{
    public interface IPaymentManagementService
    {
        Task SetSubscriptionAmountAsync(string adminId, float amount, string reason);
        Task ModifySubscriptionAmountAsync(string adminId, float newAmount, string reason);
    }
}
