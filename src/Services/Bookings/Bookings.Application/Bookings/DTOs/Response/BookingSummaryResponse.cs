using Bookings.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookings.Application.Bookings.DTOs.Response;

public record BookingSummaryResponse(
    Guid Id,
    DateTime StartsAt,
    DateTime? EndsAt,
    BookingStatus Status,
    Guid CustomerId,
    Guid? ProjectId
    );

