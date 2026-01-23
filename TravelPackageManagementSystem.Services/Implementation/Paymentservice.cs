using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interface;

namespace TravelPackageManagementSystem.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> ProcessPaymentAsync(Payment payment)
        {
            // Business Logic Example: 
            // 1. Validate the payment details
            // 2. Call external API (Stripe/PayPal) - simulated here
            if (payment.PaymentDate == DateTime.MinValue)
            {
                payment.PaymentDate = DateTime.Now;
            }

            // 2. CHECK: If status is already "Failed" (from our security check), 
            // do NOT overwrite it with "Completed".
            if (string.IsNullOrEmpty(payment.Status))
            {
                payment.Status = "Completed";
            }

            // 3. Save to our database via Repository
            await _paymentRepository.AddPaymentAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Payment>> GetTransactionHistoryAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }
    }
}
