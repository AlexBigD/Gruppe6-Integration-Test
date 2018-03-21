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
    public class IT6_CookControllerDisplayLight_UI
    {
        private IUserInterface _uut;
        private ICookController _cookController;
        private IButton _pButton, _tButton, _scButton;

        private IDisplay _display;
        private ILight _light;
        private IDoor _door;


        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _pButton = Substitute.For<IButton>();
            _tButton = Substitute.For<IButton>();
            _scButton = Substitute.For<IButton>();
            _cookController = Substitute.For<ICookController>();
            _uut = new UserInterface(_pButton, _tButton, _scButton, _door, _display, _light, _cookController);
        }

        //CookController
        [Test]
        public void UIStartCancelButton_CookControllerStart()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _cookController.Received().StartCooking(50, 60);
        }

        [Test]
        public void UIStartCancelButton_CookControllerCancel()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _cookController.Received().Stop();
        }

        //Light
        [Test]
        public void UIDoorOpen_LightTurnOn()
        {
            _uut.OnDoorOpened(_door, EventArgs.Empty);
            _light.Received().TurnOn();
        }

        [Test]
        public void UIDoorClosed_LightTurnOff()
        {
            _uut.OnDoorOpened(_door, EventArgs.Empty);
            _uut.OnDoorClosed(_door, EventArgs.Empty);
            _light.Received().TurnOff();
        }

        [Test]
        public void UIStartCancelPressed_LightTurnOn()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _light.Received().TurnOn();
        }

        [Test]
        public void UICookingIsDone_LightTurnOff()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _uut.CookingIsDone();
            _light.Received().TurnOff();
        }


        //Display
        [Test]
        public void UIPowerPressed_DisplayShowPower()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _display.Received().ShowPower(50);
        }

        [Test]
        public void UITimePressed_DisplayShowTime()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _display.Received().ShowTime(1,0);
        }

        [Test]
        public void UICookingIsDone_DisplayClear()
        {
            _uut.OnPowerPressed(_pButton, EventArgs.Empty);
            _uut.OnTimePressed(_tButton, EventArgs.Empty);
            _uut.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _uut.CookingIsDone();
            _display.Received().Clear();
        }
    }
}
