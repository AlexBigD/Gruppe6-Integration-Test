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
    public class IT2_Output_Light
    {
        private IOutput _output;
        private ILight _uut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _uut = new Light(_output);
        }

        [Test]
        public void PowerTubeTurnOn_OutputLine()
        {
            _uut.TurnOn();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void PowerTubeTurnOff_OutputLine()
        {
            _uut.TurnOn();
            _uut.TurnOff();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}
