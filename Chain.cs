using System.Collections.Generic;
using System.Linq;

namespace LeprechaunHattingProblem
{
    public class Chain
    {
        //Variables
        private Dictionary<ColorCombination, Color> chainLeprechauns = new Dictionary<ColorCombination, Color>();
        private List<ColorCombination> correctAnswers = new List<ColorCombination>();
        private List<ColorCombination> incorrectAnswers = new List<ColorCombination>();
        private List<ColorCombination> combinations;
        private int chnNmbr;

        //Getters && Setters
        public int correctNumber { get { return correctAnswers.Count; } }
        public int incorrectNumber { get { return incorrectAnswers.Count; } }
        public int chainNumber { get { return chnNmbr; } }

        //Private Functions
        private bool LookAtLeprechaun(int lepreIndex, ColorCombination combi, Color c)
        {
            KeyValuePair<ColorCombination,Color>  lepreAnswer = chainLeprechauns.ElementAt(lepreIndex);
            return (lepreAnswer.Key == combi && lepreAnswer.Value == c); 
        }
        private bool LookAtLeprechaun(int lepreIndex, ColorCombination combi, string color)
        {
            KeyValuePair<ColorCombination, Color> lepreAnswer = chainLeprechauns.ElementAt(lepreIndex);
            return (lepreAnswer.Key == combi && lepreAnswer.Value.GetString() == color);
        }
        private bool LookAtLeprechaun(int lepreIndex, ColorCombination combi, int indexColor)
        {
            KeyValuePair<ColorCombination, Color> lepreAnswer = chainLeprechauns.ElementAt(lepreIndex);
            return (lepreAnswer.Key == combi && lepreAnswer.Value == new Color(indexColor,true));
        }

        //Constructor
        public Chain(List<ColorCombination> _combinations, int _chainNumber)
        {
            combinations = _combinations;
            chnNmbr = _chainNumber;
        }

        //Public Functions
        public void AddStart(ColorCombination key, Color value)
        {
            AddKey(key, value, 0);
        }
        public void AddKey(ColorCombination key, Color value, int index)
        {
            //ALWAYS IN ORDER.
            chainLeprechauns.Add(key, value);
            List<Color> listCorrect = new List<Color>();
            List<Color> listIncorrect = new List<Color>();
            List<Color> x = key.GetList;
            int k = 0;
            for(int i = 0 ; i < x.Count + 1; i++)
            {
                if (i != index)
                {
                    listCorrect.Add(x[k]);
                    listIncorrect.Add(x[k]);
                    k++;
                }
                else
                {
                    listCorrect.Add(value);
                    listIncorrect.Add(value);
                }
            }

            ColorCombination correctAnswer = new ColorCombination(listCorrect);
            ColorCombination incorrectAnswer = new ColorCombination(listIncorrect);
            correctAnswers.Add(correctAnswer);
            incorrectAnswers.Add(incorrectAnswer);
        }
        public void CompleteChain(int chainLength)
        {
            //Step 1 : Wrong Answer of the Chain 
            ColorCombination c = incorrectAnswers[0];
            //Step 2 : Fill all the leprechauns chain slots.
            for(int i = 1; i < chainLength; i++)
            {
                ColorCombination newCombi= c.GetWithout(i);
                Color newColor = c.GetAt(i).Inverse();
                AddKey(newCombi, newColor,i);
            }
        }
        public override string ToString()
        {
            string s = "";
            //j == - 1 means the header
            for (int j = -1; j < combinations.Count; j++)
            {
                for (int i = -1; i < chainLeprechauns.Count; i++)
                {
                    if(i == - 1 || j == -1)
                    {
                        if (i == -1 && j == -1)
                            //Do Nothing
                            s += "\t";
                        else if (i == -1)
                            s += combinations[j].GetString() + "\t";
                        else if (j == -1)
                            s += i;
                    }
                    else
                    {
                        var x = chainLeprechauns.ElementAt(i);
                        if (x.Key.Compare(combinations[j]))
                            s += x.Value.GetString();
                    }
                    if (i != chainLeprechauns.Count - 1)
                        s += "\t";
                    else
                        s += "\n";
                }
            }
            return s;
        }
        public bool OverlapWithChain(Chain overlapChain)
        {
            bool overlapping = false;
            for(int i = 0; i < chainLeprechauns.Count; i++)
            {
                KeyValuePair<ColorCombination,Color> chainOne = chainLeprechauns.ElementAt(i);
                KeyValuePair<ColorCombination, Color> chainTwo = overlapChain.chainLeprechauns.ElementAt(i);
                if(chainOne.Key.Compare(chainTwo.Key) && !chainOne.Value.Compare(chainTwo.Value))
                {
                    overlapping = true;
                }
            }
            return overlapping;
        }
        public List<ColorCombination> combineCorrect(List<ColorCombination> combineList)
        {
            List<ColorCombination> newList1 = combineList.Except(correctAnswers).ToList();
            List<ColorCombination> newList2 = correctAnswers.Except(combineList).ToList();
            return newList1.Concat(newList2).ToList();
        }
        public List<ColorCombination> combineIncorrect(List<ColorCombination> combineList)
        {
            List<ColorCombination> newList1 = combineList.Except(incorrectAnswers).ToList();
            List<ColorCombination> newList2 = incorrectAnswers.Except(combineList).ToList();
            return newList1.Concat(newList2).ToList();
        }
    }
    public class ChainList : List<Chain>
    {
        public override string ToString()
        {
            string returnString = "";
            int chainCount = 1;
            foreach(Chain chain in this)
            {
                returnString += "Chain : " + chainCount++ + "\n";
                returnString += "-----------------------";
                returnString += chain.ToString();
                returnString += "\n";
            }
            return returnString;
        }
    } 
    public class ChainCombination
    {
        // variables
        private ChainList chainsComp = new ChainList();
        private List<ColorCombination> corAns = new List<ColorCombination>();
        private List<ColorCombination> incorAns = new List<ColorCombination>();

        //Getters && Setters
        public List<ColorCombination> correctAnswers {
            get
            {
                return corAns;
            }
        }
        public List<ColorCombination> incorrectAnswers
        {
            get
            {
                return incorAns;
            }
        }
        public ChainList chainsCompatible
        {
            get
            {
                return chainsComp;
            }
        }

        //Public Functions
        public void Add(Chain c)
        {
            chainsCompatible.Add(c);
            corAns = c.combineCorrect(corAns);
            incorAns = c.combineIncorrect(incorAns);
        }
    }
    public class ChainCombinationList : List<ChainCombination>
    {
        public override string ToString()
        {
            string x = "";
            foreach (ChainCombination chainCombination in this)
            {
                string z = "";
                string t = "";
                foreach (Chain chain in chainCombination.chainsCompatible)
                {
                    z += "-";
                    t += "|";
                    t += chain.chainNumber.ToString();
                    for(int i =0; i < chain.chainNumber.ToString().Count(); i++)
                    {
                        z += "-";
                    }
                    z += "-";
                    t += "|";
                    z += "\t\t\t";
                    t += "\t--->\t";
                }
                z += "\n";
                t += "\n";
                x += z;
                x += t;
                x += z;
                x += "\n";
            }
            return x;
        }
    }
}
