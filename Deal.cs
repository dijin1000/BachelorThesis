using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeprechaunHattingProblem
{
    class Deal
    {
        // Private Variables
        private Dictionary<int, Strategy> strategies = new Dictionary<int, Strategy>();

        //Getters && Setters
        public List<Strategy> GetStrategies
        {
            get
            {
                return strategies.Values.ToList();
            }
        }

        //Constructors
        public Deal()
        {

        }

        //Private Functions

        //Public Functions
        public void AddStrategy(int lepre, Strategy s)
        {
            if (strategies.ContainsKey(lepre))
                strategies[lepre] = s;
            else
                strategies.Add(lepre, s);
        }
    }
}
