﻿using System;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    class ToChoose
    {
        public ToChoose() { }

            public static Choices choices = new Choices(new string[] { "Radio", "Air_Condition", "Navigation", "Phone", "Car" });

            public static Choices second_choices = new Choices(new string[] { "warmer", "hotter", "colder", "on", "off", "next", "louder", "quieter", "previous", "silent", "out", "random", "shuffle"
                                                                        + "accept", "decline", "call"});

            public static Choices third_choices = new Choices(new string[] { "one", "two", "three", "four", "twenty_one", " " });

            public static Choices names = new Choices(new string[] { "Eduardo", "Ivan", "Anti", "Simin", "Evelina", "Kristian", "Timofei", "Leevi", "Christian", "Stephan", "Javier", "Sebastian", "Rico", "Andre", "Carlos", "Nerea", "Francisco", "Lorenzo", "Santiago", "Adria", "Miguel", "Victor", "Guillermo", "Carlotta", "Francesca", "Paolo", "Andi", "Giulia", "Paco" });
            
            public static Choices numbers = new Choices(new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four", "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine", "thirty", "thirty one", "thirty two", "thirty three", "thirty four", "thirty five", "thirty six", "thirty seven", "thirty eight", "thirty nine", "forty ", "forty one", "forty two", "forty three", "forty four", "forty five", "forty six", "forty seven", "forty eight", "forty nine", "fifty", "fifty one", "fifty two", "fifty three", "fifty four", "fifty five", "fifty six", "fifty seven", "fifty eight", "fifty nine", "sixty", "sixty one", "sixty two", "sixty three", "sixty four", "sixty five", "sixty six", "sixty seven", "sixty eight", "sixty nine", "seventy", "seventy one", "seventy two", "seventy three", "seventy four", "seventy five", "seventy six", "seventy seven", "seventy eight", "seventy nine", "eighty", "eighty one", "eighty two", "eighty three", "eighty four", "eighty five", "eighty six", "eighty seven", "eighty eight", "eighty nine", "ninety", "ninety one", "ninety two", "ninety three", "ninety four", "ninety five", "ninety six", "ninety seven", "ninety eight", "ninety nine", "one hundred" });
    
            public static Choices small_numbers = new Choices(new string[] { "one", "two", "three", "four", "five"});
    }
   
}