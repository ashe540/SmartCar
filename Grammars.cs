﻿using System;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    class Grammars
    {
        public Grammars() { }

        public static Grammar Speech()
        {
            Choices choices = new Choices(new string[] { "Radio", "Air_Condition", "Navigation", "Phone", "Car" });
            Choices second_choices = new Choices(new string[] { "warmer", "hotter", "colder", "on", "off", "next", "louder", "quieter", "previous", "silent", "out", "random", "shuffle"
                                                                    + "accept", "decline", "call", "hangup"});
            Choices third_choices = new Choices(new string[] { "one", "two", "three", "four", "twenty_one", " " });

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

        public static Grammar Confirm()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = new System.Globalization.CultureInfo("en-US");
            gb.Append(new SemanticResultKey("text", new Choices(new string[] { "Yes", "Yep", "Yeah", "No", "Nope" })));

            // Create a Grammar object and load it to the recognizer.
            Grammar g = new Grammar(gb);
            g.Name = ("Text");

            g.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Handler.recognizer_SpeechRecognized2);
            return g;
        }
    }
}
