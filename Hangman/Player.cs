namespace Hangman
{
    //Singleton
    public sealed class Player
    {
        public static readonly Player PlayerInstance = new Player();

        private Player()
        {
        }

        public static Player Instance
        {
            get
            {
                return PlayerInstance;
            }
        }

        public string Name { get; set; }

        public int AttemptsToGuess { get; set;  }
    }
}