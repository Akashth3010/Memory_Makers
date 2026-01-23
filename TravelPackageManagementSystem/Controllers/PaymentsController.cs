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
            if (payment == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                var result = await _paymentService.ProcessPaymentAsync(payment);

                if (result)
                {
                    return Ok(new { success = true });
                }

                return StatusCode(500, "Database Error: The save operation affected 0 rows.");
            }
            catch (Exception ex)
            {
                // Return the specific inner exception message (e.g., Foreign Key violation)
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, detailedError);
            }
        }
    }
}