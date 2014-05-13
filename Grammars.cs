using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    class Grammars
    {
        public Grammars() { }

        public static Grammar Speech()
        {
            GrammarBuilder choice = new GrammarBuilder();
            choice.Culture = new System.Globalization.CultureInfo("en-US");
            choice.Append(new SemanticResultKey("Choice", ToChoose.choices));
            choice.Append(new SemanticResultKey("Choice2", ToChoose.second_choices));
            choice.Append(new SemanticResultKey("Choice3", ToChoose.third_choices));
            Grammar speech = new Grammar(choice);
            speech.Name = "Choice";

            speech.SpeechRecognized += new
            EventHandler<SpeechRecognizedEventArgs>(Handler.Speech_Handler);
            return speech;
        }
    }
}
