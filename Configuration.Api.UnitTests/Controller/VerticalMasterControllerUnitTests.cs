using Configuration.Api.Controllers;
using Configuration.Repository.VerticalInfoSetup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Configuration.Api.UnitTests.Controller
{
    [TestClass]
    public class VerticalMasterControllerUnitTests
    {
        private readonly Mock<IVerticalService> _repositoryVerticalMoq;
        public VerticalMasterControllerUnitTests()
        {
            _repositoryVerticalMoq=new Mock<IVerticalService>();
        }

        [TestMethod]
        public async Task GetAllActiveVerticalSBUs_WhenCalled_ShouldReturn200Status()
        {
            //Arrange
            VerticalMasterController sut = new VerticalMasterController(_repositoryVerticalMoq.Object);

            //Act
            var result = (OkObjectResult)await sut.GetVerticalList(true);

            //Assert
            result.StatusCode.Equals(200);
        }

    }
}
