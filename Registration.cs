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

        public User currentUser;

        public Registration() { }

        public User getCurrentUser()
        {
            return this.currentUser;
        }
        /**
         * Use synthesizer to ask user his name or age
         */
        public static string askUserForData(string question, Choices choices)
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

        public void initialization()
        {
            if (!File.Exists("userInfo.txt"))
            {
                String users = askUserForData("How many users do you want to add to the system?", ToChoose.numbers);

                int numUsers;
                Dict.numDictionary.TryGetValue(users, out numUsers);
     
                System.IO.StreamWriter file = new System.IO.StreamWriter("userInfo.txt", false);

                try
                {
                    for (int i = 1; i <= numUsers; i++)
                    {
                        //Ask user name return string + confirm
                        string name = "";


                        if (i == 1) askUserForData("What's your name?", ToChoose.names);
                        else askUserForData("What is the name of user" + i + "?", ToChoose.names);

                        name = userText;

                        //ask user age + confirm
                        if (i == 1) askUserForData("How old are you?", ToChoose.numbers);
                        else askUserForData("How old is " + name + "?", ToChoose.numbers);

                        string ageStr = userText;
                        int age;
                        Dict.numDictionary.TryGetValue(ageStr, out age);

                        if (i == 1)
                        {
                            currentUser = new User(name, age);
                            currentUser.DateOfBirth();
                        }

                        file.WriteLine(currentUser.Name + "," + currentUser.Age + "\n");
                    }
                }
                catch (Exception superbogus) { Console.WriteLine("Superbogus Exception detected: " + superbogus.StackTrace); }
                finally { file.Close(); }
            }
            else
            {
                //If file exists then ask for log in name and assign to currentUser
                askUserForData("What is your name?", ToChoose.names);
                User user = getUserFromDB(userText);

                if (user != null) currentUser = user;
            }
        }

        public static User getUserFromDB(String name)
        {
            String line;
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("userInfo.txt");
            while ((line = file.ReadLine()) != null)
            {
                String[] array = line.Split(',');
                if (array[0].Equals(name))
                {
                    int age;
                    int.TryParse(array[1], out age);
                    return new User(name, age);
                }
            }
            return null;
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