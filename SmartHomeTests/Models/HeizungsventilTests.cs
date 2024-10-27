using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models.Tests
{
    [TestClass()]
    public class HeizungsventilTests
    {
        private Mock<IWettersensor> _sensorMock;

        [TestInitialize]
        public void Setup()
        {
            _sensorMock = new Mock<IWettersensor>();
        }

        [TestMethod]
        public void Heizungsventil_Geöffnet_WennAussentemperaturTiefer()
        {
            // Arrange
            var raum = new Raum { DeffinierteZimmertemperatur = 22 };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(20);
            var heizungsventil = new Heizungsventil(_sensorMock.Object);

            // Act
            heizungsventil.Trigger(raum);

            // Assert
            Assert.AreEqual(heizungsventil.Status, Heizungsventil.HeizungsventilStatus.geöffnet);
        }

        public void Heizungsventil_Geschlossen_WennAussentemperaturHöher()
        {
            // Arrange
            var raum = new Raum { DeffinierteZimmertemperatur = 22 };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(24);
            var heizungsventil = new Heizungsventil(_sensorMock.Object);

            // Act
            heizungsventil.Trigger(raum);

            // Assert
            Assert.AreEqual(heizungsventil.Status, Heizungsventil.HeizungsventilStatus.geschlossen);
        }

    }
}