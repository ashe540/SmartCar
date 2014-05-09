using System;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Collections.Generic;
using System.Threading;


namespace SpeechRecognition
{
    public class Registration
    {

        static string userText;
        static string confirmationText;
        static Boolean textRecognized = false;
        static Boolean confirmation = false;

        static Choices names = new Choices(new string[] { "Eduardo", "Ivan", "Anti", "Simin", "Evelina", "Kristian", "Timofei", "Leevi", "Christian", "Stephan", "Javier", "Sebastian", "Rico", "Andre", "Carlos", "Nerea", "Francisco", "Lorenzo", "Santiago", "Adria", "Miguel", "Victor", "Guillermo", "Carlotta", "Francesca", "Paolo", "Andi", "Giulia", "Paco" });
        static Choices numbers = new Choices(new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four", "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine", "thirty", "thirty one", "thirty two", "thirty three", "thirty four", "thirty five", "thirty six", "thirty seven", "thirty eight", "thirty nine", "forty ", "forty one", "forty two", "forty three", "forty four", "forty five", "forty six", "forty seven", "forty eight", "forty nine", "fifty", "fifty one", "fifty two", "fifty three", "fifty four", "fifty five", "fifty six", "fifty seven", "fifty eight", "fifty nine", "sixty", "sixty one", "sixty two", "sixty three", "sixty four", "sixty five", "sixty six", "sixty seven", "sixty eight", "sixty nine", "seventy", "seventy one", "seventy two", "seventy three", "seventy four", "seventy five", "seventy six", "seventy seven", "seventy eight", "seventy nine", "eighty", "eighty one", "eighty two", "eighty three", "eighty four", "eighty five", "eighty six", "eighty seven", "eighty eight", "eighty nine", "ninety", "ninety one", "ninety two", "ninety three", "ninety four", "ninety five", "ninety six", "ninety seven", "ninety eight", "ninety nine", "one hundred" });
        static Dictionary<String, int> numDictionary;


        /**
         * Use synthesizer to ask user his name or age
         */
        static string askUserForData(string question, Choices choices)
        {
            try
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.Speak(question);

                // Create a SpeechRecognitionEngine object for the default recognizer in the en-US locale.
                using (
                SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
                {
                    // Configure the input to the speech recognizer.
                    recognizer.SetInputToDefaultAudioDevice();


                    GrammarBuilder gb = new GrammarBuilder();
                    gb.Culture = new System.Globalization.CultureInfo("en-US");

                    gb.Append(new SemanticResultKey("text", choices));


                    GrammarBuilder restart = new GrammarBuilder();
                    restart.Culture = new System.Globalization.CultureInfo("en-US");
                    restart.AppendWildcard();
                    restart.Append(new SemanticResultKey("Restart", "Restart configuration"));

                    // Create a Choices for the two alternative phrases, convert the Choices
                    // to a GrammarBuilder, and construct the grammar from the result.

                    Choices bothg = new Choices(new GrammarBuilder[] { gb, restart });
                    GrammarBuilder bf = new GrammarBuilder(bothg);
                    bf.Culture = new System.Globalization.CultureInfo("en-US");

                    // Create a Grammar object and load it to the recognizer.
                    Grammar g = new Grammar(bf);
                    g.Name = ("Names");
                    recognizer.LoadGrammarAsync(g);

                    // Add a handler for the speech recognized event.
                    recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                    resetControlVariables();


                    // Start asynchronous, continuous speech recognition.
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);

                    // Keep the console window open.
                    while (!textRecognized) ;

                    recognizer.SpeechRecognized -= recognizer_SpeechRecognized;

                    Boolean restartConfig = false;

                    if (userText.Contains("restart")) restartConfig = true;
                    else synth.Speak("Is" + userText + " correct?");

                    if (restartConfig) ;
                    else if (confirmSelection()) return userText;
                    else return askUserForData(question, choices);

                } //END OF USING SPEECH RECOGNIZER
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.StackTrace.ToString());
            }
            return userText;

        }

