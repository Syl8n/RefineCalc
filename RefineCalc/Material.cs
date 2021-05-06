using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefineCalc
{
    public class Material
    {
        public double BonusProb { get; set; }
        public double Gold { get; set; }
        public int NumBig { get; set; }
        public int NumMed { get; set; }
        public int NumSmall { get; set; }
        public bool Book { get; set; }

        public Material(double bonusProb, double gold, int numBig, int numMed, int numSmall, bool book)
        {
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
                while (bonusProb - 10 > probMax)
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
            }

            return new Material(bonusProb, gold, numBig, numMed, numSmall, book);
        }
    }
}
