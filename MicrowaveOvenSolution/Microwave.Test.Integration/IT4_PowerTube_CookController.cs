using System;
using System.Diagnostics;
using System.Threading;
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
    public class IT4_PowerTube_CookController
    {
        private IDisplay _display;
        private ITimer _timer;
        private IUserInterface _UI;

        private ICookController _uut;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _UI = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _uut = new CookController(_timer, _display, _powerTube,_UI);
        }

        [Test]
        public void CookControllerStartCooking_PowerTube()
        {
            _uut.StartCooking(50, 30);

            _powerTube.Received().TurnOn(Arg.Is<int>(50));
        }

        [Test]
        public void CookControllerStopCooking_PowerTube()
        {
            _uut.Stop();

            _powerTube.Received().TurnOff();
        }

        [Test]
        public void CookControllerOnTimerExpired_PowerTube()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _uut.StartCooking(50, 0);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _powerTube.Received().TurnOff();
        }

        [Test]
        public void CookControllerOnTimerExpired_UI()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _uut.StartCooking(50, 1000);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _UI.Received().CookingIsDone();
        }

        [Test]
        public void CookControllerOnTimerTick_Display()
        {
            //ManualResetEvent pause = new ManualResetEvent(false);

            _uut.StartCooking(50, 6000);

            _timer.TimeRemaining.Returns(6000);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            _display.Received().ShowTime(0, 6);
        }
    }
}
