using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Generated;

namespace Api.IntegrationTests.Tests;

public class BooksTests : ApiTestBase
{
    [Fact]
    public async Task CreateBook_CanSuccessFullyCreateBook()
    {
        var dto = new CreateBookDto()
        {
            Author = "A",
            Genre = "A",
            Title = "A"
        };
        var result = (await new LibraryClient(Client).PostAsync(dto)).Result;
        Assert.Equivalent(result.Author, dto.Author);
        Assert.Equivalent(result.Genre, dto.Genre);
        Assert.Equivalent(result.Title, dto.Title);
        Assert.NotEqual(0, result.Id);
    }

    [Fact]
    public async Task CreateBook_FailsWhenAuthorAndGenreEmpty()
    {
        var dto = new CreateBookDto()
        {
            Author = "",
            Genre = "",
            Title = "Valid Book"
        };
        try
        {
            var response = await new LibraryClient(Client).PostAsync(dto);
            Assert.False(response.StatusCode == 200);
        }
        catch (ApiException ex)
        {
            Assert.Equal(400, ex.StatusCode);
        }
    }

    [Fact]
    public async Task CantBeLoanedWhenAlreadyLoaned()
    {
        var dto = new LoanBookDto()
        {
            BookId = 1,
            UserId = 1
        }; 
        var result = (await new LibraryClient(Client).LoanAsync(dto)).Result;
        var Seconddto = new LoanBookDto()
        {
            BookId = result.Id,
            UserId = 2
        };
        try
        {
            var response = await new LibraryClient(Client).LoanAsync(Seconddto);
            Assert.False(response.StatusCode == 200);
        }
        catch (ApiException ex)
        {
            Assert.Equal(500, ex.StatusCode);
        }
    }
}