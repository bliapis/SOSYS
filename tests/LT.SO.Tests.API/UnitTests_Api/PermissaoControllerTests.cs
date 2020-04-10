using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using AutoMapper;
using LT.SO.Services.Api.Models;
using LT.SO.Services.Api.Controllers.Gerencial;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Infra.CrossCutting.Log.Interfaces;

namespace LT.SO.Tests.API.UnitTests
{
    public class PermissaoControllerTests
    {
        // AAA => Arrange, Act, Assert
        public PermissaoController permissaoController;
        public Mock<DomainNotificationHandler> mockNotification;
        public Mock<IMapper> mockMapper;
        public Mock<ITipoPermissaoService> mockTipoPermissaoService;
        public Mock<IPermissaoService> mockPermissaoService;

        public PermissaoControllerTests()
        {
            mockMapper = new Mock<IMapper>();
            mockNotification = new Mock<DomainNotificationHandler>();
            mockTipoPermissaoService = new Mock<ITipoPermissaoService>();
            mockPermissaoService = new Mock<IPermissaoService>();
            var mockBus = new Mock<IBus>();
            var mockUser = new Mock<IUser>();
            var mockAuditConfig = Options.Create(new AuditConfig());
            var mockLogService = new Mock<ILogService>();

            permissaoController = new PermissaoController(
                mockBus.Object,
                mockUser.Object,
                mockMapper.Object,
                mockAuditConfig,
                mockNotification.Object,
                mockLogService.Object,
                mockTipoPermissaoService.Object,
                mockPermissaoService.Object);
        }

        [Fact]
        public void TipoPermissao_Add_RetornarComSucesso()
        {
            // Arrange
            var tipoPermissaoViewModel = new TipoPermissaoViewModel();
            var tipoPermissaoModel = new TipoPermissaoModel("Teste");

            mockMapper.Setup(m => m.Map<TipoPermissaoModel>(tipoPermissaoViewModel)).Returns(tipoPermissaoModel);
            mockNotification.Setup(m => m.GetNotifications()).Returns(new List<DomainNotification>());

            // Act
            var result = permissaoController.TipoPermissaoAdd(tipoPermissaoViewModel);

            // Assert
            mockTipoPermissaoService.Verify(m => m.Adicionar(tipoPermissaoModel), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TipoPermissaoAdd_RetornarComErrosDeModelState()
        {
            // Arrange
            var notficationList = new List<DomainNotification> { new DomainNotification("Erro", "ModelError") };

            mockNotification.Setup(m => m.GetNotifications()).Returns(notficationList);
            mockNotification.Setup(m => m.HasNotifications()).Returns(true);

            permissaoController.ModelState.AddModelError("Erro", "ModelErro");

            // Act
            var result = permissaoController.TipoPermissaoAdd(new TipoPermissaoViewModel());

            // Assert
            mockTipoPermissaoService.Verify(m => m.Adicionar(It.IsAny<TipoPermissaoModel>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void TipoPermissaoAdd_RetornarComErrosDeDominio()
        {
            var tipoPermissaoViewModel = new TipoPermissaoViewModel();
            var tipoPermissaoModel = new TipoPermissaoModel("Teste");

            mockMapper.Setup(m => m.Map<TipoPermissaoModel>(tipoPermissaoViewModel)).Returns(tipoPermissaoModel);

            var notficationList = new List<DomainNotification> { new DomainNotification("Erro", "Erro ao adicionar novo Tipo de Permissao") };

            mockNotification.Setup(m => m.GetNotifications()).Returns(notficationList);
            mockNotification.Setup(m => m.HasNotifications()).Returns(true);

            // Act
            var result = permissaoController.TipoPermissaoAdd(tipoPermissaoViewModel);

            // Assert
            mockTipoPermissaoService.Verify(m => m.Adicionar(tipoPermissaoModel), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}