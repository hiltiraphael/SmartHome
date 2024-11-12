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
    public class MarkiseTests
    {
        private Mock<IWettersensor> _sensorMock;

        [TestInitialize]
        public void Setup()
        {
            _sensorMock = new Mock<IWettersensor>();
        }

        [TestMethod]
        public void Markise_Ausgefahren_WennAussentemperaturHoeherWindschwindikeitTiefKeinRegen()
        {
            // Arrange 
            var raum = new Raum() { DeffinierteZimmertemperatur = 18};
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(22);
            _sensorMock.Setup(s => s.Regen).Returns(false);
            _sensorMock.Setup(s => s.Windgeschwindigkeit).Returns(29);
            var markise = new Markise(_sensorMock.Object);

            // Act
            markise.Trigger(raum);

            // Assert
            Assert.AreEqual(markise.Status, Markise.MarkisenStatus.ausgefahren);
        }


        [TestMethod]
        public void Markise_Eingefahren_WennAussentemperaturTieferWindschwindikeitTiefKeinRegen()
        {
            // Arrange 
            var raum = new Raum() { DeffinierteZimmertemperatur = 18 };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(17);
            _sensorMock.Setup(s => s.Regen).Returns(false);
            _sensorMock.Setup(s => s.Windgeschwindigkeit).Returns(29);
            var markise = new Markise(_sensorMock.Object);

            // Act
            markise.Trigger(raum);

            // Assert
            Assert.AreEqual(markise.Status, Markise.MarkisenStatus.eingefahren);
        }

        [TestMethod]
        public void Markise_Eingefahren_WennAussentemperaturHoeherWindschwindikeitHochKeinRegen()
        {
            // Arrange 
            var raum = new Raum() { DeffinierteZimmertemperatur = 18 };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(22);
            _sensorMock.Setup(s => s.Regen).Returns(false);
            _sensorMock.Setup(s => s.Windgeschwindigkeit).Returns(31);
            var markise = new Markise(_sensorMock.Object);

            // Act
            markise.Trigger(raum);

            // Assert
            Assert.AreEqual(markise.Status, Markise.MarkisenStatus.eingefahren);
        }

        [TestMethod]
        public void Markise_Eingefahren_WennAussentemperaturHoeherWindschwindikeitTiefRegen()
        {
            // Arrange 
            var raum = new Raum() { DeffinierteZimmertemperatur = 18 };
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(22);
            _sensorMock.Setup(s => s.Regen).Returns(true);
            _sensorMock.Setup(s => s.Windgeschwindigkeit).Returns(29);
            var markise = new Markise(_sensorMock.Object);

            // Act
            markise.Trigger(raum);

            // Assert
            Assert.AreEqual(markise.Status, Markise.MarkisenStatus.eingefahren);
        }
    }
}