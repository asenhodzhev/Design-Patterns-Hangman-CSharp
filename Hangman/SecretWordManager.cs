using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    // държи всички възможни тайни думи
    public class SecretWordManager
    {
        private List<string> allSecretWords = new List<string>();

        // зареждане на тайните думи от текстов файл
        public void LoadAllSecretWords(string path)
        {
            string[] words = File.ReadAllLines(path);
            foreach (string line in words)
            {
                this.allSecretWords.AddRange(line.Split(new char[] { ',', ' ', ';', '.' }, StringSplitOptions.RemoveEmptyEntries));
            }     
        }

        public List<string> GetAllSecretWords()
        {
            return this.allSecretWords;
        }

        public void Remove(int index)
        {
            this.allSecretWords.RemoveAt(index);
        }

        public void Add(string newSecretWord)
        {
            this.allSecretWords.Add(newSecretWord);
        }
    }
}
