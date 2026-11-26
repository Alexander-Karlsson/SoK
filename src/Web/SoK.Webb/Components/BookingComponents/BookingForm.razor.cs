using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.DTOs.Response;
using Employees.Application.Employees.DTOs.Employee;
using Microsoft.AspNetCore.Components;
using SoK.Webb.Clients;

namespace SoK.Webb.Components.BookingComponents;

public partial class BookingForm
{
    // Vem bokningen gäller. Admin väljer, en privatperson får sitt eget id från inloggningen.
    [Parameter] public Guid? CustomerId { get; set; }

    // Anropas med den nya bokningens id. Sidan bestämmer vart man skickas.
    [Parameter] public EventCallback<Guid> OnCreated { get; set; }

    private bool loading = true;
    private bool busy;
    private string? error;

    private List<MachineResponse> machines = [];
    private List<BusinessServiceResponse> services = [];

    private DateTime? startsAt = DateTime.Today.AddDays(1).AddHours(7);
    private DateTime? endsAt;
    private HashSet<Guid> selectedMachines = [];
    private Dictionary<Guid, decimal> serviceHours = [];
    private List<EmployeeResponse> employees = [];
    private HashSet<Guid> selectedEmployees = [];
    private string? employeeError;
    private string employeeSearch = "";

    // Valda anställda visas alltid, även om de inte matchar sökningen
    private List<EmployeeResponse> FilteredEmployees => employees
        .Where(e => selectedEmployees.Contains(e.Id)
            || string.IsNullOrWhiteSpace(employeeSearch)
            || $"{e.FirstName} {e.LastName} {e.Title}"
                .Contains(employeeSearch.Trim(), StringComparison.OrdinalIgnoreCase))
        .ToList();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            machines = (await BookingApiClient.GetMachinesAsync()).Where(m => !m.IsOutOfService).ToList();
            services = (await BookingApiClient.GetServicesAsync()).Where(s => s.IsActive).ToList();
        }
        catch (HttpRequestException)
        {
            error = "Kunde inte hämta maskiner och tjänster. Försök igen senare.";
        }

        foreach (var s in services)
            serviceHours[s.Id] = 0;

        try
        {
            employees = (await EmployeeApiClient.GetAllAsync())
                .Where(e => e.IsActive)
                .OrderBy(e => e.FirstName)
                .ToList();
        }
        catch (HttpRequestException)
        {
            employeeError = "Kunde inte hämta anställda. Försök igen senare.";
        }

        loading = false;
    }

    private void ToggleMachine(Guid id, bool selected)
    {
        if (selected) selectedMachines.Add(id);
        else selectedMachines.Remove(id);
    }
    private void ToggleEmployee(Guid id, bool selected)
    {
        if (selected) selectedEmployees.Add(id);
        else selectedEmployees.Remove(id);
    }

    // Sidan som använder formuläret lägger själv sin "Skapa bokning"-knapp
    // och anropar SaveAsync via @ref. IsBusy används för att låsa knappen.
    public bool IsBusy => busy;

    public async Task SaveAsync()
    {
        if (loading || busy)
            return;

        error = null;

        if (CustomerId is null)
        {
            error = "Välj en kund.";
            return;
        }

        if (startsAt is null)
        {
            error = "Ange startdatum.";
            return;
        }

        var request = new CreateBookingRequest
        {
            CustomerId = CustomerId.Value,
            StartsAt = startsAt.Value,
            EndsAt = endsAt,
            EmployeeIds = selectedEmployees.ToList(),
            Machines = selectedMachines
                .Select(id => new CreateBookingMachineRequest { MachineId = id })
                .ToList(),
            Services = serviceHours
                .Where(h => h.Value > 0)
                .Select(h => new CreateBookingServiceRequest { ServiceId = h.Key, EstimatedHours = h.Value })
                .ToList()
        };

        busy = true;
        var (id, createError) = await BookingApiClient.CreateAsync(request);
        busy = false;

        if (id is not null)
            await OnCreated.InvokeAsync(id.Value);
        else
            error = createError;
    }
}