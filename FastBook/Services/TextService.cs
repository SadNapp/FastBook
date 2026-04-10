using System;
using System.Linq;

namespace FastBook.Services
{
    public class TextService
    {
        
        public string GetLineNumbers(string text)
        {
            if (string.IsNullOrEmpty(text)) return "1";

            
            int lineCount = text.Split('\n').Length;

            
            return string.Join(Environment.NewLine, Enumerable.Range(1, lineCount));
        }

       
        public int GetLineCount(string text)
        {
            if (string.IsNullOrEmpty(text)) return 1;
            return text.Split('\n').Length;
        }
    }
}