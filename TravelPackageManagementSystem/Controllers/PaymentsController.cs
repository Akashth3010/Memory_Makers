using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("SavePayment")]
        public async Task<IActionResult> SavePayment([FromBody] Payment payment)
        {
            try
            {
                // 1. Zero-Reference Check: Don't let nulls through
                if (payment == null || payment.BookingId <= 0)
                    return BadRequest(new { success = false, message = "Invalid Payment Data" });

                // 2. Prevent "Shadow Property" errors by detaching the navigation object
                // This stops EF from trying to create a new 'BookingId1'
                payment.Booking = null;

                var result = await _paymentService.ProcessPaymentAsync(payment);

                if (result) return Ok(new { success = true });

                return StatusCode(500, new { success = false, message = "Database rejected save. Check Foreign Keys." });
            }
            catch (Exception ex)
            {
                // This helps you see the REAL error in the browser
                return StatusCode(500, new { success = false, message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}