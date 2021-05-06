using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefineCalc
{
    class Expected
    {
        public double Value { get; set; }
        public double Gold { get; set; }
        public double Tries { get; set; }
        public int Count { get; set; }
        public double JangGi { get; set; }
        public double MaxCost { get; set; }
        public string Log { get; set; }
        
        public Expected(double value, double gold, double tries, int count, double jang, double max, string log)
        {
            Value = value;
            Gold = gold;
            Tries = tries;
            Count = count;
            JangGi = jang;
            MaxCost = max;
            Log = log;
        }
        public static Expected BernoulliDis(double cost, double jang, double prob, double increment, int nFails, bool isLogged)
        {
            string log = "";
            double value = 0.0;
            double accQ = 1.0;
            double tries = 0.0;
            double gold = 0.0;
            double totalGold = 0.0;
            if (prob >= 100.0)
                return new Expected(100.0, 100.0, 1, 0, 100.0, cost, log);
            prob /= 100.0;
            increment /= 100.0;
            int i = 0;
            for (; jang < 100.0; i++)
            {
                value += accQ * prob;
                gold += (i + 1) * cost * accQ * prob;
                tries += (i + 1) * accQ * prob;
                jang += prob * 100.0 * 0.465;
                totalGold += cost;
                if (isLogged)
                {
                    log += Environment.NewLine + $"{i + 1})누적 기댓값: {value * 100:F3}%" + Environment.NewLine + $"누적 소모비용: {Convert.ToInt32(totalGold)}G / 장기: {jang:F2}%";
                    log += Environment.NewLine + $"{cost:F2}G / {prob * 100:F2}% / {cost / prob / 100:F2}";
                }
                if (nFails < 10)
                {
                    prob += increment;
                    nFails++;
                }
                accQ *= 1 - prob;
            }

            return new Expected(value * 100.0, gold / totalGold * 100.0, tries, i, jang, totalGold, log);
        }
    }
}
