namespace Bookings.Application.Bookings.Validation;

public static class BusinessServiceValidation
{
    public static void Validate(string name, decimal pricePerHour)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Namn saknas.");

        if (pricePerHour < 0)
            throw new ArgumentException("Priset kan inte vara negativt.");
    }
}