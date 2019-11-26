namespace Hangman
{
    // Proxy Pattern - Real Subject
    public class RealWord : Word
    {
        public RealWord(string word)
            : base(word)
        {
        }

        public override string PrintView
        {
            get
            {
                return this.Content;
            }
        }
    }
}
