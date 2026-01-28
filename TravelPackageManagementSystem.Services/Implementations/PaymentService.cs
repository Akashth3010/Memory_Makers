using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly AppDbContext _context;

        public PaymentService(IPaymentRepository paymentRepository, AppDbContext context)
        {
            _paymentRepository = paymentRepository;
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(Payment payment)
        {
            // 1. Standardize Data
            if (payment.PaymentDate == DateTime.MinValue) payment.PaymentDate = DateTime.Now;
            if (string.IsNullOrEmpty(payment.Status)) payment.Status = "Completed";

            // 2. Add Payment Record
            await _paymentRepository.AddPaymentAsync(payment);

            // 3. Update Booking Status
            var booking = await _context.Bookings.FindAsync(payment.BookingId);
            if (booking != null)
            {
                // FIX: Used CONFIRMED (All Uppercase) to match your Enum definition
                booking.Status = BookingStatus.CONFIRMED;
            }

            // 4. Save Changes
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Payment>> GetTransactionHistoryAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }
    }
}