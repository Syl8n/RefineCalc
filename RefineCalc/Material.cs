using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefineCalc
{
    public class Material
    {
        private static double tableProb;
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
        private double bonus;
        public double CurrentProb { get; set; }
        public double BonusProb { get { return Math.Min(bonus, tableProb); } set { bonus = value; } }
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

        public Material(double currentProb, double bonusProb, int numBig, int numMed, int numSmall, bool book, double partialProb)
        {
            CurrentProb = currentProb;
            BonusProb = bonusProb;
            NumBig = numBig;
            NumMed = numMed;
            NumSmall = numSmall;
            Book = book;
            PartialProb = partialProb;
        }

        public static void SetThresholds(double tableProb, double basePrice, double baseProb, double bigPrice, double bigProb, 
                                                double medPrice, double medProb, double smallPrice, double smallProb, double bookPrice)
        {
            Material.tableProb = tableProb;
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

        public Material SetMaterial(double baseProb, int level, int numBig, int numMed, int numSmall)
        {
            CurrentProb = baseProb;
            NumBig = Convert.ToInt32(CurrentProb < ThresholdBig) * numBig;
            NumMed = Convert.ToInt32(CurrentProb < ThresholdMed) * numMed;
            NumSmall = Convert.ToInt32(CurrentProb < ThresholdSmall) * numSmall;
            Book = CurrentProb < ThresholdBook && level <= 8;

            bonus = probBig * NumBig + probMed * NumMed + probSmall * NumSmall + Convert.ToInt32(Book) * 10.0;

            while (CurrentProb + bonus > 100.0)
            {
                double maxP = new double[] { Convert.ToInt32(NumBig > 0) * 1 / ThresholdBig, Convert.ToInt32(NumMed > 0) * 1 / ThresholdMed,
                                            Convert.ToInt32(NumSmall > 0) * 1 / ThresholdSmall, Convert.ToInt32(Book) * 1 / ThresholdBook }.Max();
                if (maxP == 1 / ThresholdBig && CurrentProb + bonus - probBig >= 100.0)
                {
                    bonus -= probBig;
                    NumBig--;
                }
                else if (maxP == 1 / ThresholdMed && CurrentProb + bonus - probMed >= 100.0)
                {
                    bonus -= probMed;
                    NumMed--;
                }
                else if (maxP == 1 / ThresholdSmall && CurrentProb + bonus - probSmall >= 100.0)
                {
                    bonus -= probSmall;
                    NumSmall--;
                }
                else if (maxP == 1 / ThresholdBook && CurrentProb + bonus - 10.0 >= 100.0)
                {
                    bonus -= 10.0;
                    Book = false;
                }
                else
                {
                    break;
                }
            }

            double tmpProb = bonus - 10 * Convert.ToInt32(Book);
            while (tmpProb > tableProb)
            {
                var tmpArr = new List<(double threshold, double prob)>
                { (Convert.ToInt32(NumBig > 0) * 1 / ThresholdBig, probBig),
                    (Convert.ToInt32(NumMed > 0) * 1 / ThresholdMed, probMed),
                    (Convert.ToInt32(NumSmall > 0) * 1 / ThresholdSmall, probSmall),
                    (Convert.ToInt32(Book) * 1 / ThresholdBook, 10.0)
                };
                tmpArr.Sort((x, y) => y.threshold.CompareTo(x.threshold));

                double extraP = 100.0;
                for (int i = 0; i < tmpArr.Count; i++)
                {
                    if (tmpArr[i].threshold > 0 && tmpArr[i].prob < tmpProb - tableProb)
                    {
                        extraP = tmpArr[i].prob;
                        break;
                    }
                }

                if (tmpProb - extraP >= tableProb)
                {
                    if (extraP == probBig)
                    {
                        tmpProb -= probBig;
                        NumBig--;
                    }
                    else if (extraP == probMed)
                    {
                        tmpProb -= probMed;
                        NumMed--;
                    }
                    else if (extraP == probSmall)
                    {
                        tmpProb -= probSmall;
                        NumSmall--;
                    }
                    else
                    {
                        throw new Exception("Invalid extra probability");
                    }
                    tmpProb = Math.Round(tmpProb, 6);
                }
                else
                {
                    break;
                }
            }
            BonusProb = tmpProb + 10 * Convert.ToInt32(Book);

            return this;
        }

        public Material Clone()
        {
            return new Material(this.CurrentProb, this.BonusProb, this.NumBig, this.NumMed, this.NumSmall, this.Book, this.PartialProb);
        }

        public void Duplicate(Material mat)
        {
            this.CurrentProb = mat.CurrentProb;
            this.BonusProb = mat.BonusProb;
            this.NumBig = mat.NumBig;
            this.NumMed = mat.NumMed;
            this.NumSmall = mat.NumSmall;
            this.Book = mat.Book;
            this.PartialProb = mat.PartialProb;
        }

        public static void RebuildMaterials(List<Material> mats)
        {
            foreach(Material m in mats)
            {
                m.CalcPartialProb(mats);
            }
        }

        public static void ReduceMaterials(List<Material> mats, double basePrice, double extraProb)
        {
            Material mat = mats.Last();
            Material min = mat.Clone();
            double minM = basePrice * (min.CurrentProb + min.BonusProb) / 100.0 + (2 * basePrice + min.Gold) * (100.0 - min.CurrentProb - min.BonusProb) / 100.0;
            while (extraProb >= 0)
            {
                double maxP = new double[] { Convert.ToInt32(probBig <= extraProb) * Convert.ToInt32(mat.NumBig > 0) * 1 / ThresholdBig,
                    Convert.ToInt32(probMed <= extraProb) * Convert.ToInt32(mat.NumMed > 0) * 1 / ThresholdMed,
                    Convert.ToInt32(probSmall <= extraProb) * Convert.ToInt32(mat.NumSmall > 0) * 1 / ThresholdSmall,
                    Convert.ToInt32(10.0 <= extraProb) * Convert.ToInt32(mat.Book) * 1 / ThresholdBook }.Max();
                if (maxP == 1 / ThresholdBig)
                {
                    extraProb -= probBig;
                    mat.bonus -= probBig;
                    mat.NumBig--;
                }
                else if (maxP == 1 / ThresholdMed)
                {
                    extraProb -= probMed;
                    mat.bonus -= probMed;
                    mat.NumMed--;
                }
                else if (maxP == 1 / ThresholdSmall)
                {
                    extraProb -= probSmall;
                    mat.bonus -= probSmall;
                    mat.NumSmall--;
                }
                else if (maxP == 1 / ThresholdBook)
                {
                    extraProb -= 10.0;
                    mat.bonus -= 10.0;
                    mat.Book = false;
                }
                else
                {
                    break;
                }
                extraProb = Math.Round(extraProb, 6);
                
                double matM = basePrice * (mat.CurrentProb + mat.BonusProb) / 100.0 + (2 * basePrice + mat.Gold) * (100.0 - mat.CurrentProb - mat.BonusProb) / 100.0;
                if(minM > matM)
                {
                    minM = matM;
                    min.Duplicate(mat);
                }
            }
            mats[mats.Count - 1] = min;
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
            int index = mats.IndexOf(this);
            double total = 1.0;
            for(int i = 0; i <= index; i++)
            {
                if (i != index) total *= 1 - (mats[i].CurrentProb + mats[i].BonusProb) / 100.0;
                else total *= (mats[i].CurrentProb + mats[i].BonusProb) / 100.0;
            }

            this.PartialProb = total;
        }
    }
}
