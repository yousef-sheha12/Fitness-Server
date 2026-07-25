using Fitness.Helpers;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Booking;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly IStripeService _stripeService;

        public BookingsController(
            IBookingService bookingService,
            IPaymentService paymentService,
            IStripeService stripeService)
        {
            _bookingService = bookingService;
            _paymentService = paymentService;
            _stripeService = stripeService;
        }

        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : null;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/bookings")]
        public async Task<IActionResult> GetBookings()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var bookings = await _bookingService.GetByUserIdAsync(userId.Value);
            return ApiResponse(bookings);
        }

        [HttpPost("api/bookings/schedule")]
        public async Task<IActionResult> ScheduleBooking([FromBody] Booking booking)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            booking.UserId = userId.Value;
            booking.CreatedAt = DateTime.UtcNow;
            var result = await _bookingService.CreateAsync(booking);
            return ApiResponse(result, "Booking scheduled");
        }

        [HttpPost("api/bookings/{id}/pay")]
        public async Task<IActionResult> PayBooking(int id, [FromBody] PayBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);

            var paymentIntentId = await _stripeService.CreatePaymentIntentAsync(booking.Amount, "usd");
            var payment = new Payment
            {
                UserId = booking.UserId,
                BookingId = booking.Id,
                Amount = booking.Amount,
                PaymentMethod = "Card",
                StripePaymentId = paymentIntentId,
                Status = "Pending"
            };
            await _paymentService.CreateAsync(payment);

            return ApiResponse(new { paymentIntentId }, "Payment initiated");
        }

        [HttpPost("api/bookings/{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id, [FromBody] ConfirmBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);
            booking.Status = "Confirmed";
            await _bookingService.UpdateAsync(id, booking);
            return ApiResponse(message: "Booking confirmed");
        }

        [HttpPut("api/bookings/{id}/reschedule")]
        public async Task<IActionResult> RescheduleBooking(int id, [FromBody] RescheduleBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);
            booking.BookingDate = dto.BookingDate;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.Status = "Rescheduled";
            await _bookingService.UpdateAsync(id, booking);
            return ApiResponse(message: "Booking rescheduled");
        }

        [HttpDelete("api/bookings/{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.DeleteAsync(id);
            if (!result) return ApiResponse(null, "Booking not found", 404);
            return ApiResponse(message: "Booking cancelled");
        }
    }
}
