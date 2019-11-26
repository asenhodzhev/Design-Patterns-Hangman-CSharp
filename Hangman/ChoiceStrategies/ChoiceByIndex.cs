using System.Collections.Generic;

namespace Hangman.ChoiceStrategies
{
    public class ChoiceByIndex : ChoiceStrategy
    {
        private int Index { get; set; }

        public override string Choice(List<string> allSecretWords)
        {
            return allSecretWords[this.Index];
        }

        public ChoiceByIndex(int number)
        {
            this.Index = number;
        }
    }
}
