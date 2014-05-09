using System;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Collections.Generic;
using System.Threading;


namespace SpeechRecognition
{
    class Program
    {
        public Program() { }

        static void Main(string[] args)
        {

            //Ask how many users to add and ask for info 
            Registration.initialization();


            //Capture Audio Data different class for all users (Initialization) -> Store audio file

            //Create class for log in

            //Ask user to speak password

            //Check for correct password as well as for correct user voice (DTW or Gaussian)

            //If login successful give access to SpeechRecognitionInterface
            Interaction.start();

        } // END OF MAIN
        

    }
}