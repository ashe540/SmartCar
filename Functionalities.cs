using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using Microsoft.Speech.Recognition;
using System.Media;
using System.IO;

namespace SpeechRecognition
{
    public class Functionalities
    {
        static SoundPlayer snd;

        static Boolean carOn = false;

        public Functionalities(){}

        public static void RadioFunctionalities(string[] input)
        {

            if (carOn)
            {

                bool invalid = true;
                CorrectText("Radio functionalities");

                switch (input[1])
                {
                    case "on":
                        snd = new SoundPlayer("../../Resources/Sounds/wreckingball.wav");
                        snd.Play();
                        invalid = false;
                        break;
                    case "off":
                        if(snd != null) snd.Stop();
                        invalid = false;
                        break;
                    case "louder":
                        invalid = false;
                        break;
                    case "quieter":
                        invalid = false;
                        break;
                    case "next":
                        invalid = false;
                        break;
                    case "silent":
                        invalid = false;
                        break;
                    case "previous":
                        invalid = false;
                        break;
                    case "randomize":
                        invalid = false;
                        break;
                    case "shuffle":
                        invalid = false;
                        break;
                }
                if (invalid) Invalid();

            }

        }

        public static void CarFunctionalities(string[] input)
        {

            switch (input[1])
            {
                case "on":
                    if (!carOn)
                    {
                        SoundPlayer snd = new SoundPlayer("../../Resources/Sounds/engineon.wav");
                        snd.Play();
                        carOn = true;
                    }
                    break;
                case "off":
                    if (carOn)
                    {
                        SoundPlayer snd = new SoundPlayer("../../Resources/Sounds/engineoff.wav");
                        snd.Play();
                        carOn = false;
                    }
                    break;
            }
        }

        public static void ACFunctionalities(string[] input)
        {
            CorrectText("AC-Functionalities");
            bool invalid = true;
            switch (input[1])
            {
                case "warmer":
                    invalid = false;
                    break;
                case "colder":
                    invalid = false;
                    break;
                case "hotter":
                    invalid = false;
                    break;
                case "on":
                    invalid = false;
                    break;
                case "off":
                    invalid = false;
                    break;
            }
            if (invalid) Invalid();
        }

        public static void NavFunctionalities(string[] input)
        {
            bool invalid = true;
            CorrectText("Nav-Functionalities");
            switch (input[1])
            {
                case "on":
                    invalid = false;
                    break;
                case "off":
                    invalid = false;
                    break;
            }
            if (invalid) Invalid();
        }

        public static void PhoneFuntionalities(string[] input)
        {
            bool invalid = true;
            CorrectText("Phone-Functionalities");
            switch (input[1])
            {
                case "accept":
                    invalid = false;
                    break;
                case "decline":
                    invalid = false;
                    break;
                case "call":
                    invalid = false;
                    break;
            }
            if (invalid) Invalid();
        }

        public static void Invalid()
        {
            ErrorText("Invalid Input, try again");
        }

        public static void ErrorText(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CorrectText(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
