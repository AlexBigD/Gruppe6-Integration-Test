using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

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
            _cookController = new CookController(_uut, _display, _powerTube);
        }

        [Test]
        public void TimerOnTimerTick_CookControllerCallsShowTime()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            _uut.Start(4000);

            pause.WaitOne(1000);

            _display.Received().ShowTime(0, 3);
        }

        [Test]
        public void TimerOnExpire_CookControllerCallsTurnOff()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _cookController.StartCooking(50, 1);

            pause.WaitOne(1000);

            _powerTube.Received().TurnOff();
        }
    }
}
