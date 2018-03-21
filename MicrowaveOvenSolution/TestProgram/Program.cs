using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup all the objects,
            var output = new Output();
            var door = new Door();
            var pButton = new Button();
            var tButton = new Button();
            var scButton = new Button();
            var display = new Display(output);
            var light = new Light(output);
            var timer = new Timer();
            var pTube = new PowerTube(output);
            var CController = new CookController(timer, display, pTube);
            var UI = new UserInterface(tButton, tButton, scButton, door, display, light, CController);

            // Simulate user activities
            door.Open();
            door.Close();
            pButton.Press();
            tButton.Press();
            tButton.Press();
            scButton.Press();
            door.Open();

            



            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
