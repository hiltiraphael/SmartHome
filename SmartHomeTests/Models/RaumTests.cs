using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models.Tests
{
    [TestClass()]
    public class RaumTests
    {
        private Mock<IWettersensor> _sensorMock;

        [TestInitialize]
        public void Setup()
        {
            _sensorMock = new Mock<IWettersensor>();
        }

        public void AddHeinzungsventil_ShouldAddHeizungsventil_WennRoomTypNichtWintergartenOderGarage()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wohnzimmer, _sensorMock.Object);

            // Act
            raum.AddHeinzungsventil();

            // Assert
            Assert.AreEqual(1, raum.Aktoren.Count);
            Assert.IsInstanceOfType(raum.Aktoren.First(), typeof(Heizungsventil));
        }

        [TestMethod]
        public void AddHeinzungsventil_ShouldNotAddHeizungsventil_WennRoomTypWintergarten()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wintergarten, _sensorMock.Object);

            // Act
            raum.AddHeinzungsventil();

            // Assert
            Assert.AreEqual(0, raum.Aktoren.Count);
        }

        [TestMethod]
        public void AddHeinzungsventil_ShouldNotAddHeizungsventil_WennRoomTypGarage()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Garage, _sensorMock.Object);

            // Act
            raum.AddHeinzungsventil();

            // Assert
            Assert.AreEqual(0, raum.Aktoren.Count);
        }

        [TestMethod]
        public void AddJalousienensteuerung_ShouldAddJalousiene_WennRoomTypeNichtBadOrGarage()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wohnzimmer, _sensorMock.Object);

            // Act
            raum.AddJalousienensteuerung();

            // Assert
            Assert.AreEqual(1, raum.Aktoren.Count);
            Assert.IsInstanceOfType(raum.Aktoren.First(), typeof(Jalousie));
        }

        [TestMethod]
        public void AddJalousienensteuerung_ShouldNotAddJalousiene_WennRoomTypBad()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Bad, _sensorMock.Object);

            // Act
            raum.AddJalousienensteuerung();

            // Assert
            Assert.AreEqual(0, raum.Aktoren.Count);
        }

        [TestMethod]
        public void AddJalousienensteuerung_ShouldNotAddJalousiene_WennRoomTypGarage()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Garage, _sensorMock.Object);

            // Act
            raum.AddJalousienensteuerung();

            // Assert
            Assert.AreEqual(0, raum.Aktoren.Count);
        }

        [TestMethod]
        public void AddMarkisensteuerung_ShouldAddMarkise_WennRoomTypWintergarten()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wintergarten, _sensorMock.Object);

            // Act
            raum.AddMarkisensteuerung();

            // Assert
            Assert.AreEqual(1, raum.Aktoren.Count);
            Assert.IsInstanceOfType(raum.Aktoren.First(), typeof(Markise));
        }

        [TestMethod]
        public void AddMarkisensteuerung_ShouldNotAddMarkise_WennRoomTypNichtWintergarten()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wohnzimmer, _sensorMock.Object);

            // Act
            raum.AddMarkisensteuerung();

            // Assert
            Assert.AreEqual(0, raum.Aktoren.Count);
        }

        [TestMethod]
        public void Wettersensor_PopertyChanged()
        {
            // Arrange
            var raum = new Raum(22.0m, false, RaumTyp.Wohnzimmer, _sensorMock.Object);
            var heizungsventil = new Heizungsventil(_sensorMock.Object);
            raum.Aktoren.Add(heizungsventil);
            _sensorMock.Setup(s => s.Aussentemperatur).Returns(20);

            // Act
            _sensorMock.Raise(s => s.PropertyChanged += null, new PropertyChangedEventArgs(nameof(IWettersensor.Aussentemperatur)));

            // Assert
            Assert.AreEqual(heizungsventil.Status, Heizungsventil.HeizungsventilStatus.geöffnet);
        }

    }
}