using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Implementations;

public class BookingsController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost] // This means it only runs when a form is submitted
    public async Task<IActionResult> Create(Booking booking)
    {
        if (ModelState.IsValid)
        {
            await _bookingService.CreateBookingAsync(booking);
            // After booking, send them to a "Thank You" or "My Bookings" page
            return RedirectToAction("Index", "Bookings");
        }
        return View(booking); // If there's an error, show the form again
    }
}