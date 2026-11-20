namespace Bookings.Application.Bookings.Validation;

public static class MachineValidation
{
    public static void Validate(string name, string type, string registrationNumber, decimal pricePerDay)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Namn saknas.");

        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Typ saknas.");

        if (string.IsNullOrWhiteSpace(registrationNumber))
            throw new ArgumentException("Registreringsnummer saknas.");

        if (pricePerDay < 0)
            throw new ArgumentException("Priset kan inte vara negativt.");
    }
}