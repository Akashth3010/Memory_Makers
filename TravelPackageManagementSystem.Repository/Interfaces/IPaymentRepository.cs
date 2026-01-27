using TravelPackageManagementSystem.Repository.Models;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task AddPaymentAsync(Payment payment);
    // Ensure this matches the implementation
    Task<int> SaveChangesAsync();
}