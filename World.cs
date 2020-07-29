using System;
using System.Collections.Generic;
using System.Linq;

namespace LeprechaunHattingProblem
{
    public class World
    {
        private ChainList chains = new ChainList();
        private List<ColorCombination> combinations = new List<ColorCombination>();
        private ChainCombinationList list = new ChainCombinationList();

        #region TestEnviroment
        private List<Lepri> leprechauns;
        #endregion

        //Variables
        public int nLepri;
        public int mColor;

        //Constructor
        public World(int n = 3, int m = 2)
        {
            nLepri = n;
            mColor = m;

            Initialize();
            Statistics();


            TestAllDealsViewOne();
        }

        //Private Functions
        private void TestAllDealsViewOne()
        {
            List<Deal> dealList = CreateGoodDeals();
            TestAllDeals(dealList); 
        }
        private void TestAllDeals(List<Deal> dealList)
        {
            CreateTestEnviroment();
            foreach (Deal d in dealList)
            {
                SetStrategy(d);
                PlayAndRecordStats();
            }
        }
        private void SetStrategy(Deal d)
        {
            List<Strategy> listS = d.GetStrategies;
            for (int i = 0; i < listS.Count; i++)
            {
                leprechauns[i].SetStrategy(listS[i]);
            }
        }
        private void PlayAndRecordStats()
        {
            throw new NotImplementedException();
        }
        private void CreateTestEnviroment()
        {
            for (int i = 0; i < nLepri; i++)
            {
                Lepri l = new Lepri();
                leprechauns.Add(l);
            }
        }
        private List<Deal> CreateGoodDeals()
        {
            List<Deal> deals = new List<Deal>();
            int strats = (int)Math.Pow((mColor + 1) , Math.Pow(mColor, (nLepri - 1) ));
            for (int i = 0; i < strats; i++)
            {
                Deal d = new Deal();
                
            }
            return deals;
        }

        //Public Functions
        public void Initialize()
        {
            //LoopIncrements
            int i, j;
            //Func 1 : Create Combinations
            #region Func 1
            int remainingLepri = nLepri - 1;
            int loopCombi = (int)Math.Pow(mColor, remainingLepri);
            //Step 1 : Loop for all color possibilities on the remaining
            for (i = 0; i < loopCombi; i++)
            {
                //Step 2: Create a new Combination
                #region Example
                /* N = 3
                 * M = 2, { Red, Blue }
                 * loop = 4 
                 * Color Combinations 
                 * RR RB BR BB
                 */
                #endregion
                ColorCombination comb = new ColorCombination(remainingLepri);
                for(j = 0; j < remainingLepri; j++)
                {
                    Color c = new Color(((i / (int)Math.Pow(mColor, j)) % mColor), false);
                    comb.SetLepri(j, c);
                }
                combinations.Add(comb);
            }
            #endregion
            //Func 2 : Create Chains
            #region Func 2
            int loopChains = mColor * loopCombi;
            for(i = 0; i < loopChains; i++)
            {
                Chain newChain = new Chain(combinations, i + 1);
                ColorCombination combo = combinations[i / mColor];
                Color c = new Color(i % mColor, false);
                newChain.AddStart(combo, c);
                newChain.CompleteChain(nLepri);
                chains.Add(newChain);
            }
            //Func3 : Create Overlapping chains list
            NoOverlap();
            #endregion
        }
        public void Statistics()
        {
            System.Console.WriteLine("----------------------------");
            System.Console.WriteLine(chains.ToString());
            System.Console.WriteLine("----------------------------");
            System.Console.WriteLine("\n");
            System.Console.WriteLine("----------------------------");
            System.Console.WriteLine(list.ToString());
            System.Console.WriteLine("----------------------------");
        }
        public void NoOverlap()
        {
            list = new ChainCombinationList();
            for (int i = 0; i < chains.Count; i++)
            {
                ChainCombination chainCombi = new ChainCombination();
                Chain c = chains[i];
                chainCombi.Add(c);
                for (int j = i + 1; j < chains.Count; j++)
                {
                    Chain d = chains[j];
                    if (!c.OverlapWithChain(d))
                    {
                        chainCombi.Add(d);
                    }
                }
                list.Add(chainCombi);
            }
        }
    }
}
