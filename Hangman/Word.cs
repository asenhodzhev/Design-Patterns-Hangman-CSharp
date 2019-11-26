using Hangman.Interfaces;
using System;
using System.Linq;

namespace Hangman
{
    // Proxy Pattern - Subject
    public class Word : IWord
    {
        private string content;
        public virtual string PrintView { get; set; }
        public bool[] RevealedCharacters { get; set; }
        public int WordLength { get; set; }

        public Word(string word)
        {
            this.Content = word;
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("The word cannot be null!");
                }
                if (!this.IsLettersOnly(value))
                {
                    throw new ArgumentException("The word contains nonalphabetic symbols!");
                }

                this.content = value;
            }
        }

        public int NumberOfRevealedLetters
        {
            get
            {
                return RevealedCharacters.Where(x => x).Count();
            }
            set
            {
            }
        }

        public virtual string Print()
        {
            return this.Content;
        }

        public bool IsLettersOnly(string str)
        {
            foreach (char currentChar in str)
            {
                if (!char.IsLetter(currentChar))
                {
                    return false;
                }
            }
            return true;
        }
    }
}