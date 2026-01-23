using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task<int> SaveChangesAsync()
    {
        // Returns the number of state entries written to the database.
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync() => await _context.Payments.ToListAsync();
    public async Task<Payment> GetPaymentByIdAsync(int id) => await _context.Payments.FindAsync(id);
}