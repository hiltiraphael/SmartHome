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
    public class JalousieTests
    {
        private Mock<IWettersensor> _sensorMock;

        [TestInitialize]
        public void Setup()
        {
            _sensorMock = new Mock<IWettersensor>();
        }

        [TestMethod]
        public void Jalousie_Runtergefahren_WennAussentemperaturHoeherUndKeinePerson()
        {
            // Arrange
            var raum = new Raum { Name = "Schlafzimmer", DeffinierteZimmertemperatur = 18, IsPersonInRaum = false };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(22);
            var jalousie = new Jalousie(_sensorMock.Object);

            // Act
            jalousie.Trigger(raum);

            // Assert
            Assert.AreEqual(jalousie.Status, Jalousie.JalousienenStatus.runtergefahren);
        }

        [TestMethod]
        public void Jalousie_Hochgefahren_WennAussentemperaturHoeherUndPerson()
        {
            // Arrange
            var raum = new Raum { Name = "Schlafzimmer", DeffinierteZimmertemperatur = 18, IsPersonInRaum = true };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(22);
            var jalousie = new Jalousie(_sensorMock.Object);

            // Act
            jalousie.Trigger(raum);

            // Assert
            Assert.AreEqual(jalousie.Status, Jalousie.JalousienenStatus.hochgefahren);
        }


        [TestMethod]
        public void Jalousie_Hochgefahren_WennAussentemperaturTieferUndKeinePerson()
        {
            // Arrange
            var raum = new Raum { Name = "Schlafzimmer", DeffinierteZimmertemperatur = 18, IsPersonInRaum = false };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(16);
            var jalousie = new Jalousie(_sensorMock.Object);

            // Act
            jalousie.Trigger(raum);

            // Assert
            Assert.AreEqual(jalousie.Status, Jalousie.JalousienenStatus.hochgefahren);
        }

        [TestMethod]
        public void Jalousie_Hochgefahren_WennAussentemperaturTieferUndPerson()
        {
            // Arrange
            var raum = new Raum { Name = "Schlafzimmer", DeffinierteZimmertemperatur = 18, IsPersonInRaum = true };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(16);
            var jalousie = new Jalousie(_sensorMock.Object);

            // Act
            jalousie.Trigger(raum);

            // Assert
            Assert.AreEqual(jalousie.Status, Jalousie.JalousienenStatus.hochgefahren);
        }
    }
}