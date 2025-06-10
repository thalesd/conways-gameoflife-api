using System.Net;
using Conways_GameOfLife_API.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ConwaysGameOfLife.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task ErrorMiddleware_Returns500_OnException()
        {
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseMiddleware<ErrorHandlingMiddleware>();
                    app.Run(ctx => throw new Exception("Boom"));
                });

            var client = new TestServer(hostBuilder).CreateClient();
            var response = await client.GetAsync("/");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("Internal server error");
        }
    }
}
