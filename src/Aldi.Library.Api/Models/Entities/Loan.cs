namespace Aldi.Library.Api.Models.Entities;

public class Loan
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public User? User { get; set; }
    public required Guid BookId { get; set; }
    public Book? Book { get; set; }
    public required DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}