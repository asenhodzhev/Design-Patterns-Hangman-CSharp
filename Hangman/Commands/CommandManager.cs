using Hangman.Interfaces;

namespace Hangman.Commands
{
    public class CommandManager
    {
        public void Proceed(ICommand command)
        {
            command.Execute();
        }
    }
}