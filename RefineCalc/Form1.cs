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
                txtDisProb.Text = latest.Prob.ToString("F2");
                txtDisJangGi.Text = latest.JangGi.ToString("F2");
                cBoxResearch1.Checked = latest.Research1;
                cBoxResearch2.Checked = latest.Research2;
                cBoxDetails.Checked = latest.Details;
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
            double tmpProb = baseProb;
            if (cBoxGrade.SelectedIndex == 0 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch1.Checked)
            {
                tmpProb -= 10.0;
            }
            else if (cBoxGrade.SelectedIndex == 1 && cBoxLevel.SelectedIndex <= 8 && cBoxResearch2.Checked)
            {
                tmpProb -= 10.0;
            }
            int numOfFails = Convert.ToInt32((currentProb - tmpProb) * 10 / tableProb);
            double jangGi = Convert.ToDouble(txtDisJangGi.Text);
            double sugTp;
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
            double accQ = 1.0;
            double tries = 0.0;
            double accGold = 0.0;
            double totalGold = 0.0;
            string sugStr = Environment.NewLine + "당신은 ";
            if (numOfFails < 10)
            {
                sugStr += $"{numOfFails + 1}번째 시도입니다" + Environment.NewLine + "(상세 루트)";
            }
            else
            {
                sugStr += "10+번째 시도입니다";
            }
            for (; jangGi < 100.0; i++)
            {
                double ppp = tp / currentProb;
                /*
                fBig = false;
                fMed = false;
                fSmall = false;
                fBook = false;
                bonusProb = 0.0;
                sugTp = tp;
                if (txtSubBig.Text != "" && pBig <= ppp)
                {
                    fBig = true;
                    bonusProb += Convert.ToDouble(txtDisSubBig.Text) * tables[cBoxLevel.SelectedIndex].SubNumBig;
                    sugTp += Convert.ToDouble(txtSubBig.Text) * tables[cBoxLevel.SelectedIndex].SubNumBig;
                }
                if (txtSubMed.Text != "" && pMed <= ppp)
                {
                    fMed = true;
                    bonusProb += Convert.ToDouble(txtDisSubMed.Text) * tables[cBoxLevel.SelectedIndex].SubNumMed;
                    sugTp += Convert.ToDouble(txtSubMed.Text) * tables[cBoxLevel.SelectedIndex].SubNumMed;
                }
                if (txtSubSmall.Text != "" && pSmall <= ppp)
                {
                    fSmall = true;
                    bonusProb += Convert.ToDouble(txtDisSubSmall.Text) * tables[cBoxLevel.SelectedIndex].SubNumSmall;
                    sugTp += Convert.ToDouble(txtSubSmall.Text) * tables[cBoxLevel.SelectedIndex].SubNumSmall;
                }
                if (bonusProb > tableProb)
                {
                    bonusProb = tableProb;
                    // 가격 조절
                }
                if (cBoxLevel.SelectedIndex <= 8)
                {
                    if ((cBoxPart.SelectedIndex == 1 && txtArmBook.Text != "") || (cBoxPart.SelectedIndex == 0 && txtWpBook.Text != ""))
                    {
                        if (pBook <= ppp)
                        {
                            fBook = true;
                            bonusProb += 10.0;
                            sugTp += cBoxPart.SelectedIndex == 0 ? Convert.ToDouble(txtWpBook.Text) : Convert.ToDouble(txtArmBook.Text);
                        }
                    }
                }
                */
                Material mat = Material.CreateMaterial(table.SubNumBig, table.SubNumMed, table.SubNumSmall,
                                                Convert.ToDouble(txtDisSubBig.Text), Convert.ToDouble(txtDisSubMed.Text), Convert.ToDouble(txtDisSubSmall.Text), currentProb, tableProb,
                                                txtSubBig.Text, txtSubMed.Text, txtSubSmall.Text, cBoxPart.SelectedIndex == 0 ? txtWpBook.Text : txtArmBook.Text,
                                                cBoxLevel.SelectedIndex, ppp);
                double tempProb = currentProb + mat.BonusProb;
                sugTp = tp + mat.Gold;
                accProb += accQ * (tempProb / 100.0);
                accGold += (i + 1) * sugTp * accQ * (tempProb / 100.0);
                tries += (i + 1) * accQ * (tempProb / 100.0);
                accQ *= 1 - (tempProb / 100.0);
                jangGi += tempProb * 0.465;
                totalGold += sugTp;
                if (numOfFails < 10)
                {
                    currentProb += tableProb * 0.1;
                    numOfFails++;
                    
                }
                sugStr += Environment.NewLine + $"{i + 1}) ";
                if (mat.NumBig > 0) sugStr += $"+가호{mat.NumBig}";
                if (mat.NumMed > 0) sugStr += $"+축복{mat.NumMed}";
                if (mat.NumSmall > 0) sugStr += $"+은총{mat.NumSmall}";
                if (mat.Book) sugStr += "+책";
                if (!(mat.NumBig > 0) && !(mat.NumMed > 0) && !(mat.NumSmall > 0) && !mat.Book) sugStr += "노숨";
                if (cBoxDetails.Checked)
                {
                    sugStr += Environment.NewLine + $"누적 기댓값: {accProb * 100:F3}%" + Environment.NewLine + $"누적 소모비용: {Convert.ToInt32(totalGold)}G / 장기: {jangGi:F2}%";
                    sugStr += Environment.NewLine + $"{sugTp:F2}G / {tempProb:F2}% / {sugTp / tempProb:F2}";
                }
            }
            txtResult.AppendText(Environment.NewLine + $"(추천) 장기백까지 {i}회 / 오버값: {jangGi - 100.0:F2}%", Color.Blue);
            txtResult.AppendText(Environment.NewLine + $"기대확률: {accProb * 100:F3}% / 기대횟수: {tries:F2}", Color.Blue);
            txtResult.AppendText(Environment.NewLine + $"기대비용: {Convert.ToInt32(accGold)}G / 최대비용: {Convert.ToInt32(totalGold)}G", Color.Blue);
            txtResult.AppendText(sugStr, Color.Blue);
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
                txtDisProb.Text = GetEditedBaseProb(baseProb).ToString("F2");
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
            if(currentProb < baseProb * 2)
            {
                txtDisProb.Text = (currentProb + baseProb * 0.1).ToString("F2");
                calcEfficiency();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                int i;
                Latest latest = new Latest();
                if(int.TryParse(txtWpStone.Text, out i))
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
                    latest.Prob = d;
                if (double.TryParse(txtDisJangGi.Text, out d))
                    latest.JangGi = d;
                latest.Research1 = cBoxResearch1.Checked;
                latest.Research2 = cBoxResearch2.Checked;
                latest.Details = cBoxDetails.Checked;

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
                        txtDisProb.Text = GetEditedBaseProb(Convert.ToDouble(txtDisProb.Text)).ToString("F2");
                    }
                    else if (!cBoxResearch1.Checked)
                    {
                        txtDisProb.Text = (Convert.ToDouble(txtDisProb.Text) - 10.0).ToString("F2"); ;
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
                        txtDisProb.Text = GetEditedBaseProb(Convert.ToDouble(txtDisProb.Text)).ToString("F2");
                    }
                    else if (!cBoxResearch2.Checked)
                    {
                        txtDisProb.Text = (Convert.ToDouble(txtDisProb.Text) - 10.0).ToString("F2"); ;
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
