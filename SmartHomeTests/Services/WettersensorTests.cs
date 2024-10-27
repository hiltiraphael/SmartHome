using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services.Tests
{
    [TestClass()]
    public class WettersensorTests
    {
        [TestMethod]
        public void Test_Tick()
        {
            // Arrange
            var sensor = new Wettersensor();
            decimal initialTemperature = sensor.Aussentemperatur;
            decimal initialWindSpeed = sensor.Windgeschwindigkeit;

            // Act
            sensor.Tick();

            // Assert
            Assert.IsTrue(sensor.Aussentemperatur != initialTemperature ||
                          sensor.Windgeschwindigkeit != initialWindSpeed ||
                          sensor.Regen != false);
        }

        [TestMethod]
        public void Test_Tick_IstRealistisch()
        {
            // Arrange
            var sensor = new Wettersensor();
            decimal initialAussentemperatur = sensor.Aussentemperatur;
            decimal initialWindgeschwindikkeit = sensor.Windgeschwindigkeit;

            // Act
            sensor.Tick();

            // Assert
            Assert.AreEqual(initialAussentemperatur, sensor.Aussentemperatur, 0.5m);
            Assert.AreEqual(initialWindgeschwindikkeit, sensor.Windgeschwindigkeit, 2);
        }
    }
}