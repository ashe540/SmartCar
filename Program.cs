using System;
using Microsoft.Speech.Recognition;


namespace SpeechRecognitionApp
{
    class Program
    {
        static void Main(string[] args)
        {
                               
            //Capture Audio Data different class for all users (Initialization)

            //Create class for log in

            //Ask user to speak password

            //Check 


            // Create a SpeechRecognitionEngine object for the default recognizer in the en-US locale.
            using (
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            {
                // Configure the input to the speech recognizer.
                recognizer.SetInputToDefaultAudioDevice();

                // Create  grammar

                Choices commands = new Choices(new string[] {"command1, command2, command3"});

                GrammarBuilder gb = new GrammarBuilder();
                gb.Culture = new System.Globalization.CultureInfo("en-US");

                gb.Append(new SemanticResultKey("commands", commands));

                GrammarBuilder Stopg = new GrammarBuilder();
                Stopg.Culture = new System.Globalization.CultureInfo("en-US");
                Stopg.AppendWildcard();
                Stopg.Append(new SemanticResultKey("Exit","Shutdown system"));

                // Create a Choices for the two alternative phrases, convert the Choices
                // to a GrammarBuilder, and construct the grammar from the result.
                
                Choices bothg = new Choices(new GrammarBuilder[] {gb, Stopg});
                GrammarBuilder bf = new GrammarBuilder(bothg);
                bf.Culture = new System.Globalization.CultureInfo("en-US");

                // Create a Grammar object and load it to the recognizer.
                Grammar g = new Grammar(bf);
                g.Name = ("Command");
                recognizer.LoadGrammarAsync(g);

                // Add a handler for the speech recognized event.
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(recognizer_SpeechHypothesized);
                recognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(recognizer_SpeechRecognitionRejected);
                recognizer.LoadGrammarCompleted += new EventHandler<LoadGrammarCompletedEventArgs>(recognizer_LoadGrammarCompleted);
                
                // Start asynchronous, continuous speech recognition.
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.
                while (true)
                    Console.ReadLine();
              
            }
        } // END OF MAIN



        // Handle the SpeechRecognized event.
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
        
            if (e.Result.Semantics.ContainsKey("Exit"))
            {
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
        
            if (e.Result.Semantics.ContainsKey("commands"))
            {
                int n = 0;

                switch(e.Result.Semantics["commands"].Value.ToString()){

                    case "command1": n = 1;
                        break;
                    case "command2": n = 2;
                        break;
                    case "command3": n = 3;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                        
                }

                Console.WriteLine("Command {0} executed", n);


            }

        } //END OF WORD RECOGNITION

        // SpeechHypothesized event.
        static void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine("Speech hypothesized: " + e.Result.Text);
        }

        static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("Speech input was rejected.");
        }
        static void recognizer_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
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