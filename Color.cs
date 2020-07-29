using System;
using System.Collections.Generic;
using System.Linq;

namespace LeprechaunHattingProblem
{
    public class Color
    {
        #region Static Variables Color Class
        static List<string> colors = new List<string>()
        {
            "Red",
            "Blue",
            "Yellow"
        };
        static string Pass = "Pass";

        #endregion
        //Variables
        bool isThought;
        int index;

        //Constructor
        public Color(int _index = 0, bool _isThought = false)
        {
            isThought = _isThought;
            index = _index;
        }
        public Color(string color ,bool _isThought = false)
        {
            isThought = _isThought;
            if (isThought)
            {
                if (colors.Any(p => p == color))
                    index = colors.FindIndex(p => p == color) + 1;
                else if (color == Pass)
                    index = 0;
                else
                    Console.WriteLine("Error");
            }
            else
            {
                if (colors.Any(p => p == color))
                    index = colors.FindIndex(p => p == color);
                else
                    Console.WriteLine("Error");
            }
        }

        //Private Functions

        //Public Functions
        public string GetString()
        {
            if (isThought)
            {
                if (index == 0)
                    return Pass;
                else
                    return colors[index - 1];
            }
            else
            {
                return colors[index];
            }
        }
        public Color Inverse()
        {
            //FIX IT
            string color = colors.Where(p => p != GetString()).FirstOrDefault();
            return new Color(color,isThought);
        }
        public void ClrThought2Non()
        {
            if (isThought)
            {
                if (GetString() == Pass)
                {
                    Console.WriteLine("Not Possible Transformation.");
                    return;
                }
                else
                {
                    index--;
                    isThought = false;
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invallid Transformation.");
                return;
            }
        }
        public void ClrNon2Thought()
        {
            if (isThought == false)
            {
                index++;
                isThought = true;
                return;
            }
            else
            {
                Console.WriteLine("Invallid Transformation.");
                return;
            }
        }

        public bool Compare(Color d)
        {
            return GetString() == d.GetString();
        }
    }

    public class ColorCombination
    {
        //Variables
        List<Color> combination = new List<Color>();
        //Getters && Setters
        public List<Color> GetList { get { return combination; } }
        
        //Constructors
        public ColorCombination(int max)
        {
            for (int i = 0; i < max; i++)
            {
                combination.Insert(0,new Color());
            }
        }
        public ColorCombination(List<Color> colorList)
        {
            combination = colorList;
        }

        //Private Functions
        
        //Public Functions
        public void SetLepri(int j, Color c)
        {
            combination[j] = c;
        }
        public bool Compare(ColorCombination colorCombination)
        {
            for(int i = 0; i < combination.Count; i++)
            {
                Color c = combination[i];
                Color d = colorCombination.GetList[i];
                if(c.Compare(d))
                {
                    return false;
                }
            }
            return true;
        }
        public Color GetAt(int i)
        {
            return combination[i];
        }
        public ColorCombination GetWithout(int i)
        {
            List<Color> indexList = new List<Color>();
            indexList.Add(GetAt(i));
            ColorCombination returnCom = new ColorCombination(combination.Except(indexList).ToList());
            return returnCom;
        }
        public string GetString()
        {
            string s = "";
            foreach(Color c in combination)
            {
                s += c.GetString();
            }
            return s;
        }
    }
}
