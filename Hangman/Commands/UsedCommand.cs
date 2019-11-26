using Hangman.Interfaces;
using System;
using System.Collections.Generic;

namespace Hangman.Commands
{
    public class UsedCommand : ICommand
    {
        private const int ALLLETTERSIZE = 26;
        private const ConsoleColor REDCOLOR = ConsoleColor.Red;
        private const ConsoleColor DEFAULTCOLOR = ConsoleColor.Gray;

        public HashSet<char> UsedLetters { get; set; }
        private List<Letter> AllLetters { get; set; }

        public UsedCommand(HashSet<char> usedLetters)
        {
            this.UsedLetters = usedLetters;
            this.AllLetters = this.AddAllLetters();
        }

        public void Execute()
        {
            this.SetColorToTheUsedLetters();
            this.PrintAllLetters();
        }

        // PROTOTYPE / зарежда азбуката
        private List<Letter> AddAllLetters()
        {
            var allLetters = new List<Letter>();
            Letter exampleLetter = new Letter();

            for (int i = 0; i < ALLLETTERSIZE; i++)
            {
                Letter currentLetter = (Letter)exampleLetter.Clone();
                currentLetter.Sign = Convert.ToChar(currentLetter.Sign + i);
                allLetters.Add(currentLetter);
            }

            return allLetters;
        }

        // Промяна на цвета на всички използвани букви на червен
        private void SetColorToTheUsedLetters()
        {
            for (int i = 0; i < ALLLETTERSIZE; i++)
            {
                var currentLetter = this.AllLetters[i];
                if (this.UsedLetters.Contains(currentLetter.Sign) && currentLetter.Color != REDCOLOR)
                {
                    this.AllLetters[i].Color = REDCOLOR;
                }
            }
        }

        // Принтиране на използваните букви в червено, а на останалите по дефолт
        private void PrintAllLetters()
        {
            for (int i = 0; i < this.AllLetters.Count; i++)
            {
                Console.ForegroundColor = this.AllLetters[i].Color;
                this.AllLetters[i].Print();
            }

            Console.ForegroundColor = DEFAULTCOLOR;
            Console.WriteLine();
        }
    }
}
