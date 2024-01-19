using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangaugeInclusion
{
    public class GramatikaApp
    {
        public void Run()
        {
            try
            {
                string[] lines = File.ReadAllLines("gramatika.txt");

                string[] N = lines[0].Split(':').Last().Trim().Split(',');

                string[] alphabet = lines[1].Split(':').Last().Trim().Split(',');

                string startingPoint = lines[2].Split(':').Last().Trim();

                Dictionary<string, List<string>> rules = new Dictionary<string, List<string>>();
                foreach (var line in lines.Skip(4))
                {
                    string[] parts = line.Split("->");
                    string nonTerminal = parts[0].Trim();
                    string productions = parts[1].Trim();
                    rules[nonTerminal] = productions.Split('|').Select(p => p.Trim()).ToList();
                }

                List<string> languageWords = GenerateLanguageWords(startingPoint, rules, alphabet, limit: 100);

                languageWords.Sort((x, y) => x.Length.CompareTo(y.Length));

                Console.WriteLine("Generovaná slova jazyka:");
                foreach (var word in languageWords)
                {
                    Console.WriteLine(word);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
            }
        }

        static List<string> GenerateLanguageWords(string axiom, Dictionary<string, List<string>> rules, string[] alphabet, int limit)
        {
            List<string> result = new List<string>();

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(axiom);

            while (queue.Count > 0 && result.Count < limit)
            {
                string currentWord = queue.Dequeue();
                bool containsNonTerminal = false;

                foreach (char symbol in currentWord)
                {
                    if (Array.Exists(alphabet, a => a == symbol.ToString()))
                    {
                        continue;
                    }

                    if (rules.TryGetValue(symbol.ToString(), out List<string> productions))
                    {
                        foreach (var production in productions)
                        {
                            int index = currentWord.IndexOf(symbol);
                            if (index >= 0)
                            {
                                string newWord = currentWord.Remove(index, 1).Insert(index, production);
                                queue.Enqueue(newWord);
                                containsNonTerminal = true;
                            }
                        }
                    }
                }

                if (!containsNonTerminal)
                {
                    result.Add(currentWord);
                }
            }

            return result;
        }
    }
}