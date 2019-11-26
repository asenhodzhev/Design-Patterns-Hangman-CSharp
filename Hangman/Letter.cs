using System;

namespace Hangman
{
    // PROTOTYPE
    public class Letter : ICloneable
    {
        private const ConsoleColor DEFAULTLETTERCOLOR = ConsoleColor.Gray;
        private const char DEFAULTSIGN = 'a';

        public char Sign { get; set; }
        public ConsoleColor Color { get; set; }

        public Letter()
        {
            this.Sign = DEFAULTSIGN;
            this.Color = DEFAULTLETTERCOLOR;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Print()
        {
            Console.Write("{0} ", this.Sign);
        }
    }
}
