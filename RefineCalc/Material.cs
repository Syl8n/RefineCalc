using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefineCalc
{
    public class Material
    {
        public double CurrentProb { get; set; }
        public double BonusProb { get; set; }
        public double Gold { get; set; }
        public int NumBig { get; set; }
        public int NumMed { get; set; }
        public int NumSmall { get; set; }
        public bool Book { get; set; }
        public double PartialProb { get; set; }

        public Material(double currentProb, double bonusProb, double gold, int numBig, int numMed, int numSmall, bool book)
        {
            CurrentProb = currentProb;
            BonusProb = bonusProb;
            Gold = gold;
            NumBig = numBig;
            NumMed = numMed;
            NumSmall = numSmall;
            Book = book;
        }

        public static Material CreateMaterial(
            int numBig, int numMed, int numSmall,
            double probBig, double probMed, double probSmall, double crntProb, double probMax,
            string textBig, string textMed, string textSmall, string textBook,
            int level, double ppp)
        {
            double bonusProb = 0.0;
            double gold = 0.0;
            bool book = false;
            double pBig = 99999.0;
            double pMed = 99999.0;
            double pSmall = 99999.0;
            double pBook = 99999.0;

            if (textBig != "")
            {
                pBig = Convert.ToDouble(textBig) / probBig;
            }
            numBig *= Convert.ToInt32(pBig <= ppp);

            if (textMed != "")
            {
                pMed = Convert.ToDouble(textMed) / probMed;
            }
            numMed *= Convert.ToInt32(pMed <= ppp);

            if (textSmall != "")
            {
                pSmall = Convert.ToDouble(textSmall) / probSmall;
            }
            numSmall *= Convert.ToInt32(pSmall <= ppp);

            if (level <= 8 && textBook != "")
            {
                pBook = Convert.ToDouble(textBook) / 10.0;
            }
            book = pBook <= ppp;

            bonusProb += probBig * numBig + probMed * numMed + probSmall * numSmall + Convert.ToInt32(book) * 10.0;
            gold += Convert.ToDouble(textBig) * numBig + Convert.ToDouble(textMed) * numMed + Convert.ToDouble(textSmall) * numSmall + Convert.ToInt32(book) * Convert.ToDouble(textBook);

            while (crntProb + bonusProb > 100.0)
            {
                double maxP = new double[] { Convert.ToInt32(numBig > 0) * pBig, Convert.ToInt32(numMed > 0) * pMed,
                                            Convert.ToInt32(numSmall > 0) * pSmall, Convert.ToInt32(book) * pBook }.Max();
                if (maxP == pBig && crntProb + bonusProb - probBig >= 100.0)
                {
                    bonusProb -= probBig;
                    gold -= Convert.ToDouble(textBig);
                    numBig--;
                }
                else if (maxP == pMed && crntProb + bonusProb - probMed >= 100.0)
                {
                    bonusProb -= probMed;
                    gold -= Convert.ToDouble(textMed);
                    numMed--;
                }
                else if (maxP == pSmall && crntProb + bonusProb - probSmall >= 100.0)
                {
                    bonusProb -= probSmall;
                    gold -= Convert.ToDouble(textSmall);
                    numSmall--;
                }
                else if (maxP == pBook && crntProb + bonusProb - 10.0 >= 100.0)
                {
                    bonusProb -= 10.0;
                    gold -= Convert.ToDouble(textBook);
                    book = false;
                }
                else
                {
                    break;
                }
            }

            if (book)
            {
                while (bonusProb > probMax + 10)
                {
                    double maxP = new double[] { Convert.ToInt32(numBig > 0) * pBig, Convert.ToInt32(numMed > 0) * pMed,
                                            Convert.ToInt32(numSmall > 0) * pSmall }.Max();
                    if (maxP == pBig && bonusProb - 10 - probBig >= probMax)
                    {
                        bonusProb -= probBig;
                        gold -= Convert.ToDouble(textBig);
                        numBig--;
                    }
                    else if (maxP == pMed && bonusProb - 10 - probMed >= probMax)
                    {
                        bonusProb -= probMed;
                        gold -= Convert.ToDouble(textMed);
                        numMed--;
                    }
                    else if (maxP == pSmall && bonusProb - 10 - probSmall >= probMax)
                    {
                        bonusProb -= probSmall;
                        gold -= Convert.ToDouble(textSmall);
                        numSmall--;
                    }
                    else
                    {
                        break;
                    }
                }
                if (bonusProb > probMax + 10)
                    bonusProb = probMax + 10;
            }
            else
            {
                while (bonusProb > probMax)
                {
                    double maxP = new double[] { Convert.ToInt32(numBig > 0) * pBig, Convert.ToInt32(numMed > 0) * pMed,
                                            Convert.ToInt32(numSmall > 0) * pSmall }.Max();
                    if (maxP == pBig && bonusProb - probBig >= probMax)
                    {
                        bonusProb -= probBig;
                        gold -= Convert.ToDouble(textBig);
                        numBig--;
                    }
                    else if (maxP == pMed && bonusProb - probMed >= probMax)
                    {
                        bonusProb -= probMed;
                        gold -= Convert.ToDouble(textMed);
                        numMed--;
                    }
                    else if (maxP == pSmall && bonusProb - probSmall >= probMax)
                    {
                        bonusProb -= probSmall;
                        gold -= Convert.ToDouble(textSmall);
                        numSmall--;
                    }
                    else
                    {
                        break;
                    }
                }
                if (bonusProb > probMax)
                    bonusProb = probMax;
            }

            return new Material(crntProb, bonusProb, gold, numBig, numMed, numSmall, book);
        }

        void ReduceMaterials(double threshold)
        {

        }

        public double AccGold(List<Material> mats, int index, double baseGold)
        {
            if (index == 0)
            {
                return baseGold + this.Gold;
            }

            return baseGold + this.Gold + mats[index - 1].AccGold(mats, index - 1, baseGold);
        }

        public double Janggi(List<Material> mats, int index)
        {
            if (index == 0)
            {
                return (this.CurrentProb + this.BonusProb) * 0.465;
            }

            return (this.CurrentProb + this.BonusProb) * 0.465 + mats[index - 1].Janggi(mats, index - 1);
        }

        public double AccProb(List<Material> mats, int index)
        {
            if (index == 0)
            {
                return this.PartialProb;
            }

            return this.PartialProb + mats[index - 1].AccProb(mats, index - 1);
        }

        public double CalcPartialProb(List<Material> mats, int index)
        {
            double total = 1.0;
            foreach(Material m in mats)
            {
                if (m != mats.Last()) total *= 1 - (m.CurrentProb + m.BonusProb) / 100.0;
                else total *= (m.CurrentProb + m.BonusProb) / 100.0;
            }

            return total;
        }

        public static List<Material> CreateMaterialList()
        {
            List<Material> mats = new List<Material>();

            return mats;
        }

        public static List<Material> RearrangeList(List<Material> mats)
        {


            return mats;
        }

        
    }
}
