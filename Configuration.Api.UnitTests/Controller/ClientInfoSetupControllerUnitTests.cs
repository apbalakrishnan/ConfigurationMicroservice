using Configuration.Api.Controllers;
using Configuration.Api.UnitTests.Service;
using Configuration.DomainModel.ClientInfoSetup;
using Configuration.Repository.ClientInfoSetup;
using Configuration.RepositoryHandler.MsSql.EF.ClientInfoSetup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configuration.Api.UnitTests.Controller
{
    [TestClass]
    public class ClientInfoSetupOperationUnitTests
    {
        private readonly IClientInfoSetupOperations _repositoryClient;
        public ClientInfoSetupOperationUnitTests()
        {
            _repositoryClient = new ClientInfoSetupOperationsFake();
        }

        [TestMethod]
        public async Task Get_WhenCalled_ReturnAllItems()
        {
            var okResult = await _repositoryClient.GetSBUListbasedONClient(1);

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(List<GetBESBUInfoByClientIdResponseModel>));
        }
    }

    [TestClass]
    public class ClientInfoSetupServiceUnitTests
    {
        private readonly IClientInfoSetupOperations clientInfoOperations;
        public ClientInfoSetupServiceUnitTests()
        {
            clientInfoOperations = new ClientInfoSetupOperationsFake();
        }

        [TestMethod]
        public async Task Get_WhenCalled_ReturnAllItems()
        {
            ClientInfoSetupService clientInfoSetupService = new ClientInfoSetupService(clientInfoOperations);
            var okResult = await clientInfoSetupService.GetSBUListbasedONClient(1);

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(List<GetBESBUInfoByClientIdResponseModel>));
        }
    }

    [TestClass]
    public class ClientInfoSetupControllerUnitTests
    {
        private readonly IClientInfoSetupService _repositoryClient;
        public ClientInfoSetupControllerUnitTests()
        {
            _repositoryClient = new ClientInfoSetupService(new ClientInfoSetupOperationsFake());
        }

        [TestMethod]
        public async Task Get_WhenCalled_ReturnOkResult()
        {
            ClientInfoSetupController controller = new ClientInfoSetupController(_repositoryClient);
            var okResult = await controller.GetSBUListbasedONClient(1);

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task Get_WhenCalled_ReturnAllItems()
        {
            ClientInfoSetupController controller = new ClientInfoSetupController(_repositoryClient);
            var okResult = await controller.GetSBUListbasedONClient(1) as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<GetBESBUInfoByClientIdResponseModel>));
            // Assert.IsInstanceOfType<List<GetBESBUInfoByClientIdResponseModel>>(okResult, typeof(List<GetBESBUInfoByClientIdResponseModel>));
            // Assert.IsInstanceOfType<IEnumerable<GetBESBUInfoByClientIdResponseModel>>(okResult, typeof(IEnumerable<GetBESBUInfoByClientIdResponseModel>));
        }
    }
}
