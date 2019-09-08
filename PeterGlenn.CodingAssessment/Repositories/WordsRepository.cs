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

        private static int[] _scrabbleValues;
        public static int[] GetScrabbleValues()
        {
            if (_scrabbleValues == null)
            {
                int[] values = new int[26];

                values[0] = 1;   // A
                values[1] = 3;   // B
                values[2] = 3;   // C
                values[3] = 2;   // D
                values[4] = 1;   // E
                values[5] = 4;   // F
                values[6] = 2;   // G
                values[7] = 4;   // H
                values[8] = 1;   // I
                values[9] = 8;   // J
                values[10] = 5;  // K
                values[11] = 1;  // L
                values[12] = 3;  // M
                values[13] = 1;  // N
                values[14] = 1;  // O
                values[15] = 3;  // P
                values[16] = 10; // Q
                values[17] = 1;  // R
                values[18] = 1;  // S
                values[19] = 1;  // T
                values[20] = 1;  // U
                values[21] = 4;  // V
                values[22] = 4;  // W
                values[23] = 8;  // X
                values[24] = 4;  // Y
                values[25] = 10; // Z

                _scrabbleValues = values;
            }

            return _scrabbleValues;
        }
    }
}
