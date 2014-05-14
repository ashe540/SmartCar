using System;

namespace SpeechRecognition
{
    public class Program
    {
        public Program() { }

        static void Main(string[] args)
        {
            Registration registration = new Registration();
            Dict dict = new Dict();
            Appearance appear = new Appearance();
            //Initialisation of ressources
            appear.ínit();
            dict.init();
            //Registration or Login
            registration.initialization();

            //Capture Audio Data different class for all users (Initialization) -> Store audio file or raw

            //Ask user to say his name for voice recognition

            //Check for name in "database"
            if (registration.getCurrentUser() != null)
            {
                //User is in database
                Console.WriteLine("Successfully authenticated!");

                //Check for correct password as well as for correct user voice (DTW or Gaussian)

                //If login successful give access to SpeechRecognitionInterface
                Interaction.start();

                Console.ReadLine();
            }
        } // END OF MAIN
    }
}