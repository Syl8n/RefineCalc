using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefineCalc
{
    public class Material
    {
        private static double probBig;
        private static double probMed;
        private static double probSmall;
        private static double priceBig;
        private static double priceMed;
        private static double priceSmall;
        private static double priceBook;
        public static double ThresholdBig;
        public static double ThresholdMed;
        public static double ThresholdSmall;
        public static double ThresholdBook;
        public double CurrentProb { get; set; }
        public double BonusProb { get; set; }
        public double Gold
        {
            get
            {
                return Material.priceBig * NumBig + Material.priceMed * NumMed + Material.priceSmall * NumSmall + Convert.ToInt32(Book) * priceBook;
            } 
        }
        public int NumBig { get; set; }
        public int NumMed { get; set; }
        public int NumSmall { get; set; }
        public bool Book { get; set; }
        public double PartialProb { get; set; }
        

        public Material()
        {

        }
        public Material(double currentProb, double bonusProb, int numBig, int numMed, int numSmall, bool book)
        {
            CurrentProb = currentProb;
            BonusProb = bonusProb;
            NumBig = numBig;
            NumMed = numMed;
            NumSmall = numSmall;
            Book = book;
        }

        public static void SetThresholds(double basePrice, double baseProb, double bigPrice, double bigProb, 
                                                double medPrice, double medProb, double smallPrice, double smallProb, double bookPrice)
        {
            probBig = bigProb;
            probMed = medProb;
            probSmall = smallProb;
            priceBig = bigPrice;
            priceMed = medPrice;
            priceSmall = smallPrice;
            priceBook = bookPrice;
            ThresholdBig = baseProb * ((basePrice / baseProb) / (bigPrice / bigProb));
            ThresholdMed = baseProb* ((basePrice / baseProb) / (medPrice / medProb));
            ThresholdSmall = baseProb * ((basePrice / baseProb) / (smallPrice / smallProb));
            ThresholdBook = baseProb * ((basePrice / baseProb) / (bookPrice / 10.0));
        }

        public Material SetMaterial(double baseProb, double tableProb, int level, int numBig, int numMed, int numSmall)
        {
            CurrentProb = baseProb;
            NumBig = Convert.ToInt32(CurrentProb < ThresholdBig) * numBig;
            NumMed = Convert.ToInt32(CurrentProb < ThresholdMed) * numMed;
            NumSmall = Convert.ToInt32(CurrentProb < ThresholdSmall) * numSmall;
            Book = CurrentProb < ThresholdBook && level <= 8;

            BonusProb = probBig * NumBig + probMed * NumMed + probSmall * NumSmall + Convert.ToInt32(Book) * 10.0;

            while (CurrentProb + BonusProb > 100.0)
            {
                double maxP = new double[] { Convert.ToInt32(NumBig > 0) * 1 / ThresholdBig, Convert.ToInt32(NumMed > 0) * 1 / ThresholdMed,
                                            Convert.ToInt32(NumSmall > 0) * 1 / ThresholdSmall, Convert.ToInt32(Book) * 1 / ThresholdBook }.Max();
                if (maxP == 1 / ThresholdBig && CurrentProb + BonusProb - probBig >= 100.0)
                {
                    BonusProb -= probBig;
                    NumBig--;
                }
                else if (maxP == 1 / ThresholdMed && CurrentProb + BonusProb - probMed >= 100.0)
                {
                    BonusProb -= probMed;
                    NumMed--;
                }
                else if (maxP == 1 / ThresholdSmall && CurrentProb + BonusProb - probSmall >= 100.0)
                {
                    BonusProb -= probSmall;
                    NumSmall--;
                }
                else if (maxP == 1 / ThresholdBook && CurrentProb + BonusProb - 10.0 >= 100.0)
                {
                    BonusProb -= 10.0;
                    Book = false;
                }
                else
                {
                    break;
                }
            }

            double tmpProb = BonusProb - 10 * Convert.ToInt32(Book);
            while (tmpProb > tableProb)
            {
                double maxP = new double[] { Convert.ToInt32(NumBig > 0) * 1 / ThresholdBig, Convert.ToInt32(NumMed > 0) * 1 / ThresholdMed,
                                            Convert.ToInt32(NumSmall > 0) * 1 / ThresholdSmall, Convert.ToInt32(Book) * 1 / ThresholdBook }.Max();
                if (maxP == 1 / ThresholdBig && tmpProb - probBig >= tableProb)
                {
                    tmpProb -= probBig;
                    NumBig--;
                }
                else if (maxP == 1 / ThresholdMed && tmpProb - probMed >= tableProb)
                {
                    tmpProb -= probMed;
                    NumMed--;
                }
                else if (maxP == 1 / ThresholdSmall && tmpProb - probSmall >= tableProb)
                {
                    tmpProb -= probSmall;
                    NumSmall--;
                }
                else
                {
                    break;
                }
            }
            if (tmpProb > tableProb) tmpProb = tableProb;
            BonusProb = tmpProb + 10 * Convert.ToInt32(Book);

            return this;
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

            return new Material(crntProb, bonusProb, numBig, numMed, numSmall, book);
        }
        
        public static void ReduceMaterials(List<Material> mats)
        {
            // with while loop
            // while(condition = when profit/loss are switched)
            //      if(calc profit/loss == profit)
            //          remove 1 lowest efficient meterial
        }

        public static void RearrangeMaterials(List<Material> mats)
        {
            // if the sum of submaterial prices is less than the price of last trial
            // it is worth to reduce one trial and put some extra unefficient materials
            // what needs:
            // probability to fill
            // each material's efficiency at each trial level
            // the price of last trial
            // 1st Method: fill in materials from highest efficiency
            // 2st Method: Calculate the number of each material to fill the lack of probability precisely
        }

        public double AccGold(List<Material> mats, double baseGold)
        {
            int index = mats.IndexOf(this);
            if (index == 0)
            {
                return baseGold + this.Gold;
            }

            return baseGold + this.Gold + mats[index - 1].AccGold(mats, baseGold);
        }

        public double ExpGold(List<Material> mats, double baseGold)
        {
            int index = mats.IndexOf(this);
            if (index == 0)
            {
                return (baseGold + this.Gold) * this.PartialProb;
            }

            return (index + 1) * (baseGold + this.Gold) * this.PartialProb + mats[index - 1].ExpGold(mats, baseGold);
        }

        public double Tries(List<Material> mats)
        {
            int index = mats.IndexOf(this);
            if (index == 0)
            {
                return this.PartialProb;
            }

            return (index + 1) * this.PartialProb + mats[index - 1].Tries(mats);
        }

        public double Janggi(List<Material> mats)
        {
            int index = mats.IndexOf(this);
            if (index == 0)
            {
                return (this.CurrentProb + this.BonusProb) * 0.465;
            }

            return (this.CurrentProb + this.BonusProb) * 0.465 + mats[index - 1].Janggi(mats);
        }

        public double AccProb(List<Material> mats)
        {
            int index = mats.IndexOf(this);
            if (index == 0)
            {
                return this.PartialProb;
            }

            return this.PartialProb + mats[index - 1].AccProb(mats);
        }

        public void CalcPartialProb(List<Material> mats)
        {
            double total = 1.0;
            foreach(Material m in mats)
            {
                if (m != mats.Last()) total *= 1 - (m.CurrentProb + m.BonusProb) / 100.0;
                else total *= (m.CurrentProb + m.BonusProb) / 100.0;
            }

            this.PartialProb = total;
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
