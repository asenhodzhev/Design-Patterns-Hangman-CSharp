﻿using Hangman.ChoiceStrategies;
using Hangman.Commands;
using Hangman.Interfaces;
using System;
using System.Collections.Generic;

namespace Hangman
{
    // FACADE
    public class GameEngine
    {
        private const int InitialPlayerScore = 0;
        private const int MaxPlayerAttempts = 10;
        private const string PathToSecretWordsDatabase = @"../../Resources/secretWords.txt";

        public ChoiceStrategy ChoiceStrategy { get; set; }
        public IConsole ConsoleWrapper { get; set; }
        public string PathToSecretWordsDirectory { get; set; }
        private Player Player { get; set; }
        private CheckManager CheckManager { get; set; }
        private ScoreBoard ScoreBoard { get; set; }

        public GameEngine(Player player, IConsole consoleWrapper)
        {
            this.Player = player;
            this.ConsoleWrapper = consoleWrapper;
            this.ChoiceStrategy = new ChoiceRandom();
            this.PathToSecretWordsDirectory = PathToSecretWordsDatabase;
        }

        public void InitializeData()
        {
            Console.Clear();
            this.CheckManager = new CheckManager(this.Player);
            this.ScoreBoard = new ScoreBoard(this.ConsoleWrapper);
            this.Player.AttemptsToGuess = InitialPlayerScore;
            this.CheckManager.HasHelpUsed = false;
            var secretWord = this.LoadSecretWord();
            this.GamePlayStart(secretWord);
        }

        // зареждане на тайната дума чрез стратегия
        private IWord LoadSecretWord()
        {
            SecretWordManager wordsManager = new SecretWordManager();
            wordsManager.LoadAllSecretWords(this.PathToSecretWordsDirectory);

            List<string> allWords = wordsManager.GetAllSecretWords();
            IWord secretWord = new ProxyWord(this.ChoiceWord(this.ChoiceStrategy, allWords));
            this.CheckManager.DefineCommands(secretWord);
            UIMessages.WelcomeMessage(MaxPlayerAttempts);

            return secretWord;
        }

        private string ChoiceWord(ChoiceStrategy choiceStrategy, List<string> words)
        {
            string chosenSecretWord = choiceStrategy.Choice(words);
            return chosenSecretWord;
        }

        // Проверка дали играчит има още опити за познаване
        private void GamePlayStart(IWord word)
        {
            while (word.NumberOfRevealedLetters < word.Content.Length && this.Player.AttemptsToGuess < 10)
            {
                UIMessages.SecretWordMessage(word.PrintView, false);
                this.InputData(word);
            }

            this.GameOver(word);
        }

        // Обработва опитите на играча и командите, които той въвежда
        private void InputData(IWord word)
        {
            while (true)
            {
                UIMessages.InviteForGuessOrCommandMessage();
                string playerChoice = this.ConsoleWrapper.ReadLine().ToLower();

                if (playerChoice == string.Empty)
                {
                    continue;
                }

                char playerLetter = playerChoice.ToLower()[0];
                if (playerChoice.Length > 1)
                {
                    if (this.IsTheCommandCorrect(playerChoice))
                    {
                        this.CheckManager.CheckCommand(playerChoice, word);
                    }
                    else
                    {
                        UIMessages.IncorrectInputMessage();
                    }

                    if (word.NumberOfRevealedLetters == word.WordLength)
                    {
                        break;
                    }
                }
                else
                {
                    if (char.IsLetter(playerLetter))
                    {
                        this.CheckManager.CheckLetterAccordance(word, playerLetter);
                    }
                    else
                    {
                        UIMessages.IncorrectInputMessage();
                    }

                    break;
                }
            }
        }

        private void GameOver(IWord word)
        {
            if (this.Player.AttemptsToGuess == MaxPlayerAttempts)
            {
                UIMessages.LostGameMessage();
            }
            else
            {
                UIMessages.GuessAllWordMessage(this.Player.AttemptsToGuess);
                UIMessages.SecretWordMessage(word.Content, true);
                this.ScoreBoard.Update(this.Player);
                this.ScoreBoard.Print();
            }
        }

        private bool IsTheCommandCorrect(string command)
        {
            var commandToLower = command.ToLower();
            if (Command.Exit.ToString().ToLower() == commandToLower ||
                Command.Help.ToString().ToLower() == commandToLower ||
                Command.Restart.ToString().ToLower() == commandToLower ||
                Command.Top.ToString().ToLower() == commandToLower ||
                Command.Used.ToString().ToLower() == commandToLower)
            {
                return true;
            }

            return false;
        }
    }
}