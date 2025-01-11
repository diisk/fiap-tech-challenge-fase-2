using System;
using System.Net;
using System.Text;
using System.Text.Json;
using Application.Exceptions;
using Application.Services;
using Application.Test.Factories;
using Application.Test.Fixtures;
using Domain.Entities;
using Domain.Enums.AreaEnums;
using Domain.Exceptions.AreaExceptions;
using Domain.Interfaces.AreaInterfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Testcontainers.MySql;
using Application.DTOs.Auth;
using Application.DTOs;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Application.Test.Tests
{
    [Trait("Category", "Integration")]
    public class AuthControllerTest : IAsyncLifetime
    {

        private readonly MySqlContainer mySqlContainer;
        private readonly HttpClient client;

        public AuthControllerTest()
        {
            mySqlContainer = MySqlContainerFactory.CreateMySqlContainer();
            client = new CustomWebApplicationFactory(mySqlContainer).CreateClient();
        }

        public async Task DisposeAsync()
        {
            await mySqlContainer.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            CryptoService cryptoService = new CryptoService();
            await mySqlContainer.StartAsync();

            using var scope = new CustomWebApplicationFactory(mySqlContainer).Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OnlyReadDbContext>();

            await dbContext.Database.MigrateAsync();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.UsuarioSet.AddAsync(new Usuario
            {
                Login = "teste",
                Senha = cryptoService.HashearSenha("teste"),
            });
            await dbContext.SaveChangesAsync();


        }



        [Fact]
        public async Task Should_Registrar_Usuario_Successfully()
        {
            // Arrange
            var usuario = new
            {
                login = "admin",
                senha = "123456"
            };
            var content = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/auth/registrar", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_Logar_Usuario_Successfully()
        {
            // Arrange
            var credenciais = new
            {
                login = "teste",
                senha = "teste"
            };
            var content = new StringContent(JsonSerializer.Serialize(credenciais), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/auth/logar", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseData = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseData); // Verifica se retorna um token ou algo do tipo
        }
    }
}