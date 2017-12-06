using System.Collections.Generic;
using NHunspell;

namespace TagsCloudContainer
{
    internal class DefaultWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            var wordsSet = new HashSet<string>();
            //using (var hunspell = new Hunspell("ru_eng.aff", "ru_eng.dic"))            
            foreach (var word in words)
            {                    
                if (word.Length > 3)
                    wordsSet.Add(word.ToLower());
            }            
            return wordsSet;
        }
    }
}