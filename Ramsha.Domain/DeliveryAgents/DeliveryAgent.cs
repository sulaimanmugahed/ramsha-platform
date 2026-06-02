using Ramsha.Common.Domain;
using Ramsha.Domain.Common;

namespace Ramsha.Domain.DeliveryAgents;

public record DeliveryAgentId(Guid Value);

public class DeliveryAgent : Entity<DeliveryAgentId>, IEntityCreation, IEntityModification, ISoftDeletable, IUser
{

    private DeliveryAgent(DeliveryAgentId id, string username, string firstName, string lastName)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }


    public static DeliveryAgent Create(string username, string firstName, string lastName)
    {
        return new(
            new DeliveryAgentId(Guid.NewGuid()), username, firstName, lastName);
    }


    public DeliveryAgentId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
}
