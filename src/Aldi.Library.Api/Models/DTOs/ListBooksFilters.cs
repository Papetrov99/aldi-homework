namespace Aldi.Library.Api.Models.DTOs;

public record ListBooksFilters
{
    public bool? IsAvailable { get; init; }
    public string? Author { get; init; }
}
