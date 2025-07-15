namespace Aldi.Library.Api.Models.Entities;

public class User
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateTime RegisteredDate { get; set; }

    public ICollection<Loan>? Loans { get; set; }
}