        public static void initialization()
        {


            if (!File.Exists("userInfo.txt"))
            {

                numDictionary = new Dictionary<String, int>() { { "zero", 0 }, { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }, { "ten", 10 }, { "eleven", 11 }, { "twelve", 12 }, { "thirteen", 13 }, { "fourteen", 14 }, { "fifteen", 15 }, { "sixteen", 16 }, { "seventeen", 17 }, { "eighteen", 18 }, { "nineteen", 19 }, { "twenty", 20 }, { "twenty one", 21 }, { "twenty two", 22 }, { "twenty three", 23 }, { "twenty four", 24 }, { "twenty five", 25 }, { "twenty six", 26 }, { "twenty seven", 27 }, { "twenty eight", 28 }, { "twenty nine", 29 }, { "thirty", 30 }, { "thirty one", 31 }, { "thirty two", 32 }, { "thirty three", 33 }, { "thirty four", 34 }, { "thirty five", 35 }, { "thirty six", 36 }, { "thirty seven", 37 }, { "thirty eight", 38 }, { "thirty nine", 39 }, { "forty ", 40 }, { "forty one", 41 }, { "forty two", 42 }, { "forty three", 43 }, { "forty four", 44 }, { "forty five", 45 }, { "forty six", 46 }, { "forty seven", 47 }, { "forty eight", 48 }, { "forty nine", 49 }, { "fifty", 50 }, { "fifty one", 51 }, { "fifty two", 52 }, { "fifty three", 53 }, { "fifty four", 54 }, { "fifty five", 55 }, { "fifty six", 56 }, { "fifty seven", 57 }, { "fifty eight", 58 }, { "fifty nine", 59 }, { "sixty", 60 }, { "sixty one", 61 }, { "sixty two", 62 }, { "sixty three", 63 }, { "sixty four", 64 }, { "sixty five", 65 }, { "sixty six", 66 }, { "sixty seven", 67 }, { "sixty eight", 68 }, { "sixty nine", 69 }, { "seventy", 70 }, { "seventy one", 71 }, { "seventy two", 72 }, { "seventy three", 73 }, { "seventy four", 74 }, { "seventy five", 75 }, { "seventy six", 76 }, { "seventy seven", 77 }, { "seventy eight", 78 }, { "seventy nine", 79 }, { "eighty", 80 }, { "eighty one", 81 }, { "eighty two", 82 }, { "eighty three", 83 }, { "eighty four", 84 }, { "eighty five", 85 }, { "eighty six", 86 }, { "eighty seven", 87 }, { "eighty eight", 88 }, { "eighty nine", 89 }, { "ninety", 90 }, { "ninety one", 91 }, { "ninety two", 92 }, { "ninety three", 93 }, { "ninety four", 94 }, { "ninety five", 95 }, { "ninety six", 96 }, { "ninety seven", 97 }, { "ninety eight", 98 }, { "ninety nine", 99 }, { "one hundred", 100 } };

                String users = askUserForData("How many users do you want to add to the system?", numbers);

                int numUsers;
                numDictionary.TryGetValue(users, out numUsers);

                System.IO.StreamWriter file = new System.IO.StreamWriter("userInfo.txt", false);


                for (int i = 1; i <= numUsers; i++)
                {

                    //Ask user name return string + confirm
                    string name = "";


                    if (i == 1) askUserForData("What's your name?", names);
                    else askUserForData("What is the name of user" + i + "?", names);

                    name = userText;

                    //ask user age + confirm
                    if (i == 1) askUserForData("How old are you?", numbers);
                    else askUserForData("How old is " + name + "?", numbers);

                    string ageStr = userText;


                    int age;
                    numDictionary.TryGetValue(ageStr, out age);

                    file.WriteLine(name + "," + age + "\n");

                }

                file.Close();

            }

        }

        static Boolean confirmSelection()
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            recognizer.SetInputToDefaultAudioDevice();

            // Add a handler for the speech recognized event.
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = new System.Globalization.CultureInfo("en-US");
            gb.Append(new SemanticResultKey("text", new Choices(new string[] { "Yes", "Yep", "Yeah", "No", "Nope" })));

            // Create a Grammar object and load it to the recognizer.
            Grammar g = new Grammar(gb);
            g.Name = ("Text");
            recognizer.LoadGrammar(g);
            //RecognitionResult result = recognizer.Recognize();

            confirmation = true;
            textRecognized = false;

            // Start synchronous speech recognition.
            recognizer.RecognizeAsync();

            while (!textRecognized) ;

            recognizer.SpeechRecognized -= recognizer_SpeechRecognized;

            switch (confirmationText)
            {
                case "Yes":
                case "Yeah":
                case "Yep":
                    return true;
                case "No":
                case "Nope":
                    return false;
                default: return false;
            }


        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {


            if (confirmation) confirmationText = e.Result.Text;
            else userText = e.Result.Text;

            textRecognized = true;


            /*            if (e.Result.Text.Contains("Restart configuration"))
                        {
                            initialization();
                            throw SteveScumbagException();
                        }
            */
            Console.WriteLine(e.Result.Text);




        }

        //END OF WORD RECOGNITION

        static void resetControlVariables()
        {
            textRecognized = false;
            confirmation = false;
        }


        static void recognizer_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
        {
            Console.WriteLine("The new audio state is: " + e.AudioState);
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