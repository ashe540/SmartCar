using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    public class Functionalities
    {
        public Functionalities(){}

        public static void RadioFunctionalities(string[] input)
        {
            bool invalid = true;
            CorrectText("Radio functionalities");
            switch (input[1])
            {
                case "on":
                    invalid = false;
                    break;
                case "off":
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
