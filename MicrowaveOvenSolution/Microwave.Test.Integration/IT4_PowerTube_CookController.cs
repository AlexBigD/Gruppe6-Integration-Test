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
    public class IT4_PowerTube_CookController
    {
        private IDisplay _display;
        private ITimer _timer;
        private IUserInterface _UI;
        private IOutput _output;

        private ICookController _uut;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _timer = new Timer();
            

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
    }
}
