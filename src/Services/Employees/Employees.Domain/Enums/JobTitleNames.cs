namespace Employees.Domain.Enums;

public static class JobTitleNames
{
    public static string TranslateToSwedish(this JobTitle title) => title switch
    {
        JobTitle.MachineOperator => "Maskinförare",
        JobTitle.ExcavatorOperator => "Grävmaskinist",
        JobTitle.CraneOperator => "Kranförare",
        JobTitle.Groundworker => "Anläggningsarbetare",
        JobTitle.Supervisor => "Arbetsledare",
        JobTitle.Mechanic => "Mekaniker",
        JobTitle.Driver => "Chaufför",
        JobTitle.WarehouseWorker => "Lagerarbetare",
        JobTitle.TransportCoordinator => "Transportledare",
        JobTitle.CustomerServiceAgent => "Kundtjänst",
        JobTitle.SalesRepresentative => "Säljare",
        JobTitle.Accountant => "Ekonom",
        JobTitle.MarketingSpecialist => "Marknadsförare",
        JobTitle.HrSpecialist => "HR-specialist",
        JobTitle.Developer => "Utvecklare",
        _=> "Övrigt"
    };
}
