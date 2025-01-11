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
using System.Net.Http.Headers;
using Application.DTOs.ContatoDtos;
using Docker.DotNet.Models;

namespace Application.Test.Tests
{
    [Trait("Category", "Integration")]
    public class ContatoControllerTest : IAsyncLifetime
    {

        private readonly MySqlContainer mySqlContainer;
        private readonly HttpClient client;

        public ContatoControllerTest()
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

            await dbContext.AreaSet.AddRangeAsync(new Area[] {
                new()
                {
                    Codigo = 11,
                    Regiao = RegiaoBrasil.SUDESTE,
                    SiglaEstado = "SP",
                },
                new()
                {
                    Codigo = 31,
                    Regiao = RegiaoBrasil.SUDESTE,
                    Cidades = "Belo Horizonte;Contagem;Betim;Nova Lima",
                    SiglaEstado = "MG",
                },
            });

            await dbContext.ContatoSet.AddRangeAsync(new Contato[] {
                new()
                {
                    CodigoArea = 31,
                    Email="joao@teste.com",
                    Nome = "Joao Teste da Silva",
                    Telefone = 970707070,
                },
                new()
                {
                    CodigoArea = 11,
                    Email="maria@teste.com",
                    Nome = "Maria Teste da Silva",
                    Telefone = 999999999,
                }
            });

            await dbContext.SaveChangesAsync();

            await Utils.ConfigureAuthorization(client);
        }

        [Fact]
        public async Task Should_Cadastrar_Contato_Successfully()
        {
            // Arrange
            var contato = new
            {
                nome = "Joao Teste da Silva",
                telefone = 987654321,
                email = "joao@teste.com",
                codigoArea = 11
            };
            var content = new StringContent(JsonSerializer.Serialize(contato), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/contatos", content);
            var responseData = await response.Content.ReadAsStringAsync();
            var baseResponse = Utils.DeserializerJson<BaseResponse<ContatoResponse>>(responseData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(((int)HttpStatusCode.OK), baseResponse!.Status);
            Assert.Equal(3, baseResponse!.Data!.ID);
            Assert.Equal(contato.email, baseResponse!.Data!.Email);
        }

        [Fact]
        public async Task Should_Listar_Contatos_Successfully()
        {
            // Act
            var response = await client.GetAsync("/api/contatos");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            var baseResponse = Utils.DeserializerJson<BaseResponse<ListarContatoResponse>>(responseData);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(((int)HttpStatusCode.OK), baseResponse!.Status);
            Assert.Equal(2, baseResponse!.Data!.TotalResultados);
        }

        [Fact]
        public async Task Should_Atualizar_Contato_Successfully()
        {
            // Arrange
            var contatoAtualizado = new { email = "novoemail@teste.com" };
            var content = new StringContent(JsonSerializer.Serialize(contatoAtualizado), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PatchAsync("/api/contatos/1", content);
            var responseData = await response.Content.ReadAsStringAsync();
            var baseResponse = Utils.DeserializerJson<BaseResponse<ContatoResponse>>(responseData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(((int)HttpStatusCode.OK), baseResponse!.Status);
            Assert.Equal(contatoAtualizado.email, baseResponse!.Data!.Email);
        }

        [Fact]
        public async Task Should_Excluir_Contato_Successfully()
        {
            // Act
            var response = await client.DeleteAsync("/api/contatos/2");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

    }
}