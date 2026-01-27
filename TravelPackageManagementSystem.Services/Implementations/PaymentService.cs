using System;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Services.Interfaces;
using TravelPackageManagementSystem.Repository.Data;
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
            // Transaction ensures both tables update or none do
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (payment.PaymentDate == DateTime.MinValue)
                    {
                        payment.PaymentDate = DateTime.Now;
                    }

                    // 1. Save the Payment (This part is working based on your SQL results)
                    await _paymentRepository.AddPaymentAsync(payment);
                    await _paymentRepository.SaveChangesAsync();

                    // 2. Find the Booking to update its status
                    var booking = await _context.Bookings.FindAsync(payment.BookingId);
                    if (booking != null)
                    {
                        // FIX: Use the Enum value instead of a string to resolve CS0029
                        booking.Status = BookingStatus.CONFIRMED;

                        _context.Bookings.Update(booking);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Payment/Booking Sync Error: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Payment>> GetTransactionHistoryAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }
    }
}