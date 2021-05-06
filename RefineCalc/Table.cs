using System.Collections.Generic;

namespace RefineCalc
{
    public class Table
    {
        public int Level { get; set; }
        public int StoneNeeds { get; set; }
        public int EnhanceNeeds { get; set; }
        public int OrehaNeeds { get; set; }
        public int HonourNeeds { get; set; }
        public int GoldNeeds { get; set; }
        public double BaseProb { get; set; }
        public double SubProbBig { get; set; }
        public double SubProbMed { get; set; }
        public double SubProbSmall { get; set; }
        public int SubNumBig { get; set; }
        public int SubNumMed { get; set; }
        public int SubNumSmall { get; set; }

        public Table()
        {

        }
    }
}
