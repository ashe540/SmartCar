using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    public class Interaction
    {
        public Interaction() {}

        public static void start()
        {
            // Create a SpeechRecognitionEngine object for the default recognizer in the en-US locale.
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            {
                // Configure the input to the speech recognizer.
                recognizer.SetInputToDefaultAudioDevice();

                Grammar interaction = Grammars.Speech();
                interaction.Name = ("Speech analyser");
                recognizer.LoadGrammarAsync(interaction);
                // Add a handler for the speech recognized event.
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Handler.recognizer_SpeechRecognized);
                recognizer.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(Handler.recognizer_SpeechHypothesized);

                // Start asynchronous, continuous speech recognition.
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.
                while (true) Console.ReadLine();
            }
        }
    }
}