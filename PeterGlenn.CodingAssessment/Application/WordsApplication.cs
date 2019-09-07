using PeterGlenn.CodingAssessment.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterGlenn.CodingAssessment.Application
{
    public interface IWordsApplication
    {
        List<string> GetMatchingWords(string word);
    }
    public class WordsApplication : IWordsApplication
    {
        private IWordsRepository _wordsRepository;
        public WordsApplication(IWordsRepository repository)
        {
            _wordsRepository = (repository != null) ? repository : throw new ArgumentNullException("Unable to instantiate WordsRepository becuase the provided WordsRepository is null.");

        }

        public List<string> GetMatchingWords(string word)
        {
            //don't wast time if the input word is null
            if (string.IsNullOrEmpty(word))
                return new List<string>();

            var wordsList = GetAllWords();
            var matchingWords = CheckForMatchingWordsInWordsList(wordsList, word);
            return matchingWords ?? new List<string>();
        }

        private List<string> GetAllWords()
        {
            var allTheWords = _wordsRepository.GetWordsList();

            if (allTheWords == null)
                throw new ApplicationException("Unable to retrive a valid Words List from the Repository.");
            if (allTheWords.Count() < 1)
                throw new ApplicationException("Unable to retrive a valid Words List from the Repository.");

            return allTheWords.ToList();
        }

        private List<String> CheckForMatchingWordsInWordsList(List<string> wordsList, string inputWord)
        {
            //count the occurence of each letter availble it the input word
            var availableLettersCount = CountAvailableLetterOccurences(inputWord);

            //for each word in the Word List count the occurence of each letter 
            List<String> result = new List<string>();
            foreach (string word in wordsList)
            {
                int[] letterCount = new int[26];
                bool ok = true;
                foreach (char c in word.ToUpper())
                {
                    if (Char.IsLetter(c))
                    {
                        int index = c - 'A';
                        letterCount[index]++;
                        //if a word contains more occurence than the input word than is is not a valid word
                        if (letterCount[index] > availableLettersCount[index])
                        {
                            ok = false;
                            break;
                        }
                    }
                }
                //add valid words to the results list
                if (ok)
                {
                    result.Add(word);
                }
            }
            return result;
        }

        private int[] CountAvailableLetterOccurences(string word)
        {
            //return an empty array if the word is null
            int[] avail = new int[26];
            if (string.IsNullOrEmpty(word))
                return avail;

            //count the occurences of each letter in the word
            var upperWord = word.ToUpper();
            foreach (char c in upperWord)
            {
                if (Char.IsLetter(c))
                {
                    int index = c - 'A';
                    avail[index]++;
                }
            }
            return avail;
        }
    }
}
