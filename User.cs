using System;

namespace SpeechRecognition
{
    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int DateOfBirth()
        {
            return DateTime.Now.Year - Age;
        }

        public override string ToString()
        {
            string s = Age.ToString();
            return Name + ", " + s;
        }
    }
}