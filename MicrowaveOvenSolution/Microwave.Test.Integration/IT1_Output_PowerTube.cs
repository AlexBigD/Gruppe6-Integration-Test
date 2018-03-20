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
    public class IT1_Output_PowerTube
    {
        private IOutput _output;
        private IPowerTube _uut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _uut = new PowerTube(_output);
        }

        [Test]
        public void PowerTubeTurnOn_OutputLine()
        {
            _uut.TurnOn(50);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 50")));
        }

        [Test]
        public void PowerTubeTurnOff_OutputLine()
        {
            _uut.TurnOn(50);
            _uut.TurnOff();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube turned off")));
        }
    }
}
