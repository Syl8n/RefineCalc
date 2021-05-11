namespace RefineCalc
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpEquipment = new System.Windows.Forms.GroupBox();
            this.cBoxLevel = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cBoxPart = new System.Windows.Forms.ComboBox();
            this.lblPart = new System.Windows.Forms.Label();
            this.cBoxGrade = new System.Windows.Forms.ComboBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.gBoxMeterials = new System.Windows.Forms.GroupBox();
            this.cBoxResearch2 = new System.Windows.Forms.CheckBox();
            this.cBoxResearch1 = new System.Windows.Forms.CheckBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSubSmall = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubMed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubBig = new System.Windows.Forms.TextBox();
            this.txtWpBook = new System.Windows.Forms.TextBox();
            this.txtArmBook = new System.Windows.Forms.TextBox();
            this.txtEnhance = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cBoxHonour = new System.Windows.Forms.ComboBox();
            this.txtOreha = new System.Windows.Forms.TextBox();
            this.txtHonour = new System.Windows.Forms.TextBox();
            this.txtArmStone = new System.Windows.Forms.TextBox();
            this.txtWpStone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDisJangGi = new System.Windows.Forms.TextBox();
            this.txtDisSubSmall = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDisStone = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDisSubMed = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDisEnhance = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDisSubBig = new System.Windows.Forms.TextBox();
            this.txtDisProb = new System.Windows.Forms.TextBox();
            this.txtDisOreha = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDisHonour = new System.Windows.Forms.TextBox();
            this.txtDisGold = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.btnSuccess = new System.Windows.Forms.Button();
            this.btnFail = new System.Windows.Forms.Button();
            this.cBoxDetails = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cBoxOpt = new System.Windows.Forms.CheckBox();
            this.grpEquipment.SuspendLayout();
            this.gBoxMeterials.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEquipment
            // 
            this.grpEquipment.Controls.Add(this.cBoxLevel);
            this.grpEquipment.Controls.Add(this.lblLevel);
            this.grpEquipment.Controls.Add(this.cBoxPart);
            this.grpEquipment.Controls.Add(this.lblPart);
            this.grpEquipment.Controls.Add(this.cBoxGrade);
            this.grpEquipment.Controls.Add(this.lblGrade);
            this.grpEquipment.Location = new System.Drawing.Point(12, 15);
            this.grpEquipment.Name = "grpEquipment";
            this.grpEquipment.Size = new System.Drawing.Size(355, 60);
            this.grpEquipment.TabIndex = 0;
            this.grpEquipment.TabStop = false;
            this.grpEquipment.Text = "장비 선택";
            // 
            // cBoxLevel
            // 
            this.cBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBoxLevel.FormattingEnabled = true;
            this.cBoxLevel.Items.AddRange(new object[] {
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.cBoxLevel.Location = new System.Drawing.Point(285, 25);
            this.cBoxLevel.Name = "cBoxLevel";
            this.cBoxLevel.Size = new System.Drawing.Size(51, 20);
            this.cBoxLevel.TabIndex = 5;
            this.cBoxLevel.SelectedIndexChanged += new System.EventHandler(this.cBoxLevel_SelectedIndexChanged);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(221, 29);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(61, 12);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.Text = "목표 단계:";
            // 
            // cBoxPart
            // 
            this.cBoxPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBoxPart.FormattingEnabled = true;
            this.cBoxPart.Items.AddRange(new object[] {
            "무기",
            "방어구"});
            this.cBoxPart.Location = new System.Drawing.Point(148, 25);
            this.cBoxPart.Name = "cBoxPart";
            this.cBoxPart.Size = new System.Drawing.Size(67, 20);
            this.cBoxPart.TabIndex = 3;
            this.cBoxPart.SelectedIndexChanged += new System.EventHandler(this.cBoxPart_SelectedIndexChanged);
            // 
            // lblPart
            // 
            this.lblPart.AutoSize = true;
            this.lblPart.Location = new System.Drawing.Point(112, 29);
            this.lblPart.Name = "lblPart";
            this.lblPart.Size = new System.Drawing.Size(33, 12);
            this.lblPart.TabIndex = 2;
            this.lblPart.Text = "부위:";
            // 
            // cBoxGrade
            // 
            this.cBoxGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBoxGrade.FormattingEnabled = true;
            this.cBoxGrade.Items.AddRange(new object[] {
            "1302",
            "1340"});
            this.cBoxGrade.Location = new System.Drawing.Point(49, 25);
            this.cBoxGrade.Name = "cBoxGrade";
            this.cBoxGrade.Size = new System.Drawing.Size(56, 20);
            this.cBoxGrade.TabIndex = 1;
            this.cBoxGrade.SelectedIndexChanged += new System.EventHandler(this.cBoxGrade_SelectedIndexChanged);
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(13, 29);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(33, 12);
            this.lblGrade.TabIndex = 0;
            this.lblGrade.Text = "등급:";
            // 
            // gBoxMeterials
            // 
            this.gBoxMeterials.Controls.Add(this.cBoxResearch2);
            this.gBoxMeterials.Controls.Add(this.cBoxResearch1);
            this.gBoxMeterials.Controls.Add(this.btnCalc);
            this.gBoxMeterials.Controls.Add(this.label9);
            this.gBoxMeterials.Controls.Add(this.label8);
            this.gBoxMeterials.Controls.Add(this.txtSubSmall);
            this.gBoxMeterials.Controls.Add(this.label7);
            this.gBoxMeterials.Controls.Add(this.txtSubMed);
            this.gBoxMeterials.Controls.Add(this.label5);
            this.gBoxMeterials.Controls.Add(this.txtSubBig);
            this.gBoxMeterials.Controls.Add(this.txtWpBook);
            this.gBoxMeterials.Controls.Add(this.txtArmBook);
            this.gBoxMeterials.Controls.Add(this.txtEnhance);
            this.gBoxMeterials.Controls.Add(this.label19);
            this.gBoxMeterials.Controls.Add(this.label2);
            this.gBoxMeterials.Controls.Add(this.label4);
            this.gBoxMeterials.Controls.Add(this.cBoxHonour);
            this.gBoxMeterials.Controls.Add(this.txtOreha);
            this.gBoxMeterials.Controls.Add(this.txtHonour);
            this.gBoxMeterials.Controls.Add(this.txtArmStone);
            this.gBoxMeterials.Controls.Add(this.txtWpStone);
            this.gBoxMeterials.Controls.Add(this.label3);
            this.gBoxMeterials.Controls.Add(this.label6);
            this.gBoxMeterials.Controls.Add(this.label1);
            this.gBoxMeterials.Location = new System.Drawing.Point(11, 214);
            this.gBoxMeterials.Name = "gBoxMeterials";
            this.gBoxMeterials.Size = new System.Drawing.Size(355, 182);
            this.gBoxMeterials.TabIndex = 1;
            this.gBoxMeterials.TabStop = false;
            this.gBoxMeterials.Text = "재료 가격";
            // 
            // cBoxResearch2
            // 
            this.cBoxResearch2.AutoSize = true;
            this.cBoxResearch2.Location = new System.Drawing.Point(176, 159);
            this.cBoxResearch2.Name = "cBoxResearch2";
            this.cBoxResearch2.Size = new System.Drawing.Size(78, 16);
            this.cBoxResearch2.TabIndex = 7;
            this.cBoxResearch2.Text = "영지연구2";
            this.cBoxResearch2.UseVisualStyleBackColor = true;
            this.cBoxResearch2.CheckedChanged += new System.EventHandler(this.cBoxResearch2_CheckedChanged);
            // 
            // cBoxResearch1
            // 
            this.cBoxResearch1.AutoSize = true;
            this.cBoxResearch1.Location = new System.Drawing.Point(176, 138);
            this.cBoxResearch1.Name = "cBoxResearch1";
            this.cBoxResearch1.Size = new System.Drawing.Size(78, 16);
            this.cBoxResearch1.TabIndex = 7;
            this.cBoxResearch1.Text = "영지연구1";
            this.cBoxResearch1.UseVisualStyleBackColor = true;
            this.cBoxResearch1.CheckedChanged += new System.EventHandler(this.cBoxResearch1_CheckedChanged);
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCalc.Location = new System.Drawing.Point(254, 134);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(90, 42);
            this.btnCalc.TabIndex = 6;
            this.btnCalc.Text = "계산하기";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "태양의 은총:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "태양의 축복:";
            // 
            // txtSubSmall
            // 
            this.txtSubSmall.Location = new System.Drawing.Point(92, 155);
            this.txtSubSmall.Name = "txtSubSmall";
            this.txtSubSmall.Size = new System.Drawing.Size(65, 21);
            this.txtSubSmall.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "태양의 가호:";
            // 
            // txtSubMed
            // 
            this.txtSubMed.Location = new System.Drawing.Point(92, 129);
            this.txtSubMed.Name = "txtSubMed";
            this.txtSubMed.Size = new System.Drawing.Size(65, 21);
            this.txtSubMed.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "오레하:";
            // 
            // txtSubBig
            // 
            this.txtSubBig.Location = new System.Drawing.Point(92, 103);
            this.txtSubBig.Name = "txtSubBig";
            this.txtSubBig.Size = new System.Drawing.Size(65, 21);
            this.txtSubBig.TabIndex = 4;
            // 
            // txtWpBook
            // 
            this.txtWpBook.Location = new System.Drawing.Point(254, 74);
            this.txtWpBook.Name = "txtWpBook";
            this.txtWpBook.Size = new System.Drawing.Size(65, 21);
            this.txtWpBook.TabIndex = 4;
            // 
            // txtArmBook
            // 
            this.txtArmBook.Location = new System.Drawing.Point(254, 99);
            this.txtArmBook.Name = "txtArmBook";
            this.txtArmBook.Size = new System.Drawing.Size(65, 21);
            this.txtArmBook.TabIndex = 4;
            // 
            // txtEnhance
            // 
            this.txtEnhance.Location = new System.Drawing.Point(254, 49);
            this.txtEnhance.Name = "txtEnhance";
            this.txtEnhance.Size = new System.Drawing.Size(65, 21);
            this.txtEnhance.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(202, 77);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 12);
            this.label19.TabIndex = 3;
            this.label19.Text = "야금술:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "재봉술:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "돌파석:";
            // 
            // cBoxHonour
            // 
            this.cBoxHonour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxHonour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBoxHonour.FormattingEnabled = true;
            this.cBoxHonour.Items.AddRange(new object[] {
            "소",
            "중",
            "대"});
            this.cBoxHonour.Location = new System.Drawing.Point(254, 22);
            this.cBoxHonour.Name = "cBoxHonour";
            this.cBoxHonour.Size = new System.Drawing.Size(36, 20);
            this.cBoxHonour.TabIndex = 2;
            // 
            // txtOreha
            // 
            this.txtOreha.Location = new System.Drawing.Point(92, 74);
            this.txtOreha.Name = "txtOreha";
            this.txtOreha.Size = new System.Drawing.Size(65, 21);
            this.txtOreha.TabIndex = 1;
            // 
            // txtHonour
            // 
            this.txtHonour.Location = new System.Drawing.Point(296, 22);
            this.txtHonour.Name = "txtHonour";
            this.txtHonour.Size = new System.Drawing.Size(48, 21);
            this.txtHonour.TabIndex = 1;
            // 
            // txtArmStone
            // 
            this.txtArmStone.Location = new System.Drawing.Point(92, 48);
            this.txtArmStone.Name = "txtArmStone";
            this.txtArmStone.Size = new System.Drawing.Size(65, 21);
            this.txtArmStone.TabIndex = 1;
            // 
            // txtWpStone
            // 
            this.txtWpStone.Location = new System.Drawing.Point(92, 21);
            this.txtWpStone.Name = "txtWpStone";
            this.txtWpStone.Size = new System.Drawing.Size(65, 21);
            this.txtWpStone.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "명예의 파편:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "수호석(10개):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "파괴석(10개):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDisJangGi);
            this.groupBox1.Controls.Add(this.txtDisSubSmall);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtDisStone);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtDisSubMed);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtDisEnhance);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtDisSubBig);
            this.groupBox1.Controls.Add(this.txtDisProb);
            this.groupBox1.Controls.Add(this.txtDisOreha);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtDisHonour);
            this.groupBox1.Controls.Add(this.txtDisGold);
            this.groupBox1.Location = new System.Drawing.Point(13, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 125);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "정보 확인(다르면 수정)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(123, 99);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(153, 12);
            this.label26.TabIndex = 6;
            this.label26.Text = "(Optional) 장인의 기운(%):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "파괴석/수호석:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "성공률:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(256, 75);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 12);
            this.label18.TabIndex = 5;
            this.label18.Text = "은총:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(162, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "골드:";
            // 
            // txtDisJangGi
            // 
            this.txtDisJangGi.Location = new System.Drawing.Point(280, 95);
            this.txtDisJangGi.Name = "txtDisJangGi";
            this.txtDisJangGi.Size = new System.Drawing.Size(55, 21);
            this.txtDisJangGi.TabIndex = 4;
            // 
            // txtDisSubSmall
            // 
            this.txtDisSubSmall.Location = new System.Drawing.Point(295, 70);
            this.txtDisSubSmall.Name = "txtDisSubSmall";
            this.txtDisSubSmall.Size = new System.Drawing.Size(40, 21);
            this.txtDisSubSmall.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(162, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 12);
            this.label17.TabIndex = 5;
            this.label17.Text = "축복:";
            // 
            // txtDisStone
            // 
            this.txtDisStone.Location = new System.Drawing.Point(105, 22);
            this.txtDisStone.Name = "txtDisStone";
            this.txtDisStone.Size = new System.Drawing.Size(39, 21);
            this.txtDisStone.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(150, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "돌파석:";
            // 
            // txtDisSubMed
            // 
            this.txtDisSubMed.Location = new System.Drawing.Point(200, 70);
            this.txtDisSubMed.Name = "txtDisSubMed";
            this.txtDisSubMed.Size = new System.Drawing.Size(38, 21);
            this.txtDisSubMed.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(66, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 12);
            this.label16.TabIndex = 5;
            this.label16.Text = "가호:";
            // 
            // txtDisEnhance
            // 
            this.txtDisEnhance.Location = new System.Drawing.Point(200, 23);
            this.txtDisEnhance.Name = "txtDisEnhance";
            this.txtDisEnhance.Size = new System.Drawing.Size(38, 21);
            this.txtDisEnhance.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(244, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "오레하:";
            // 
            // txtDisSubBig
            // 
            this.txtDisSubBig.Location = new System.Drawing.Point(105, 70);
            this.txtDisSubBig.Name = "txtDisSubBig";
            this.txtDisSubBig.Size = new System.Drawing.Size(40, 21);
            this.txtDisSubBig.TabIndex = 4;
            // 
            // txtDisProb
            // 
            this.txtDisProb.Location = new System.Drawing.Point(295, 46);
            this.txtDisProb.Name = "txtDisProb";
            this.txtDisProb.Size = new System.Drawing.Size(40, 21);
            this.txtDisProb.TabIndex = 1;
            // 
            // txtDisOreha
            // 
            this.txtDisOreha.Location = new System.Drawing.Point(295, 22);
            this.txtDisOreha.Name = "txtDisOreha";
            this.txtDisOreha.Size = new System.Drawing.Size(40, 21);
            this.txtDisOreha.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "명예의 파편:";
            // 
            // txtDisHonour
            // 
            this.txtDisHonour.Location = new System.Drawing.Point(105, 46);
            this.txtDisHonour.Name = "txtDisHonour";
            this.txtDisHonour.Size = new System.Drawing.Size(39, 21);
            this.txtDisHonour.TabIndex = 1;
            // 
            // txtDisGold
            // 
            this.txtDisGold.Location = new System.Drawing.Point(200, 46);
            this.txtDisGold.Name = "txtDisGold";
            this.txtDisGold.Size = new System.Drawing.Size(38, 21);
            this.txtDisGold.TabIndex = 4;
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.Location = new System.Drawing.Point(384, 21);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(264, 317);
            this.txtResult.TabIndex = 4;
            this.txtResult.Text = "";
            // 
            // btnSuccess
            // 
            this.btnSuccess.Location = new System.Drawing.Point(496, 348);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(70, 42);
            this.btnSuccess.TabIndex = 5;
            this.btnSuccess.Text = "성공";
            this.btnSuccess.UseVisualStyleBackColor = true;
            this.btnSuccess.Click += new System.EventHandler(this.btnSuccess_Click);
            // 
            // btnFail
            // 
            this.btnFail.Location = new System.Drawing.Point(578, 348);
            this.btnFail.Name = "btnFail";
            this.btnFail.Size = new System.Drawing.Size(70, 42);
            this.btnFail.TabIndex = 5;
            this.btnFail.Text = "실패";
            this.btnFail.UseVisualStyleBackColor = true;
            this.btnFail.Click += new System.EventHandler(this.btnFail_Click);
            // 
            // cBoxDetails
            // 
            this.cBoxDetails.AutoSize = true;
            this.cBoxDetails.Location = new System.Drawing.Point(384, 348);
            this.cBoxDetails.Name = "cBoxDetails";
            this.cBoxDetails.Size = new System.Drawing.Size(100, 16);
            this.cBoxDetails.TabIndex = 6;
            this.cBoxDetails.Text = "세부내역 표시";
            this.cBoxDetails.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(397, 365);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 12);
            this.label20.TabIndex = 7;
            this.label20.Text = "(체크 후 재계산)";
            // 
            // cBoxOpt
            // 
            this.cBoxOpt.AutoSize = true;
            this.cBoxOpt.Location = new System.Drawing.Point(384, 380);
            this.cBoxOpt.Name = "cBoxOpt";
            this.cBoxOpt.Size = new System.Drawing.Size(60, 16);
            this.cBoxOpt.TabIndex = 8;
            this.cBoxOpt.Text = "최적화";
            this.cBoxOpt.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(660, 408);
            this.Controls.Add(this.cBoxOpt);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cBoxDetails);
            this.Controls.Add(this.btnFail);
            this.Controls.Add(this.btnSuccess);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gBoxMeterials);
            this.Controls.Add(this.grpEquipment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "강화 효율 계산기";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpEquipment.ResumeLayout(false);
            this.grpEquipment.PerformLayout();
            this.gBoxMeterials.ResumeLayout(false);
            this.gBoxMeterials.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEquipment;
        private System.Windows.Forms.ComboBox cBoxGrade;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.ComboBox cBoxPart;
        private System.Windows.Forms.Label lblPart;
        private System.Windows.Forms.ComboBox cBoxLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.GroupBox gBoxMeterials;
        private System.Windows.Forms.ComboBox cBoxHonour;
        private System.Windows.Forms.TextBox txtHonour;
        private System.Windows.Forms.TextBox txtWpStone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSubSmall;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSubMed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubBig;
        private System.Windows.Forms.TextBox txtEnhance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOreha;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDisStone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDisEnhance;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDisProb;
        private System.Windows.Forms.TextBox txtDisOreha;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDisHonour;
        private System.Windows.Forms.TextBox txtDisGold;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDisSubSmall;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDisSubMed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDisSubBig;
        private System.Windows.Forms.TextBox txtArmBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Button btnSuccess;
        private System.Windows.Forms.Button btnFail;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtDisJangGi;
        private System.Windows.Forms.CheckBox cBoxResearch2;
        private System.Windows.Forms.CheckBox cBoxResearch1;
        private System.Windows.Forms.TextBox txtWpBook;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtArmStone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cBoxDetails;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox cBoxOpt;
    }
}

