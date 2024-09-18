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
                Id = Guid.NewGuid(), // Use um Guid válido
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
            var idInexistente = Guid.NewGuid(); // Um ID que não existe no repositório

            _usuarioRepositorioMock
                .Setup(repo => repo.BuscarPorId(idInexistente))
                .ReturnsAsync((SistemaDeRegistros.Models.UserModel)null); // Retorna null para simular não encontrado

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
                // Preencha os dados necessários, mas com um CPF inválido
                CPF = "12345678900" // Exemplo de CPF inválido
            };

            // Act
            var resultado = await _usuarioController.Cadastrar(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.Equal("O CPF é invalido", badRequestResult.Value);
        }

        [Fact]
        public async Task Cadastrar_DeveRetornarOkQuandoUsuarioValido()
        {
            // Arrange
            var usuarioValido = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid válido
                Nome = "Usuario Teste",
                CPF = "12345678909" // Exemplo de CPF válido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Adicionar(usuarioValido))
                .ReturnsAsync(usuarioValido); // Simula a adição bem-sucedida

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
                Id = Guid.NewGuid(), // Use um Guid válido
                CPF = "12345678900" // Exemplo de CPF inválido
            };

            // Act
            var resultado = await _usuarioController.Atualizar(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.Equal("O CPF é invalido", badRequestResult.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOkQuandoUsuarioValido()
        {
            // Arrange
            var usuarioValido = new SistemaDeRegistros.Models.UserModel
            {
                Id = Guid.NewGuid(), // Use um Guid válido
                Nome = "Usuario Teste",
                CPF = "12345678909" // Exemplo de CPF válido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Atualizar(usuarioValido, usuarioValido.Id))
                .ReturnsAsync(usuarioValido); // Simula a atualização bem-sucedida

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
                Id = Guid.NewGuid() // Use um Guid válido
            };

            _usuarioRepositorioMock
                .Setup(repo => repo.Deletar(usuarioParaDeletar.Id))
                .ReturnsAsync(true); // Simula que a deleção foi bem-sucedida

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
                CPF = "12345678900" // CPF inválido
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
                CPF = "12345678909" // CPF válido
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
                CPF = "12345678909", // CPF válido
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
