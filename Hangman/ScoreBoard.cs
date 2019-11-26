using Hangman.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hangman
{
    // пази данни за най-високите резултати
    public class ScoreBoard
    {
        private const string TopScoresDataPath = @"../../Resources/topScores.txt";
        private const int NumberOfTopScores = 5;
        private Dictionary<string, int> scoreBoard = new Dictionary<string, int>();

        public string Source { get; set; }
        public IConsole ConsoleWrapper { get; set; }

        public ScoreBoard(IConsole consoleWrapper)
        {
            this.Source = TopScoresDataPath;
            this.ConsoleWrapper = consoleWrapper;
        }

        public Dictionary<string, int> TopScores
        {
            get
            {
                return this.scoreBoard;
            }

            private set
            {
                this.scoreBoard = value;
            }
        }

        // зарежда резултатите от текстов файл
        public void Load()
        {
            string[] scoreTemp;
            string[] scores = File.ReadAllLines(this.Source);
            foreach (string score in scores)
            {
                scoreTemp = score.Split(',');
                this.scoreBoard.Add(scoreTemp[0], int.Parse(scoreTemp[1]));
            }
        }

        public void AddScore(Player player)
        {
            this.TopScores.Add(player.Name, player.AttemptsToGuess);
            this.ExtractSpecificTopScores();
        }

        // Сортира съществуващите резултати и оставя само 5те най-високи
        private void ExtractSpecificTopScores()
        {
            this.OrderScore();

            if (this.TopScores.Count > NumberOfTopScores)
            {
                for (int i = NumberOfTopScores; i < this.scoreBoard.Count; i++)
                {
                    this.TopScores.Remove(this.TopScores.ElementAt(i).Key);
                }
            }
        }

        private void OrderScore()
        {
            this.TopScores = this.TopScores.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        // запазва резултатите в текстов файл
        public void Save()
        {
            using (StreamWriter scoreWriter = new StreamWriter(this.Source))
            {
                foreach (var score in scoreBoard)
                {
                    scoreWriter.WriteLine("{0},{1}", score.Key, score.Value);
                }
            }
        }

        // принтира текущите най-високи резултати
        public void Print()
        {
            int position = 1;
            Console.WriteLine("{0}***** Top {1} Scores *****", new string(' ', 5), NumberOfTopScores);
            foreach (var score in this.TopScores)
            {
                Console.WriteLine("{0}.  {1} -->  {2} mistakes", position, score.Key, score.Value.ToString());
                position++;
            }
        }

        // Ъпдейтва класацията с играч, който е подобрил предишен най-висок резултат
        public void Update(Player player)
        {
            this.Load();
            if (this.TopScores.Count < NumberOfTopScores || player.AttemptsToGuess < this.TopScores.Values.Last())
            {
                while (true)
                {
                    UIMessages.EnterPlayerNameMessage();
                    player.Name = this.ConsoleWrapper.ReadLine();

                    if (player.Name == string.Empty)
                    {
                        throw new ArgumentException("The player's name cannot be an empty strig!");
                    }
                    else if (this.TopScores.ContainsKey(player.Name))
                    {
                        throw new ArgumentException("Existing name!");
                    }

                    break;
                }

                this.AddScore(player);
                this.Save();
            }
        }
    }
}