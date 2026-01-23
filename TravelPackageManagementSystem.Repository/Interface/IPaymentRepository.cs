using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(Payment payment);
        Task SaveChangesAsync();
    }
}
