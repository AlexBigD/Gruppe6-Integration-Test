using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_CookController_Timer
    {
        private IDisplay _display;
        private ICookController _cookController;
        private IUserInterface _UI;
        private IOutput _output;

        private ITimer _uut;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _uut = new Timer();
            _UI = Substitute.For<IUserInterface>();
            _display = Substitute.For<IDisplay>();


            _powerTube = Substitute.For<IPowerTube>();
            _cookController = Substitute.For<ICookController>();
        }

        [Test]
        public void TimerStart_Cookcontroller()
        {
            _uut.Start(5);

            _cookController.Received();
        }

        [Test]
        public void CookControllerStopCooking_PowerTube()
        {
            _uut.Stop();

            _powerTube.Received().TurnOff();
        }
    }
}
