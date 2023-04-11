using Configuration.DataTransfer.VerticalInfoSetup;
using Configuration.Repository.VerticalInfoSetup;
using Configuration.RepositoryHandler.MsSql.EF.VerticalInfoSetup;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Configuration.Api.UnitTests.Repository
{
    [TestClass]
    public class VerticalServiceUnitTests
    {
        private readonly Mock<IVerticalInfoOperations> verticalInfoOperations;
        private readonly VerticalService service;
        public VerticalServiceUnitTests()
        {
            verticalInfoOperations = new Mock<IVerticalInfoOperations>();
            service = new VerticalService(verticalInfoOperations.Object);
        }

        [TestMethod]
        public async Task GetAllVerticalList_WhenData_ReturnListOfVerticals()
        {
            //Arrange
            bool IsActive = true;
            List<Config_tblVerticalMaster> config_TblVerticalMasters = (List<Config_tblVerticalMaster>)Builder<Config_tblVerticalMaster>.CreateListOfSize(2).Build();
            verticalInfoOperations.Setup(v => v.GetVerticalList(IsActive)).ReturnsAsync(config_TblVerticalMasters);

            //Act
            var result = await service.GetVerticalList(IsActive);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Config_tblVerticalMaster>));
            Assert.AreEqual(result, config_TblVerticalMasters);
        }

        [TestMethod]
        public async Task GetAllVerticalList_WhenNoData_ReturnEmptyListOfVerticals()
        {
            //Arrange
            bool IsActive = true;
            List<Config_tblVerticalMaster> config_TblVerticalMasters = new List<Config_tblVerticalMaster>();
            verticalInfoOperations.Setup(v => v.GetVerticalList(IsActive)).ReturnsAsync(config_TblVerticalMasters);

            //Act
            var result = await service.GetVerticalList(IsActive);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Config_tblVerticalMaster>));
            Assert.AreEqual(result, config_TblVerticalMasters);
        }
    }
}
