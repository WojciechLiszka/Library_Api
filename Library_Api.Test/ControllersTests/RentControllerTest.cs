using Library_Api.Entity;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
