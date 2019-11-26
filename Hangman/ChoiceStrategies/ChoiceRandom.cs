using System.Collections.Generic;

namespace Hangman.ChoiceStrategies
{
    public class ChoiceRandom : ChoiceStrategy
    {
        public override string Choice(List<string> allSecretWords)
        {
            RandomUtils randomGenerator = new RandomUtils();
            return randomGenerator.RandomizeWord(allSecretWords);
        }
    }
}
