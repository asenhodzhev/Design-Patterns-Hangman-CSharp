using Hangman.Interfaces;

namespace Hangman.Commands
{
    public class RestartCommand : ICommand
    {
        public void Execute()
        {
            Hangman.Main();
        }
    }
}