using FluentAssertions;
using Library_Api.Entity;
using Library_Api.Test.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Library_Api.Test.ControllersTests
{
    public class RentControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public RentControllerTest(WebApplicationFactory<Program> factory)
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
                        .AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("Librarydb"));
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

        private void SeedUser(User user)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<LibraryDbContext>();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task RentBook_WitchValidUserAndBookId_ReturnsOk()
        {
            // arrange
            var user = new User()
            {
                Email = "Test@test.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1999, 4, 5),
                Nationality = "German",
                PasswordHash = "TestHash",
                RoleId = 1
            };
            SeedUser(user);
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            var httpcontent = user.Id.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync($"/api/Rent/{book.Id}", httpcontent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RentBook_WitchInValidUserOrBookId_ReturnsNotFound()
        {
            // arrange
            var user = new User()
            {
                Email = "Test@test.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1999, 4, 5),
                Nationality = "German",
                PasswordHash = "TestHash",
                RoleId = 1
            };
            SeedUser(user);
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            int invalidId = 997;
            var httpcontent = invalidId.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync($"/api/Rent/{1024}", httpcontent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserRents_WitchValidQueryAndUserId_ReturnsOk()
        {
            // arrange
            var user = new User()
            {
                Email = "Test@test.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1999, 4, 5),
                Nationality = "German",
                PasswordHash = "TestHash",
                RoleId = 1
            };
            SeedUser(user);
            var query = "PageNumber=1&PageSize=5";
            // act
            var response = await _client.GetAsync($"/api/Rent/User/{user.Id}?"+query);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetUserRents_WitchValidQueryAndInvalidUserId_ReturnsNotFound()
        {
            // arrange
            var user = new User()
            {
                Email = "Test@test.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1999, 4, 5),
                Nationality = "German",
                PasswordHash = "TestHash",
                RoleId = 1
            };
            SeedUser(user);
            var query = "PageNumber=1&PageSize=5";
            // act
            var response = await _client.GetAsync($"/api/Rent/User/{997}?" + query);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetUserRents_WitchInValidQueryAndValidUserId_ReturnsBadRequest()
        {
            // arrange
            var user = new User()
            {
                Email = "Test@test.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1999, 4, 5),
                Nationality = "German",
                PasswordHash = "TestHash",
                RoleId = 1
            };
            SeedUser(user);
            var query = "PageNumber=1&PageSize=8";
            // act
            var response = await _client.GetAsync($"/api/Rent/User/{user.Id}?" + query);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}