using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetTransactionHistoryAsync();
    }
}
