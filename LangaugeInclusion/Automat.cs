using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangaugeInclusion
{
    public class Automat
    {
        private string[] states;
        private string[] alphabet;
        private Dictionary<(string, string), string> rules;
        private string initialState;
        private string[] finalStates;

        public Automat(string[] states, string[] alphabet, Dictionary<(string, string), string> rules, string initialState, string[] finalStates)
        {
            this.states = states;
            this.alphabet = alphabet;
            this.rules = rules;
            this.initialState = initialState;
            this.finalStates = finalStates;
        }

        public bool IsWordAccepted(string word)
        {
            string currentState = initialState;
            foreach (char symbol in word)
            {
                string inputSymbol = symbol.ToString();
                if(rules.TryGetValue((currentState, inputSymbol), out string nextState))
                {
                    currentState = nextState;
                }
                else
                {
                    return false;
                }
            }

            return Array.Exists(finalStates, state => state == currentState);
        }
    }
}
