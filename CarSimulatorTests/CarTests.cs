using System;

using CarSimulator;

using FluentAssertions;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Roche.LabCore.Utilities.UnitTesting;

namespace CarSimulatorTests
{
    [TestClass]
    public class CarTests
    {
        private readonly IUnityContainer container = new UnityContainer().WithAutoMocking();

        [TestMethod]
        public void AutoDrive_WhenGpsTraceRouteToDestinationReturnsFalse_ReturnsFalse_OldManner()
        {
            //Arrange
            var engineMock = new Mock<IEngine>();
            var gpsMock = new Mock<IGps>();
            gpsMock.Setup(x => x.CanDriveToDestination()).Returns(false);

            //Act
            var sut = new Car(engineMock.Object, gpsMock.Object);
            var result = sut.AutoDrive("Londres");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AutoDrive_WhenGpsTraceRouteToDestinationReturnsFalse_ReturnsFalse()
        {
            //Arrange
            var gpsMock = this.container.RegisterMock<IGps>();
            gpsMock.Setup(x => x.CanDriveToDestination()).Returns(false);

            //Act
            var sut = this.container.Resolve<Car>();
            var result = sut.AutoDrive("Londres");

            //Assert
            result.Should().Be(false, because: "It cannot drive to this destination");
        }
    }
}
