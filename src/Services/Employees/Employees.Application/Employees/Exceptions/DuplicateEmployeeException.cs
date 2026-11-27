using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Exceptions;

public class DuplicateEmployeeException(string message) : Exception(message);
