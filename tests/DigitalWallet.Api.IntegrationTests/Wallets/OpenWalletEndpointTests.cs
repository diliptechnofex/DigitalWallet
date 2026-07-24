using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DigitalWallet.Api.IntegrationTests.Wallets
{
    public sealed class OpenWalletEndpointTests( DigitalWalletApiFactory factory): IClassFixture<DigitalWalletApiFactory>
    {
        [Fact]
        public async Task PostWallets_ValidRequest_ReturnsCreated()
        {
            var client = factory.CreateClient();

            var request = new
            {
                CustomerId = Guid.NewGuid(),
                Currency = "AUD"
            };

            var response = await client.PostAsJsonAsync(
                "/api/wallets",
                request);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            Assert.Equal(
                "AUD",
                json.GetProperty("currency").GetString());

            Assert.Equal(
                "PendingActivation",
                json.GetProperty("status").GetString());

            Assert.True(
                response.Headers.Location is not null);
        }

        [Fact]
        public async Task PostWallets_InvalidCurrency_ReturnsBadRequestProblemDetails()
        {
            var client = factory.CreateClient();

            var request = new
            {
                CustomerId = Guid.NewGuid(),
                Currency = "12"
            };

            var response = await client.PostAsJsonAsync(
                "/api/wallets",
                request);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);

            var problemDetails =
                await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.NotNull(problemDetails);
            Assert.Equal(400, problemDetails.Status);
            Assert.Equal("Validation error", problemDetails.Title);
        }

        [Fact]

        public async Task PostWallets_DuplicateCustomerCurrency_ReturnsConflictProblemDetails()
        {
            var client = factory.CreateClient();

            var customerId = Guid.NewGuid();

            var request = new
            {
                CustomerId = customerId,
                Currency = "AUD"
            };

            var firstResponse = await client.PostAsJsonAsync(
                "/api/wallets",
                request);

            Assert.Equal(
                HttpStatusCode.Created,
                firstResponse.StatusCode);

            var secondResponse = await client.PostAsJsonAsync(
                "/api/wallets",
                request);

            Assert.Equal(
                HttpStatusCode.Conflict,
                secondResponse.StatusCode);

            var problemDetails =
                await secondResponse.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.NotNull(problemDetails);
            Assert.Equal(409, problemDetails.Status);
            Assert.Equal("Conflict", problemDetails.Title);
        }
    }
}
