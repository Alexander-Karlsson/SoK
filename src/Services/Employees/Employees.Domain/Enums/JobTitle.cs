using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Enums;

public enum JobTitle
{
    // Operations
    MachineOperator = 1,        // Maskinförare
    ExcavatorOperator = 2,      // Grävmaskinist
    CraneOperator = 3,          // Kranförare
    Groundworker = 4,           // Anläggningsarbetare
    Supervisor = 5,             // Arbetsledare
    Mechanic = 6,               // Mekaniker

    // Logistics
    Driver = 7,                 // Chaufför
    WarehouseWorker = 8,        // Lagerarbetare
    TransportCoordinator = 9,   // Transportledare

    // Kontor
    CustomerServiceAgent = 10,  // Kundtjänst
    SalesRepresentative = 11,   // Säljare
    Accountant = 12,            // Ekonom
    MarketingSpecialist = 13,   // Marknadsförare
    HrSpecialist = 14,          // HR-specialist
    Developer = 15,              // Utvecklare

    // Övrigt
    Other = 16
}
