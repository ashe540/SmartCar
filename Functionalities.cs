using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using Microsoft.Speech.Recognition;
using System.Media;
using System.IO;
using System.Speech.Synthesis;

namespace SpeechRecognition
{
    public class Functionalities
    {
        static SoundPlayer snd;

        static Boolean carOn = false;

        static String DIR = "../../Resources/Sounds/";
        static int currentSong = 0;
        static String[] music = {"wreckingball.wav","sweethomealabama.wav","gangmanstyle.wav","happy.wav"};

        public Functionalities(){}

        public static void RadioFunctionalities(string[] input)
        {

            if (carOn)
            {

                bool invalid = false;
                CorrectText("Radio functionalities");

                switch (input[1])
                {
                    case "on":
                        snd = new SoundPlayer(DIR + music[currentSong]);
                        snd.Play();
                        break;
                    case "off":
                        if (snd != null) snd.Stop();
                        break;
                    case "louder":
                        break;
                    case "quieter":
                        break;
                    case "next":
                        currentSong = (currentSong + 1) % music.Length;
                        snd.SoundLocation = DIR + music[currentSong];
                        snd.Play();
                        break;
                    case "silent":
                        break;
                    case "previous":
                        if (currentSong > 0) currentSong--;
                        else currentSong = music.Length - 1;

                        snd.SoundLocation = DIR + music[currentSong];
                        snd.Play();
                        break;
                    case "random":
                    case "shuffle":
                        Random rnd = new Random();
                        currentSong = rnd.Next(0, music.Length - 1);
                        snd.SoundLocation = DIR + music[currentSong];
                        snd.Play();
                        break;
                    default:
                        invalid = true;
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
                        SoundPlayer snd = new SoundPlayer(DIR+"engineon.wav");
                        snd.Play();
                        carOn = true;
                    }
                    break;
                case "off":
                    if (carOn)
                    {
                        SoundPlayer snd = new SoundPlayer(DIR+"engineoff.wav");
                        snd.Play();
                        carOn = false;
                    }
                    break;
            }
        }

        public static void ACFunctionalities(string[] input)
        {
            CorrectText("AC-Functionalities");
            bool invalid = false;
            switch (input[1])
            {
                case "warmer":
                    break;
                case "colder":
                    break;
                case "hotter":
                    break;
                case "on":
                    break;
                case "off":
                    break;
                default:
                    invalid = true;
                    break;
            }
            if (invalid) Invalid();
        }

        public static void NavFunctionalities(string[] input)
        {
            bool invalid = false;
            CorrectText("Nav-Functionalities");
            switch (input[1])
            {
                case "on":
                    break;
                case "off":
                    break;
                default:
                    invalid = true;
                    break;
            }
            if (invalid) Invalid();
        }

        public static void PhoneFuntionalities(string[] input)
        {
            bool invalid = false;
            CorrectText("Phone-Functionalities");
            switch (input[1])
            {
                case "accept":
                    break;
                case "decline":
                    break;
                case "call":
                    SpeechSynthesizer synth = new SpeechSynthesizer();
                    synth.Speak("Initiating call. Please wait...");
                    System.Threading.Thread.Sleep(2000);
                    snd = new SoundPlayer(DIR+"phonecall.wav");
                    snd.Play();
                    break;
                case "hangup":
                    snd.Stop();
                    break;
                default:
                    invalid = true;
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
