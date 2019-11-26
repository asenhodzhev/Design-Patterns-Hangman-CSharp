using Hangman.Interfaces;
using System;
using System.Threading;

namespace Hangman.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            const int PauseInMiliseconds = 1000;
            UIMessages.ExitMessage();
            Thread.Sleep(PauseInMiliseconds);
            Environment.Exit(0);
        }
    }
}
