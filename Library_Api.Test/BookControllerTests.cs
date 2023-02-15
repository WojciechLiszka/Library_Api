using FluentAssertions;
using Library_Api.Entity;
using Library_Api.Models;
using Library_Api.Test.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Library_Api.Test
{
    public class BookControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        public BookControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<LibraryDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                        services
                         .AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                    });
                });
            _client = _factory.CreateClient();
        }
        private void SeedBook(Book book)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<LibraryDbContext>();

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        [Fact]
        public async Task CreateBook_WithValidModel_ReturnsCreated()
        {
            // arrange
            var model = new CreateBookDto()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5)
            };

            var httpContent = model.ToJsonHttpContent();

            // act
            var response = await _client.PostAsync("/api/Book", httpContent);
            // arrange
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
        [Fact]
        public async Task UpdateBook_WithValidModelAndId_ReturnsOk()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            var model = new CreateBookDto()
            {
                Tittle = "UpdatedTittle",
                Author = "UpdatedAuthor",
                PublishDate = new DateTime(2001, 10, 5)
            };
            var httpContent = model.ToJsonHttpContent();
            SeedBook(book);
            // act
            var response = await _client.PutAsync("/api/Book/" + book.Id, httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateBook_WithValidModelandInvalId_ReturnsNotFound()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            var model = new CreateBookDto()
            {
                Tittle = "UpdatedTittle",
                Author = "UpdatedAuthor",
                PublishDate = new DateTime(2001, 10, 5)
            };
            var httpContent = model.ToJsonHttpContent();
            SeedBook(book);
            // act
            var response = await _client.PutAsync("/api/Book/" + "987", httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeleteBook_WithValidid_ReturnsNoContent()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response = await _client.DeleteAsync("/api/Book/" + book.Id);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task DeleteBook_WithInValidId_ReturnsNotFound()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response = await _client.DeleteAsync("/api/Book/" + "987");
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetBookById_WithValidId_ReturnsOk()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response= await _client.GetAsync("/api/Book/"+book.Id);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetBookById_WithInValidId_ReturnsNotFound()
        {
            // arrange
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response = await _client.GetAsync("/api/Book/" + "987");
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}