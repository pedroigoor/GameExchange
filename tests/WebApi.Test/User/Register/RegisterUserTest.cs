using CommonTestUtileties.Requests;
using GameExchange.Excptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.User.Register
{
    public  class RegisterUserTest(CustomWebApplicationFactory factory) : GameExchangeClassFixture(factory)
    {
        private readonly string method = "user";

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var response = await DoPost(method, request);

            response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await  JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();

            responseData.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);

        }
        [Fact]
        public async Task Error_email_empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var response = await DoPost(method, request);

            response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData
              .RootElement
              .GetProperty("errors")
              .EnumerateArray();

            var expectedMessage = ResourceMessagesException
               .ResourceManager
               .GetString("NAME_EMPTY", new CultureInfo("pt-BR"));

            errors.Count().ShouldBe(1);
            errors.ShouldContain(errors => errors.GetString()!.Equals(expectedMessage));

        }
    }
}
