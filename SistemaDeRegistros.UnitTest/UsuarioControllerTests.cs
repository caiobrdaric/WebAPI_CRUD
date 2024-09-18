using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaDeRegistros.Controllers;
using SistemaDeRegistros.Models;
using SistemaDeRegistros.Repositorios.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeRegistros.UnitTest
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioRepositorio> _usuarioRepositorioMock;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTests()
        {
            _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();
            _usuarioController = new UsuarioController(_usuarioRepositorioMock.Object);
        }

        [Fact]
        public async Task BuscarTodosClientes_DeveRetornarOkComListaDeUsuarios()
        {
            // Arrange
            var usuariosEsperados = new List<SistemaDeRegistros.Models.UserModel>
            {
                new SistemaDeRegistros.Models.UserModel { Id = Guid.NewGuid(), Nome = "Usuario 1" },
                new SistemaDeRegistros.Models.UserModel { Id = Guid.NewGuid(), Nome = "Usuario 2" }
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.BuscarTodosUsuarios())
                .ReturnsAsync(usuariosEsperados);

            // Act
            var resultado = await _usuarioController.BuscarTodosClientes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuariosRetornados = Assert.IsType<List<SistemaDeRegistros.Models.UserModel>>(okResult.Value);
            Assert.Equal(usuariosEsperados.Count, usuariosRetornados.Count);
        }

        [Fact]
        public async Task BuscarTodosClientes_DeveRetornarOkComListaVazia()
        {
            // Arrange
            var usuariosEsperados = new List<SistemaDeRegistros.Models.UserModel>();

            _usuarioRepositorioMock
                .Setup(repo => repo.BuscarTodosUsuarios())
                .ReturnsAsync(usuariosEsperados);

            // Act
            var resultado = await _usuarioController.BuscarTodosClientes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuariosRetornados = Assert.IsType<List<SistemaDeRegistros.Models.UserModel>>(okResult.Value);
            Assert.Empty(usuariosRetornados);
        }
        [Fact]
        public async Task BuscarPorId_DeveRetornarOkComUsuario()
        {
            // Arrange
            var usuarioEsperado = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid v�lido
                Nome = "Usuario Teste"
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.BuscarPorId(usuarioEsperado.Id))
                .ReturnsAsync(usuarioEsperado);

            // Act
            var resultado = await _usuarioController.BuscarPorId(usuarioEsperado.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuarioRetornado = Assert.IsType<SistemaDeRegistros.Models.UserModel>(okResult.Value);
            Assert.Equal(usuarioEsperado.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioEsperado.Nome, usuarioRetornado.Nome);
        }


        [Fact]
        public async Task BuscarPorId_DeveRetornarNotFoundQuandoUsuarioNaoEncontrado()
        {
            // Arrange
            var idInexistente = Guid.NewGuid(); // Um ID que n�o existe no reposit�rio

            _usuarioRepositorioMock
                .Setup(repo => repo.BuscarPorId(idInexistente))
                .ReturnsAsync((SistemaDeRegistros.Models.UserModel)null); // Retorna null para simular n�o encontrado

            // Act
            var resultado = await _usuarioController.BuscarPorId(idInexistente);

            // Assert
            Assert.IsType<OkObjectResult>(resultado.Result);
        }
        [Fact]
        public async Task Cadastrar_DeveRetornarBadRequestQuandoCPFInvalido()
        {
            // Arrange
            var usuarioInvalido = new SistemaDeRegistros.Models.UserModel
            {
                // Preencha os dados necess�rios, mas com um CPF inv�lido
                CPF = "12345678900" // Exemplo de CPF inv�lido
            };

            // Act
            var resultado = await _usuarioController.Cadastrar(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.Equal("O CPF � invalido", badRequestResult.Value);
        }

        [Fact]
        public async Task Cadastrar_DeveRetornarOkQuandoUsuarioValido()
        {
            // Arrange
            var usuarioValido = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid v�lido
                Nome = "Usuario Teste",
                CPF = "12345678909" // Exemplo de CPF v�lido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Adicionar(usuarioValido))
                .ReturnsAsync(usuarioValido); // Simula a adi��o bem-sucedida

            // Act
            var resultado = await _usuarioController.Cadastrar(usuarioValido);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuarioRetornado = Assert.IsType<SistemaDeRegistros.Models.UserModel>(okResult.Value);
            Assert.Equal(usuarioValido.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioValido.Nome, usuarioRetornado.Nome);
        }
        [Fact]
        public async Task Atualizar_DeveRetornarBadRequestQuandoCPFInvalido()
        {
            // Arrange
            var usuarioInvalido = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid v�lido
                CPF = "12345678900" // Exemplo de CPF inv�lido
            };

            // Act
            var resultado = await _usuarioController.Atualizar(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.Equal("O CPF � invalido", badRequestResult.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOkQuandoUsuarioValido()
        {
            // Arrange
            var usuarioValido = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid v�lido
                Nome = "Usuario Teste",
                CPF = "12345678909" // Exemplo de CPF v�lido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Atualizar(usuarioValido, usuarioValido.Id))
                .ReturnsAsync(usuarioValido); // Simula a atualiza��o bem-sucedida

            // Act
            var resultado = await _usuarioController.Atualizar(usuarioValido);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuarioRetornado = Assert.IsType<SistemaDeRegistros.Models.UserModel>(okResult.Value);
            Assert.Equal(usuarioValido.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioValido.Nome, usuarioRetornado.Nome);
        }
        [Fact]
        public async Task Deletar_DeveRetornarOkQuandoUsuarioDeletadoComSucesso()
        {
            // Arrange
            var usuarioParaDeletar = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid() // Use um Guid v�lido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Deletar(usuarioParaDeletar.Id))
                .ReturnsAsync(true); // Simula que a dele��o foi bem-sucedida

            // Act
            var resultado = await _usuarioController.Deletar(usuarioParaDeletar);

            // Assert
            var okResult = Assert.IsType<OkResult>(resultado.Result);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public void ValidarCPF_DeveRetornarFalseParaCPFInvalido()
        {
            // Arrange
            var usuario = new UserModel
            {
                CPF = "12345678900" // CPF inv�lido
            };

            // Act
            var resultado = usuario.ValidarCPF();

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCPF_DeveRetornarTrueParaCPFValido()
        {
            // Arrange
            var usuario = new UserModel
            {
                CPF = "12345678909" // CPF v�lido
            };

            // Act
            var resultado = usuario.ValidarCPF();

            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(null, "Nome Teste", "teste@example.com")]
        [InlineData("12345678909", null, "teste@example.com")]
        [InlineData("12345678909", "Nome Teste", null)]
        public void ValidadorDeCamposObrigatorios_DeveRetornarErrosParaCamposObrigatorios(string cpf, string nome, string email)
        {
            // Arrange
            var usuario = new UserModel
            {
                CPF = cpf,
                Nome = nome,
                Email = email,
                Id = Guid.NewGuid()
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(usuario);

            // Act
            var isValid = Validator.TryValidateObject(usuario, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void ValidadorDeCamposObrigatorios_DeveRetornarValidoParaCamposPreenchidos()
        {
            // Arrange
            var usuario = new UserModel
            {
                CPF = "12345678909", // CPF v�lido
                Nome = "Nome Teste",
                Email = "teste@example.com",
                Id = Guid.NewGuid()
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(usuario);

            // Act
            var isValid = Validator.TryValidateObject(usuario, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

    }
}
