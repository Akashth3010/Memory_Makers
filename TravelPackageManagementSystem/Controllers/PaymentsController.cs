using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interface;
using System;
using System.Threading.Tasks;

namespace TravelPackageManagementSystem.Application.Controllers
{
    // The route becomes: https://localhost:7257/Payment/SavePayment
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
            // 1. Basic Null Check
            if (payment == null)
            {
                return BadRequest("No payment data received from the browser.");
            }

            // 2. Validate the Data (Matches the [Required] attributes in your Model)
            // If JavaScript didn't send 'PaymentDate' or 'Amount', this will be false.
            if (!ModelState.IsValid)
            {
                // Returns the EXACT error (e.g., "The PaymentDate field is required.")
                return BadRequest(ModelState);
            }

            try
            {
                // 3. Set Server-Side Values
                // We overwrite these to ensure they are correct regardless of what JS sent
                payment.PaymentDate = DateTime.Now;
                

                // 4. Call Service to Save to Database
                var result = await _paymentService.ProcessPaymentAsync(payment);

                if (result)
                {
                    return Ok(new { message = "Data Recorded",status = payment.Status });
                }
                else
                {
                    return StatusCode(500, "Database insert failed. The service returned false.");
                }
            }
            catch (Exception ex)
            {
                // 5. Catch SQL/System Errors and return them
                // This helps you see the REAL error in your Failure page
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}