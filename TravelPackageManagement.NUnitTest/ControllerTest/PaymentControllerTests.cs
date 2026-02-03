using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Application.Controllers;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class PaymentControllerTests
    {
        private PaymentController _controller;
        private Mock<IPaymentService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPaymentService>();
            _controller = new PaymentController(_mockService.Object);
        }

        [Test]
        public async Task SavePayment_ValidData_ReturnsOk()
        {
            // ARRANGE
            var payment = new Payment { Amount = 100, BookingId = 1, TransactionId = "T1" };
            _mockService.Setup(s => s.ProcessPaymentAsync(It.IsAny<Payment>()))
                        .ReturnsAsync(true);

            // ACT
            var result = await _controller.SavePayment(payment);

            // ASSERT
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task SavePayment_WhenInputIsNull_ReturnsBadRequest()
        {
            // ACT
            var result = await _controller.SavePayment(null);

            // ASSERT
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}