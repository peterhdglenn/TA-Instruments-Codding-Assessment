using System.Collections.Generic;
using System.Linq;

namespace PeterGlenn.CodingAssessment.Repositories
{
    public interface IWordsRepository
    {
        IList<string> GetWordsList();
    }

    public class WordsRepository : IWordsRepository
    {
        private static List<string> _wordsList;
        public IList<string> GetWordsList()
        {
            if (_wordsList == null)
            {
                var filePath = @"./Resources/sowpods.txt";
                List<string> words = System.IO.File.ReadLines(filePath).ToList();
                _wordsList = words;
            }

            return _wordsList;
        }
    }
}
