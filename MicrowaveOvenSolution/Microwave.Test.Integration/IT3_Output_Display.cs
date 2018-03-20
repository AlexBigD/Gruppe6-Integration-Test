using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT3_Output_Display
    {
        private IOutput _output;
        private IDisplay _uut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _uut = new Display(_output);
        }

        [Test]
        public void DisplayShowTime_OutputLine()
        {
            _uut.ShowTime(2,30);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 02:30")));
        }

        [Test]
        public void DisplayShowPower_OutputLine()
        {
            _uut.ShowPower(50);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 50 W")));
        }

        [Test]
        public void DisplayClear_OutputLine()
        {
            _uut.Clear();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
