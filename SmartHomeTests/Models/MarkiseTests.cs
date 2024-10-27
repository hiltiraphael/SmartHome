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
    }
}