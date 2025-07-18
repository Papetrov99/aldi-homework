using Aldi.Library.Api.Models.Entities;
using Aldi.Library.Api.Models.Exceptions.Common;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Aldi.Library.Tests;

public class BookServiceTests
{
    private readonly BookService _sut;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();

        _sut = new BookService(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Delete_WhenBookIsLoaned_ThrowsConflictException()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var loanedBook = new Book
        {
            Id = bookId,
            Title = "Test Book",
            Author = "Test Author",
            ISBN = "1234567890",
            PublishedYear = 2020,
            IsAvailable = false,
            Loans = new List<Loan> { }
        };

        _bookRepositoryMock
            .Setup(repo => repo.Get(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loanedBook);

        // Act
        var act = async () => await _sut.Delete(bookId);

        // Assert
        await act.Should().ThrowExactlyAsync<ConflictException>("*not available*");
    }
}