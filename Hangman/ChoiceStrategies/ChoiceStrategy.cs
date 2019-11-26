using System.Collections.Generic;

namespace Hangman.ChoiceStrategies
{
    public abstract class ChoiceStrategy
    {
        public abstract string Choice(List<string> allSecretWords);
    }
}
