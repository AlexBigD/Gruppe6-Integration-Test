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
    public class IT7_IT8_UI_Door_Button
    {
        private IDoor _door;
        private IUserInterface _ui;

        
        
        private ICookController _cookController;
        private IButton _pButton, _tButton, _scButton;

        private IDisplay _display;
        private ILight _light;
       

        [SetUp]
        public void SetUp()
        {
            
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _pButton = new Button();
            _tButton = new Button();
            _scButton = new Button();
            _cookController = Substitute.For<ICookController>();
            _door = new Door();
            _ui = new UserInterface(_pButton, _tButton, _scButton, _door, _display, _light, _cookController);
            
        }

        [Test]
        public void DoorIsOpen_UserInterface()
        {
            _door.Open();
            _light.Received().TurnOn();
        }

        [Test]
        public void DoorIsClosed_UserInterface()
        {
            _door.Open();
            _door.Close();
            _light.Received().TurnOff();
        }

        [Test]
        public void BtnTimePressed_UserInterface()
        {
            _pButton.Press();
            _tButton.Press();
            _display.Received().ShowTime(1, 0);

        }

        [Test]
        public void BtnPowerPressed_UserInterface()
        {
            _pButton.Press();
            _display.Received().ShowPower(50);

        }

        [Test]
        public void BtnSCPressedStart_UserInterface()
        {
            _pButton.Press();
            _tButton.Press();
            _scButton.Press();
            _light.Received().TurnOn();

        }

        [Test]
        public void BtnSCPressedStop_UserInterface()
        {
            _pButton.Press();
            _tButton.Press();
            _scButton.Press();
            _scButton.Press();
            _light.Received().TurnOff();

        }



    }
}
