using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangaugeInclusion
{
    public class App
    {

        public void Run()
        {
            if (File.Exists("./input.txt"))
            {
                string[] automat = File.ReadAllLines("./input.txt");
                string[] states = automat[0].Split(',');
                string[] alphabet = automat[1].Split(',');
                int ruleCount = int.Parse(automat[2]);

                Dictionary<(string, string), string> rules = new Dictionary<(string, string), string>();
                for (int i = 3; i < 3 + ruleCount; i++)
                {
                    string[] ruleParts = automat[i].Split("->");
                    string[] leftParts = ruleParts[0].Trim('(', ')').Split(",");

                    string currentState = leftParts[0].Trim();
                    string inputSymbol = leftParts[1].Trim();
                    string nextState = ruleParts[1].Trim();

                    rules[(currentState, inputSymbol)] = nextState;
                }

                string initialState = automat[3 + ruleCount];

                string[] finalState = automat[4 + ruleCount].Split(',');

                Console.Write("Enter word: ");
                string input = Console.ReadLine();

                Automat auto = new Automat(states, alphabet, rules, initialState, finalState);

                Console.WriteLine(auto.IsWordAccepted(input));
            }
        }
    }
}
