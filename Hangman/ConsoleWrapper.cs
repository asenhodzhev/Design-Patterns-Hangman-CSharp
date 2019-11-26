using Hangman.Interfaces;
using System;

namespace Hangman
{
    public class ConsoleWrapper : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
