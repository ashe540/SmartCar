using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    class Handler
    {
        public Handler(){}
        
        public static void Speech_Handler(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result == null) return;

            RecognitionResult result = e.Result;
            Console.WriteLine(result);
            string[] text = result.Text.Split(new Char[] { ' ' });
            foreach (string i in text) Console.WriteLine(i);
            switch (text[0])
            {
                case "Radio":
                    Functionalities.RadioFunctionalities(text);
                    break;
                case "Car":
                    Functionalities.CarFunctionalities(text);
                    break;
                case "Air_Condition":
                    Functionalities.ACFunctionalities(text);
                    break;
                case "Navigation":
                    Functionalities.NavFunctionalities(text);
                    break;
                case "Phone":
                    Functionalities.PhoneFuntionalities(text);
                    break;
            }
        }

        // Handle the SpeechRecognized event.
        public static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
            Console.WriteLine("  Confidence score: " + e.Result.Confidence);
            Console.WriteLine();

            if (e.Result.Semantics.ContainsKey("Passphrase"))
                Console.WriteLine("  The passphrase is: " +
                     e.Result.Semantics["Passphrase"].Value);
        }

        // Handle the SpeechHypothesized event.
        public static void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine("Speech hypothesized: " + e.Result.Text);
        }

        public static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("Speech input was rejected.");
            foreach (RecognizedPhrase phrase in e.Result.Alternates)
            {
                //Console.WriteLine("  Rejected phrase: " + phrase.Text);
                Console.WriteLine("  Confidence score: " + phrase.Confidence);
                //Console.WriteLine("  Grammar name:  " + phrase.Grammar.Name);
            }
        }

        public static void recognizer_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            Console.WriteLine("The audio level is now: {0}.", e.AudioLevel);
        }

        public static void recognizer_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
        {
            Console.WriteLine("The new audio state is: " + e.AudioState);
        }

        public static bool end_of_phrase(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.InitialSilenceTimeout || e.BabbleTimeout)
            {
                Console.WriteLine(
                  "RecognizeCompleted: BabbleTimeout({0}), InitialSilenceTimeout({1}).",
                  e.BabbleTimeout, e.InitialSilenceTimeout);
                return true;
            }
            return false;
        }

        public static void recognizer_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            string grammarName = e.Grammar.Name;
            bool grammarLoaded = e.Grammar.Loaded;
            bool grammarEnabled = e.Grammar.Enabled;

            if (e.Error != null)
            {
                Console.WriteLine("LoadGrammar for {0} failed with a {1}.",
                grammarName, e.Error.GetType().Name);
                // Add exception handling code here.
            }
            Console.WriteLine("Grammar {0} {1} loaded and {2} enabled.", grammarName, (grammarLoaded) ? "is" : "is not", (grammarEnabled) ? "is" : "is not");
        }
    }
}