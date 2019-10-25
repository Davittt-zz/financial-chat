using JobsityFinancialChat.Logic.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace JobsityFinancialChat.Logic.Test
{
    public class TestTokenService
    {
        [Fact]
        public void TestCreateJwsToken()
        {
            // Arrange
            var configuration = new Mock<IOptions<TokenOptions>>();

            configuration.Setup(repo => repo.Value)
                .Returns( new TokenOptions() { Secret = "Rm9OWO5aqAtOa9R53tNC", ExpirationDays = "1" });

            TokenService _tokenService = new TokenService(configuration.Object);

            // Act
            var jsonWebToken = _tokenService.GenerateJwtToken("Username", new Domain.Models.DB.ApplicationUser() {  Email="user@jobsity.com"});

            //Assert 
            Assert.True(jsonWebToken!=null);
            Assert.Equal(637077019204711081, jsonWebToken.Expiration);
        }
       
    }
}
