﻿using FluentAssertions;
using Library_Api.Entity;
using Library_Api.Test.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Library_Api.Test.ControllersTests
{
    public class TagControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public TagControllerTests(WebApplicationFactory<Program> factory)
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

        private void SeedTag(Tag tag)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<LibraryDbContext>();

            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task CreateTag_WitchUniqueName_ReturnsCreated()
        {
            // arrange
            var newtagname = "UniqueName";
            var httpContent = newtagname.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync($"/api/Tag", httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateTag_WitchNonUniqueName_ReturnsBadRequest()
        {
            // arrange
            var tag = new Tag()
            {
                Name = "TestName"
            };
            SeedTag(tag);
            var newtagname = "TestName";
            var httpContent = newtagname.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync($"/api/Tag", httpContent);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllTags_ReturnsOk()
        {
            // arrange
            var tag = new Tag()
            {
                Name = "TestName"
            };
            SeedTag(tag);
            // act
            var response = await _client.GetAsync($"/api/Tag");
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTagToBook_WitchValidBookAndTagId_ReturnsOK()
        {
            // arrange
            var tag = new Tag()
            {
                Name = "TestName"
            };
            SeedTag(tag);
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response = await _client.PutAsync($"api/Book/{book.Id}/Tag/{tag.Id}", null);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTagToBook_WitchinValidBookAndTagId_ReturnsNotFound()
        {
            // arrange
            var tag = new Tag()
            {
                Name = "TestName"
            };
            SeedTag(tag);
            var book = new Book()
            {
                Tittle = "TestTittle",
                Author = "TestAuthor",
                PublishDate = new DateTime(2010, 10, 5),
                IsAvailable = true,
            };
            SeedBook(book);
            // act
            var response = await _client.PutAsync($"api/Book/{999}/Tag/{976}", null);
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}