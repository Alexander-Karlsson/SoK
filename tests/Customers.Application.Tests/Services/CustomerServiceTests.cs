using Customers.Application.Customers.DTOs;
using Customers.Application.Customers.Exceptions;
using Customers.Application.Customers.Services;
using Customers.Application.Tests.Mock;
using Customers.Domain.Entities;
using Customers.Domain.Enums;

namespace Customers.Application.Tests.Services;


public class CustomerServiceTests
{
    [Fact]
    public async Task CreateCustomer_WhenCustomerNumberAlreadyExists_ThrowException()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        mockRepo.ExistingCustomer = new Customer
        {
            CustomerNumber = "CUST-001",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };
        
        var request = new CreateCustomerRequest
        {
            CustomerNumber = "CUST-001",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Erik",
            LastName = "Eriksson",
            Address = "Kungsgatan 5",
            PostalCode = "46130",
            City = "Trollhättan",
            Phone = "0709876543",
            Email = "erik@example.com"
        };
        
        // Act -----
        Func<Task> act = () => sut.CreateCustomerAsync(request);
        
        // Assert -----
        await Assert.ThrowsAsync<CustomerAlreadyExistsException>(act);
    }

    [Fact]
    public async Task CreateCustomer_WhenCustomerNumberIsUnique_ReturnsCreatedCustomer()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var request = new CreateCustomerRequest
        {
            CustomerNumber = "CUST-002",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Erik",
            LastName = "Svensson",
            Address = "Kungsgatan 5",
            PostalCode = "46130",
            City = "Uddevalla",
            Phone = "0709876543",
            Email = "erik@example.com"
        };
        
        // Act -----
        var result = await sut.CreateCustomerAsync(request);
        
        // Assert -----
        Assert.NotNull(result);
        Assert.Equal(request.CustomerNumber, result.CustomerNumber);
        Assert.Equal(request.FirstName, result.FirstName);
        Assert.Equal(request.LastName, result.LastName);
    }
    
    [Fact]
    public async Task GetById_WhenCustomerExists_ReturnsCustomer()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var customer = new Customer
        {
            CustomerNumber = "CUST-003",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };

        mockRepo.ExistingCustomer = customer;

        // Act -----
        var result = await sut.GetByIdAsync(customer.Id);

        // Assert -----
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.CustomerNumber, result.CustomerNumber);
    }
    
    [Fact]
    public async Task GetById_WhenCustomerDoesNotExist_ReturnsNull()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var nonExistingCustomerId = Guid.NewGuid();

        // Act -----
        var result = await sut.GetByIdAsync(nonExistingCustomerId);

        // Assert -----
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetByCustomerNumber_WhenCustomerExists_ReturnsCustomer()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var customer = new Customer
        {
            CustomerNumber = "CUST-005",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Erik",
            LastName = "Svensson",
            Address = "Kungsgatan 5",
            PostalCode = "46130",
            City = "Uddevalla",
            Phone = "0709876543",
            Email = "erik@example.com"
        };

        mockRepo.ExistingCustomer = customer;

        // Act -----
        var result = await sut.GetByCustomerNumberAsync("CUST-005");

        // Assert -----
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.CustomerNumber, result.CustomerNumber);
    }
    
    [Fact]
    public async Task GetByCustomerNumber_WhenCustomerDoesNotExist_ReturnsNull()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        mockRepo.ExistingCustomer = new Customer
        {
            CustomerNumber = "CUST-005",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Erik",
            LastName = "Svensson",
            Address = "Kungsgatan 5",
            PostalCode = "46130",
            City = "Uddevalla",
            Phone = "0709876543",
            Email = "erik@example.com"
        };

        // Act -----
        var result = await sut.GetByCustomerNumberAsync("CUST-999");

        // Assert -----
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Update_WhenCustomerDoesNotExist_ReturnsNull()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var id = Guid.NewGuid();

        var request = new UpdateCustomerRequest
        {
            Address = "Storgatan 10",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };

        // Act -----
        var result = await sut.UpdateAsync(id, request);

        // Assert -----
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Update_WhenCustomerExists_ReturnsUpdatedCustomer()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var customer = new Customer
        {
            CustomerNumber = "CUST-001",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };

        mockRepo.ExistingCustomer = customer;
        
        var request = new UpdateCustomerRequest
        {
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0709999999",
            Email = "anna@example.com"
        };
        
        // Act -----
        var result =  await sut.UpdateAsync(customer.Id, request);
        
        // Assert -----
        Assert.NotNull(result);
        Assert.Equal("0709999999", result.Phone);
    }
    
    [Fact]
    public async Task Update_WhenFirstNameIsMissing_ThrowsArgumentException()
    {
        // Arrange -----
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var customer = new Customer
        {
            CustomerNumber = "CUST-001",
            CustomerType = CustomerType.PrivatePerson,
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };

        mockRepo.ExistingCustomer = customer;

        var request = new UpdateCustomerRequest
        {
            FirstName = null,
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0709999999",
            Email = "anna@example.com"
        };

        // Act -----
        Func<Task> act = () => sut.UpdateAsync(customer.Id, request);

        // Assert -----
        var exception = await Assert.ThrowsAsync<ArgumentException>(act);

        Assert.Equal(
            "Förnamn och efternamn är obligatoriskt.",
            exception.Message);
    }
    
    [Fact]
    public async Task GetAll_WhenCustomersExist_ReturnsAllCustomers()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        mockRepo.Customers.Add(new Customer
        {
            CustomerNumber = "CUST-001",
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        });

        mockRepo.Customers.Add(new Customer
        {
            CustomerNumber = "CUST-002",
            FirstName = "Erik",
            LastName = "Eriksson",
            Address = "Kungsgatan 2",
            PostalCode = "46130",
            City = "Trollhättan",
            Phone = "0709876543",
            Email = "erik@example.com"
        });

        // Act
        var result = await sut.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.CustomerNumber == "CUST-001");
        Assert.Contains(result, c => c.CustomerNumber == "CUST-002");
    }
    
    [Fact]
    public async Task GetAll_WhenNoCustomersExist_ReturnsEmptyCollection()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        // Act
        var result = await sut.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task SearchByName_WhenCustomersMatch_ReturnsMatchingCustomers()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        mockRepo.SearchResults.Add(new Customer
        {
            CustomerNumber = "CUST-001",
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        });

        // Act
        var result = await sut.SearchByNameAsync("Anna");

        // Assert
        Assert.Single(result);
        Assert.Equal("CUST-001", result.First().CustomerNumber);
        Assert.Equal("Anna", result.First().FirstName);
    }
    
    [Fact]
    public async Task SearchByName_WhenNoCustomersMatch_ReturnsEmptyCollection()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        // Act
        var result = await sut.SearchByNameAsync("Korvgöran");

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task Delete_WhenCustomerExists_ReturnsTrue()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var customer = new Customer
        {
            CustomerNumber = "CUST-001",
            FirstName = "Anna",
            LastName = "Andersson",
            Address = "Storgatan 1",
            PostalCode = "46230",
            City = "Vänersborg",
            Phone = "0701234567",
            Email = "anna@example.com"
        };

        mockRepo.Customers.Add(customer);

        // Act
        var result = await sut.DeleteAsync(customer.Id);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task Delete_WhenCustomerDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var mockRepo = new MockCustomerRepository();
        var sut = new CustomerService(mockRepo);

        var nonExistingId = Guid.NewGuid();

        // Act
        var result = await sut.DeleteAsync(nonExistingId);

        // Assert
        Assert.False(result);
    }
}