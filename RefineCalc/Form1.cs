using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RefineCalc
{
    public partial class Form1 : Form
    {
        public const string FILENAME = "RefineData.xlsx";
        private double baseProb;
        private List<Table> tables = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ReadExcel();

                if (File.Exists(Latest.FILE_NAME))
                {
                    SetPrice(Latest.ReadFile());
                }
                else
                {
                    InitPrice();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitPrice()
        {
            cBoxHonour.SelectedIndex = 0;
            txtWpStone.Text = "0";
            txtArmStone.Text = "0";
            txtEnhance.Text = "0";
            txtOreha.Text = "0";
            txtHonour.Text = "0";
            cBoxGrade.SelectedIndex = 0;
            cBoxPart.SelectedIndex = 0;
            cBoxLevel.SelectedIndex = 0;
            txtDisJangGi.Text = "0.0";
        }

        private void SetPrice(Latest latest)
        {
            try
            {
                txtWpStone.Text = latest.WpStone.ToString();
                txtArmStone.Text = latest.ArmStone.ToString();
                txtEnhance.Text = latest.Enhance.ToString();
                txtOreha.Text = latest.Oreha.ToString();
                cBoxHonour.SelectedIndex = latest.HonourLevel;
                txtHonour.Text = latest.Honour.ToString();
                txtSubBig.Text = latest.SubBig.ToString();
                txtSubMed.Text = latest.SubMed.ToString();
                txtSubSmall.Text = latest.SubSmall.ToString();
                txtWpBook.Text = latest.WpBook.ToString();
                txtArmBook.Text = latest.ArmBook.ToString();
                cBoxGrade.SelectedIndex = latest.EquipGrade;
                cBoxPart.SelectedIndex = latest.EquipPart;
                cBoxLevel.SelectedIndex = latest.EquipLevel;
                txtDisProb.Text = latest.Prob.ToString("F3");
                txtDisJangGi.Text = latest.JangGi.ToString("F2");
                cBoxResearch1.Checked = latest.Research1;
                cBoxResearch2.Checked = latest.Research2;
                cBoxDetails.Checked = latest.Details;
                cBoxOpt.Checked = latest.Opti;
            }
            catch
            {
                InitPrice();
            }
        }

        private void ReadExcel()
        {
            TableManager.LoadFromFile($@"{Application.StartupPath}\{FILENAME}");
        }

        private void CalcJangGi(double tp, bool fBig, bool fMed, bool fSmall, bool fBook)
        {
            Table table = tables[cBoxLevel.SelectedIndex];
            double tableProb = table.BaseProb;
            double currentProb = Convert.ToDouble(txtDisProb.Text);
            double tmpProb = tableProb;
            if (cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch1.Checked)
            {
                tmpProb += 10.0;
            }
            else if (cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch2.Checked)
            {
                tmpProb += 10.0;
            }
            int numOfFails = Convert.ToInt32((currentProb - tmpProb) * 10 / tableProb);
            double jangGi = Convert.ToDouble(txtDisJangGi.Text);
            double allTp = tp + Convert.ToDouble(txtSubBig.Text) * table.SubNumBig
                + Convert.ToDouble(txtSubMed.Text) * table.SubNumMed
                + Convert.ToDouble(txtSubSmall.Text) * table.SubNumSmall;
            if (cBoxLevel.SelectedIndex <= 8)
            {
                allTp += cBoxPart.SelectedIndex == 0 ? Convert.ToDouble(txtWpBook.Text) : Convert.ToDouble(txtArmBook.Text);
            }

            txtResult.AppendText(Environment.NewLine + "=========예상 결과=========");

            // with full meterials
            double bonusProb = Convert.ToDouble(txtDisSubBig.Text) * table.SubNumBig
                + Convert.ToDouble(txtDisSubMed.Text) * table.SubNumMed
                + Convert.ToDouble(txtDisSubSmall.Text) * table.SubNumSmall;
            if (bonusProb > tableProb)
            {
                bonusProb = tableProb;
                //가격 조절 추가
            }
            if (cBoxLevel.SelectedIndex <= 8) bonusProb += 10.0;
            Expected allExpected = Expected.BernoulliDis(allTp, jangGi, currentProb + bonusProb, tableProb * 0.1, numOfFails, cBoxDetails.Checked);
            txtResult.AppendText(Environment.NewLine + $"(풀숨+책) 장기백까지 {allExpected.Count}회 / 오버값: {allExpected.JangGi - 100.0:F2}%");
            txtResult.AppendText(Environment.NewLine + $"기대확률: {allExpected.Value:F3}% / 기대횟수: {allExpected.Tries:F2}");
            txtResult.AppendText(Environment.NewLine + $"기대비용: {Convert.ToInt32(allTp * allExpected.Tries)}G / 최대비용: {Convert.ToInt32(allExpected.MaxCost)}G");
            if (cBoxDetails.Checked) txtResult.AppendText(allExpected.Log);

            // with no metarials
            Expected noExpected = Expected.BernoulliDis(tp, jangGi, currentProb, tableProb * 0.1, numOfFails, cBoxDetails.Checked);
            txtResult.AppendText(Environment.NewLine + $"(노숨) 장기백까지 {noExpected.Count}회 / 오버값: {noExpected.JangGi - 100.0:F2}%", Color.Red);
            txtResult.AppendText(Environment.NewLine + $"기대확률: {noExpected.Value:F3}% / 기대횟수: {noExpected.Tries:F2}", Color.Red);
            txtResult.AppendText(Environment.NewLine + $"기대비용 {Convert.ToInt32(tp * noExpected.Tries)}G / 최대비용: {Convert.ToInt32(noExpected.MaxCost)}G", Color.Red);
            if (cBoxDetails.Checked) txtResult.AppendText(noExpected.Log, Color.Red);

            // with Suggested submeterials
            double pBig = 0.0;
            double pMed = 0.0;
            double pSmall = 0.0;
            double pBook = 0.0;
            if (txtSubBig.Text != "")
                pBig = Convert.ToDouble(txtSubBig.Text) / Convert.ToDouble(txtDisSubBig.Text);
            if (txtSubMed.Text != "")
                pMed = Convert.ToDouble(txtSubMed.Text) / Convert.ToDouble(txtDisSubMed.Text);
            if (txtSubSmall.Text != "")
                pSmall = Convert.ToDouble(txtSubSmall.Text) / Convert.ToDouble(txtDisSubSmall.Text);
            if ((cBoxPart.SelectedIndex == 1 && txtArmBook.Text != "") || (cBoxPart.SelectedIndex == 0 && txtWpBook.Text != ""))
                pBook = (cBoxPart.SelectedIndex == 0 ? Convert.ToDouble(txtWpBook.Text) : Convert.ToDouble(txtArmBook.Text)) / 10.0;
            int i = 0;
            double accProb = 0.0;
            List<Material> mats = new List<Material>();
            Material.SetThresholds(tableProb, tp, currentProb,
                                       txtDisSubBig.Text != "" ? Convert.ToDouble(txtSubBig.Text) : -999, Convert.ToDouble(txtDisSubBig.Text),
                                       txtDisSubMed.Text != "" ? Convert.ToDouble(txtSubMed.Text) : -999, Convert.ToDouble(txtDisSubMed.Text),
                                       txtDisSubSmall.Text != "" ? Convert.ToDouble(txtSubSmall.Text) : -999, Convert.ToDouble(txtDisSubSmall.Text),
                                       cBoxPart.SelectedIndex == 0 ? (txtWpBook.Text != "" ? Convert.ToDouble(txtWpBook.Text) : -999)
                                       : (txtArmBook.Text != "" ? Convert.ToDouble(txtArmBook.Text) : -999));
            for (int fails = numOfFails; jangGi < 100.0 && accProb < 1.0; i++)
            {
                Material mat = new Material().SetMaterial(currentProb, cBoxLevel.SelectedIndex, table.SubNumBig, table.SubNumMed, table.SubNumSmall);
                mats.Add(mat);
                mat.CalcPartialProb(mats);
                accProb += mat.PartialProb;
                jangGi += (mat.CurrentProb + mat.BonusProb) * 0.465;
                if (fails < 10)
                {
                    currentProb += tableProb * 0.1;
                    fails++;
                    
                }
            }

            // Rearrange the mat tables to minimize gold/the number of trials
            if (cBoxOpt.Checked)
            {
                double extraJanggi = Math.Min(Convert.ToDouble(txtDisJangGi.Text) + mats.Last().Janggi(mats) - 100.0, mats.Last().BonusProb * 0.465);
                if (txtSubBig.Text != "" && extraJanggi / 0.465 > Convert.ToDouble(txtDisSubBig.Text) && mats.Last().NumBig > 0
                    || txtSubMed.Text != "" && extraJanggi / 0.465 > Convert.ToDouble(txtDisSubMed.Text) && mats.Last().NumMed > 0
                    || txtSubSmall.Text != "" && extraJanggi / 0.465 > Convert.ToDouble(txtDisSubSmall.Text) && mats.Last().NumSmall > 0
                    || cBoxPart.SelectedIndex == 0 && txtWpBook.Text != "" && extraJanggi / 0.465 > 10.0 && mats.Last().Book
                    || cBoxPart.SelectedIndex == 1 && txtArmBook.Text != "" && extraJanggi / 0.465 > 10.0 && mats.Last().Book)
                {
                    Material.ReduceMaterials(mats, tp, extraJanggi / 0.465);
                    Material.RebuildMaterials(mats);
                }
            }

            // Display the result
            txtResult.AppendText(Environment.NewLine + $"(추천) 장기백까지 {i}회 / 오버값: {(mats.Last().AccProb(mats) < 1.0 ? Convert.ToDouble(txtDisJangGi.Text) + mats.Last().Janggi(mats) - 100.0 : 0.00):F2}%", Color.Blue);
            txtResult.AppendText(Environment.NewLine + $"기대확률: {(mats.Last().AccProb(mats) > 1.0 ? 100.000 : mats.Last().AccProb(mats) * 100):F3}% / 기대횟수: {mats.Last().Tries(mats):F2}", Color.Blue);
            txtResult.AppendText(Environment.NewLine + $"기대비용: {Convert.ToInt32(mats.Last().ExpGold(mats, tp))}G / 최대비용: {Convert.ToInt32(mats.Last().AccGold(mats, tp))}G", Color.Blue);
        #if DEBUG
            txtResult.AppendText(Environment.NewLine + $"Thresholds: {Material.ThresholdBig:F3}, {Material.ThresholdMed:F3}, {Material.ThresholdSmall:F3}, {Material.ThresholdBook:F3}", Color.Blue);
#endif
            txtResult.AppendText(Environment.NewLine + "당신은 " + (numOfFails < 10 ? $"{numOfFails + 1}번째 시도입니다" : "10+번째 시도입니다"), Color.Blue);
            txtResult.AppendText(Environment.NewLine + "(상세 루트)");
            for (i = 0; i < mats.Count; i++)
            {
                txtResult.AppendText(Environment.NewLine + $"{numOfFails + i + 1})", Color.Blue);
                if (mats[i].NumBig > 0) txtResult.AppendText($"+가호{mats[i].NumBig}", Color.Blue);
                if (mats[i].NumMed > 0) txtResult.AppendText($"+축복{mats[i].NumMed}", Color.Blue);
                if (mats[i].NumSmall > 0) txtResult.AppendText($"+은총{mats[i].NumSmall}", Color.Blue);
                if (mats[i].Book) txtResult.AppendText("+책", Color.Blue);
                if (mats[i].NumBig == 0 && mats[i].NumMed == 0 && mats[i].NumSmall == 0 && !mats[i].Book) txtResult.AppendText("노숨", Color.Blue); ;
                if (cBoxDetails.Checked)
                {
                    txtResult.AppendText(Environment.NewLine + $"누적 기댓값: {mats[i].AccProb(mats) * 100:F3}%"
                        + Environment.NewLine + $"누적 소모비용: {Convert.ToInt32(mats[i].AccGold(mats, tp))}G / 장기: {Convert.ToDouble(txtDisJangGi.Text) + mats[i].Janggi(mats):F2}%");
                #if DEBUG
                    txtResult.AppendText(Environment.NewLine + $"비용: {Convert.ToInt32(tp + mats[i].Gold)}G / 확률: {mats[i].CurrentProb + mats[i].BonusProb:F2}%");
                #endif
                }
            }
        }

        private void calcEfficiency()
        {
            try
            {
                double pStone = (cBoxPart.SelectedIndex == 0 ? Convert.ToDouble(txtWpStone.Text) : Convert.ToDouble(txtArmStone.Text)) / 10.0;
                double pEnhance = Convert.ToDouble(txtEnhance.Text);
                double pOreha = Convert.ToDouble(txtOreha.Text);
                double pHonour = Convert.ToDouble(txtHonour.Text) / (cBoxHonour.SelectedIndex + 1) / 500.0;

                int nStone = Convert.ToInt32(txtDisStone.Text);
                int nEnhance = Convert.ToInt32(txtDisEnhance.Text);
                int nOreha = Convert.ToInt32(txtDisOreha.Text);
                int nHonour = Convert.ToInt32(txtDisHonour.Text);
                int gold = Convert.ToInt32(txtDisGold.Text);

                double basePrice = pStone * nStone + pEnhance * nEnhance + pOreha * nOreha + pHonour * nHonour + gold;
                
                double ppp = basePrice / Convert.ToDouble(txtDisProb.Text);

                txtResult.Text = $"효율 계산 결과:\n기본 1%당 {ppp:F2}G";

                bool fBig = false;
                if (txtSubBig.Text != "")
                {
                    double pBig = Convert.ToDouble(txtSubBig.Text) / Convert.ToDouble(txtDisSubBig.Text);
                    if (pBig <= ppp)
                    {
                        txtResult.AppendText("\n(추천) ", Color.Blue);
                        fBig = true;
                    }
                    else
                    {
                        txtResult.AppendText("\n(비추천) ", Color.Red);
                    }
                    txtResult.AppendText($"가호 1%당 {pBig:F2}G");
                }

                bool fMed = false;
                if (txtSubMed.Text != "")
                {
                    double pMed = Convert.ToDouble(txtSubMed.Text) / Convert.ToDouble(txtDisSubMed.Text);
                    if (pMed <= ppp)
                    {
                        txtResult.AppendText("\n(추천) ", Color.Blue);
                        fMed = true;
                    }
                    else
                    {
                        txtResult.AppendText("\n(비추천) ", Color.Red);
                    }
                    txtResult.AppendText($"축복 1%당 {pMed:F2}G");
                }

                bool fSmall = false;
                if (txtSubSmall.Text != "")
                {
                    double pSmall = Convert.ToDouble(txtSubSmall.Text) / Convert.ToDouble(txtDisSubSmall.Text);
                    if (pSmall <= ppp)
                    {
                        txtResult.AppendText("\n(추천) ", Color.Blue);
                        fSmall = true;
                    }
                    else
                    {
                        txtResult.AppendText("\n(비추천) ", Color.Red);
                    }
                    txtResult.AppendText($"은총 1%당 {pSmall:F2}G");
                }

                bool fBook = false;
                
                if ((cBoxPart.SelectedIndex == 1 && txtArmBook.Text != "") || (cBoxPart.SelectedIndex == 0 && txtWpBook.Text != ""))
                {
                    if (cBoxLevel.SelectedIndex <= 8)
                    {
                        double pBook = (cBoxPart.SelectedIndex == 0 ? Convert.ToDouble(txtWpBook.Text) : Convert.ToDouble(txtArmBook.Text)) / 10.0;
                        if (pBook <= ppp)
                        {
                            txtResult.AppendText("\n(추천) ", Color.Blue);
                            fBook = true;
                        }
                        else
                        {
                            txtResult.AppendText("\n(비추천) ", Color.Red);
                        }
                        txtResult.AppendText($"야금/재봉 1%당 {pBook:F2}G");
                    }
                    else
                    {
                        txtResult.AppendText("\n(미사용:장비레벨 초과) 야금/재봉", Color.DarkSlateGray);
                    }
                }

                CalcJangGi(basePrice, fBig, fMed, fSmall, fBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (cBoxGrade.SelectedIndex == -1 || cBoxPart.SelectedIndex == -1 || cBoxLevel.SelectedIndex == -1)
            {
                MessageBox.Show("장비부터 전부 선택하세요 ㅆㅂㄹㅁ");
                return;
            }
            calcEfficiency();
        }

        private void SetDisInfo()
        {
            try
            {
                if (cBoxGrade.SelectedIndex == 0 && cBoxPart.SelectedIndex == 0)
                {
                    tables = TableManager.tbWp1302;
                }
                else if (cBoxGrade.SelectedIndex == 0 && cBoxPart.SelectedIndex == 1)
                {
                    tables = TableManager.tbArm1302;
                }
                else if (cBoxGrade.SelectedIndex == 1 && cBoxPart.SelectedIndex == 0)
                {
                    tables = TableManager.tbWp1340;
                }
                else if (cBoxGrade.SelectedIndex == 1 && cBoxPart.SelectedIndex == 1)
                {
                    tables = TableManager.tbArm1340;
                }
                txtDisStone.Text = tables[cBoxLevel.SelectedIndex].StoneNeeds.ToString();
                txtDisEnhance.Text = tables[cBoxLevel.SelectedIndex].EnhanceNeeds.ToString();
                txtDisOreha.Text = tables[cBoxLevel.SelectedIndex].OrehaNeeds.ToString();
                txtDisHonour.Text = tables[cBoxLevel.SelectedIndex].HonourNeeds.ToString();
                txtDisGold.Text = tables[cBoxLevel.SelectedIndex].GoldNeeds.ToString();
                baseProb = tables[cBoxLevel.SelectedIndex].BaseProb;
                txtDisProb.Text = GetEditedBaseProb(baseProb).ToString("F3");
                txtDisSubBig.Text = tables[cBoxLevel.SelectedIndex].SubProbBig.ToString("F2");
                txtDisSubMed.Text = tables[cBoxLevel.SelectedIndex].SubProbMed.ToString("F2");
                txtDisSubSmall.Text = tables[cBoxLevel.SelectedIndex].SubProbSmall.ToString("F2");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + cBoxLevel.SelectedIndex.ToString() + "\n" + ex.StackTrace);
            }
        }

        private void cBoxGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtResult.Text = "";
            if (cBoxGrade.SelectedIndex != -1 && cBoxPart.SelectedIndex != -1 && cBoxLevel.SelectedIndex != -1)
                SetDisInfo();
        }

        private void cBoxPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtResult.Text = "";
            if (cBoxGrade.SelectedIndex != -1 && cBoxPart.SelectedIndex != -1 && cBoxLevel.SelectedIndex != -1)
                SetDisInfo();
        }

        private void cBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtResult.Text = "";
            if (cBoxGrade.SelectedIndex != -1 && cBoxPart.SelectedIndex != -1 && cBoxLevel.SelectedIndex != -1)
                SetDisInfo();
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            if (cBoxGrade.SelectedIndex == -1 || cBoxPart.SelectedIndex == -1 || cBoxLevel.SelectedIndex == -1)
            {
                MessageBox.Show("장비부터 전부 선택하세요 ㅆㅂㄹㅁ");
                return;
            }
            if (cBoxLevel.SelectedIndex < cBoxLevel.Items.Count - 1)
            {
                cBoxLevel.SelectedIndex++;
                calcEfficiency();
            }
        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            if (cBoxGrade.SelectedIndex == -1 || cBoxPart.SelectedIndex == -1 || cBoxLevel.SelectedIndex == -1)
            {
                MessageBox.Show("장비부터 전부 선택하세요 ㅆㅂㄹㅁ");
                return;
            }
            double currentProb = Convert.ToDouble(txtDisProb.Text);
            if(currentProb < tables[cBoxLevel.SelectedIndex].BaseProb * 2 + 10 * Convert.ToInt32((cBoxResearch1.Checked && cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex <= 8)
                || cBoxResearch2.Checked && cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex <= 8))
            {
                txtDisProb.Text = (currentProb + tables[cBoxLevel.SelectedIndex].BaseProb * 0.1).ToString("F3");
                calcEfficiency();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                int i;
                Latest latest = new Latest();
                if (int.TryParse(txtWpStone.Text, out i))
                    latest.WpStone = i;
                if (int.TryParse(txtArmStone.Text, out i))
                    latest.ArmStone = i;
                if (int.TryParse(txtEnhance.Text, out i))
                    latest.Enhance = i;
                if (int.TryParse(txtOreha.Text, out i))
                    latest.Oreha = i;
                if (int.TryParse(txtHonour.Text, out i))
                    latest.Honour = i;
                if (int.TryParse(txtSubBig.Text, out i))
                    latest.SubBig = i;
                if (int.TryParse(txtSubMed.Text, out i))
                    latest.SubMed = i;
                if (int.TryParse(txtSubSmall.Text, out i))
                    latest.SubSmall = i;
                if (int.TryParse(txtWpBook.Text, out i))
                    latest.WpBook = i;
                if (int.TryParse(txtArmBook.Text, out i))
                    latest.ArmBook = i;
                latest.HonourLevel = cBoxHonour.SelectedIndex;
                latest.EquipGrade = cBoxGrade.SelectedIndex;
                latest.EquipPart = cBoxPart.SelectedIndex;
                latest.EquipLevel = cBoxLevel.SelectedIndex;
                double d;
                if (double.TryParse(txtDisProb.Text, out d))
                {
                    if (cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch1.Checked) d -= 10;
                    if (cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch2.Checked) d -= 10.0;
                    latest.Prob = d;
                }
                if (double.TryParse(txtDisJangGi.Text, out d))
                    latest.JangGi = d;
                latest.Research1 = cBoxResearch1.Checked;
                latest.Research2 = cBoxResearch2.Checked;
                latest.Details = cBoxDetails.Checked;
                latest.Opti = cBoxOpt.Checked;

                Latest.WriteFile(latest);
            }
            catch
            {
                throw new Exception("Error from FormClosing");
            }
        }

        private double GetEditedBaseProb(double bProb)
        {
            if (cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch1.Checked)
            {
                return baseProb = bProb + 10.0;
            }
            else if (cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch2.Checked)
            {
                return baseProb = bProb + 10.0;
            }
            else
            {
                return baseProb = bProb;
            }
        }

        private void cBoxResearch1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex != -1 && txtDisProb.Text != "")
                {
                    if (cBoxResearch1.Checked)
                    {
                        txtDisProb.Text = GetEditedBaseProb(Convert.ToDouble(txtDisProb.Text)).ToString("F3");
                    }
                    else if (!cBoxResearch1.Checked)
                    {
                        baseProb = Convert.ToDouble(txtDisProb.Text) - 10.0;
                        txtDisProb.Text = baseProb.ToString("F3");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cBoxResearch2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex != -1 && txtDisProb.Text != "")
                {
                    if (cBoxResearch2.Checked)
                    {
                        txtDisProb.Text = GetEditedBaseProb(Convert.ToDouble(txtDisProb.Text)).ToString("F3");
                    }
                    else if (!cBoxResearch2.Checked)
                    {
                        baseProb = Convert.ToDouble(txtDisProb.Text) - 10.0;
                        txtDisProb.Text = baseProb.ToString("F3");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
