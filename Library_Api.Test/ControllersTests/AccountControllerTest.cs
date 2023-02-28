using FluentAssertions;
using Library_Api.Entity;
using Library_Api.Models;
using Library_Api.Test.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Library_Api.Test.ControllersTests
{
    public class AccountControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public AccountControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<LibraryDbContext>)); 
                        services.Remove(dbContextOptions);
                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                        services
                         .AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("Librarydb"));
                    });
                });
            _client = _factory.CreateClient();
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
        public async Task RegisterUser_ForValidRegisterUserDto_ReturnsOk()
        {
            // arrange
            var dto = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "testpassword",
                ConfirmPassword = "testpassword",
                FirstName = "John",
                LastName = "Doe",
                Nationality = "Polish",
                DateOfBirth = new DateTime(2000, 6, 1),
                RoleId = 1
            };
            var httpContent = dto.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync("api/Account/Register", httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidRegisterUserDto_ReturnsBadRequest()
        {
            // arrange
            var dto = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "testpassword",
                ConfirmPassword = "notTheSamePassword",
                FirstName = "John",
                LastName = "Doe",
                Nationality = "Polish",
                DateOfBirth = new DateTime(2000, 6, 1),
                RoleId = 1
            };
            var httpContent = dto.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync("api/Account/Register", httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}