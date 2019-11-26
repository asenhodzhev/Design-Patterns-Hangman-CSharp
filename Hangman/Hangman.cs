namespace Hangman
{
    public static class Hangman
    {
        public static void Main()
        {
            Player newPlayer = Player.Instance;
            var console = new ConsoleWrapper();
            GameEngine newGame = new GameEngine(newPlayer, console);
            newGame.InitializeData();
        }
    }
}
