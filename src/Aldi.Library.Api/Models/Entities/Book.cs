namespace Aldi.Library.Api.Models.Entities;

public class Book
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public required int PublishedYear { get; set; }
    public required bool IsAvailable { get; set; }

    public ICollection<Loan>? Loans { get; set; }
}
