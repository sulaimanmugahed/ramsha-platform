using Ramsha.Common.Domain;
using Ramsha.Domain.Common;

namespace Ramsha.Domain.Customers.Entities;

public class Customer : Entity<CustomerId>, IEntityCreation, IEntityModification, ISoftDeletable, IUser
{

    private Customer(CustomerId id, string username, string firstName, string lastName)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }


    public static Customer Create(string username, string firstName, string lastName)
    {
        return new(
            new CustomerId(Guid.NewGuid()), username, firstName, lastName);
    }

    public void SetAddress(CustomerAddress customerAddress)
    {
        Address = customerAddress;
    }



    public CustomerId Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public string Username { get; set; }
    public CustomerAddress Address { get; private set; }
    public string? CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}
