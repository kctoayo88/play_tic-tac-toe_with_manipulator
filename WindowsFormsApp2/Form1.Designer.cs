namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button14 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.PosX = new System.Windows.Forms.TextBox();
            this.PosRX = new System.Windows.Forms.TextBox();
            this.PosRY = new System.Windows.Forms.TextBox();
            this.PosY = new System.Windows.Forms.TextBox();
            this.PosZ = new System.Windows.Forms.TextBox();
            this.PosRZ = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Jog_X = new System.Windows.Forms.Label();
            this.Jog_Y = new System.Windows.Forms.Label();
            this.Jog_Z = new System.Windows.Forms.Label();
            this.JogXup = new System.Windows.Forms.Button();
            this.JogXdown = new System.Windows.Forms.Button();
            this.JogYup = new System.Windows.Forms.Button();
            this.JogYdown = new System.Windows.Forms.Button();
            this.JogZup = new System.Windows.Forms.Button();
            this.JogZdown = new System.Windows.Forms.Button();
            this.Move2Target = new System.Windows.Forms.Button();
            this.Move2W = new System.Windows.Forms.Button();
            this.WorldXValue = new System.Windows.Forms.Label();
            this.WorldYValue = new System.Windows.Forms.Label();
            this.JogRXup = new System.Windows.Forms.Button();
            this.JogRX = new System.Windows.Forms.Label();
            this.JogRXdown = new System.Windows.Forms.Button();
            this.JogRY = new System.Windows.Forms.Label();
            this.JogRz = new System.Windows.Forms.Label();
            this.JogRYup = new System.Windows.Forms.Button();
            this.JogRYdown = new System.Windows.Forms.Button();
            this.JogRZup = new System.Windows.Forms.Button();
            this.JogRZdown = new System.Windows.Forms.Button();
            this.Reach = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Reach2DP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(218, 305);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(179, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(567, 305);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 25);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(22, 381);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(375, 338);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(427, 550);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(375, 168);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(427, 381);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(375, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 303);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "Camera Device";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 339);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 27);
            this.button3.TabIndex = 6;
            this.button3.Text = "Save Image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(839, 361);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(156, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Threshold";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(839, 390);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(156, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "Gaussium";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(839, 332);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(156, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "Gray";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(839, 303);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(156, 23);
            this.button8.TabIndex = 11;
            this.button8.Text = "Origin";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(839, 419);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(156, 23);
            this.button9.TabIndex = 13;
            this.button9.Text = "Countours";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(839, 447);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(156, 23);
            this.button10.TabIndex = 14;
            this.button10.Text = "Get Numbers";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(858, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "_";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(894, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "_";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(930, 518);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "_";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(944, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(858, 562);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "_";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(22, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(190, 23);
            this.comboBox2.TabIndex = 27;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("新細明體", 20F);
            this.button14.Location = new System.Drawing.Point(427, 12);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(340, 46);
            this.button14.TabIndex = 31;
            this.button14.Text = "Feeding Area";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(421, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 15);
            this.label9.TabIndex = 32;
            this.label9.Text = "X :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(420, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 15);
            this.label10.TabIndex = 33;
            this.label10.Text = "Y :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(421, 254);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 15);
            this.label11.TabIndex = 34;
            this.label11.Text = "Z :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(624, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 15);
            this.label12.TabIndex = 35;
            this.label12.Text = "RX :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(625, 223);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 15);
            this.label13.TabIndex = 36;
            this.label13.Text = "RY :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(625, 256);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 15);
            this.label14.TabIndex = 37;
            this.label14.Text = "RZ :";
            // 
            // PosX
            // 
            this.PosX.Location = new System.Drawing.Point(452, 183);
            this.PosX.Name = "PosX";
            this.PosX.Size = new System.Drawing.Size(100, 25);
            this.PosX.TabIndex = 38;
            this.PosX.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // PosRX
            // 
            this.PosRX.Location = new System.Drawing.Point(665, 183);
            this.PosRX.Name = "PosRX";
            this.PosRX.Size = new System.Drawing.Size(100, 25);
            this.PosRX.TabIndex = 39;
            // 
            // PosRY
            // 
            this.PosRY.Location = new System.Drawing.Point(665, 218);
            this.PosRY.Name = "PosRY";
            this.PosRY.Size = new System.Drawing.Size(100, 25);
            this.PosRY.TabIndex = 40;
            // 
            // PosY
            // 
            this.PosY.Location = new System.Drawing.Point(451, 218);
            this.PosY.Name = "PosY";
            this.PosY.Size = new System.Drawing.Size(100, 25);
            this.PosY.TabIndex = 41;
            // 
            // PosZ
            // 
            this.PosZ.Location = new System.Drawing.Point(451, 253);
            this.PosZ.Name = "PosZ";
            this.PosZ.Size = new System.Drawing.Size(100, 25);
            this.PosZ.TabIndex = 42;
            // 
            // PosRZ
            // 
            this.PosRZ.Location = new System.Drawing.Point(665, 254);
            this.PosRZ.Name = "PosRZ";
            this.PosRZ.Size = new System.Drawing.Size(100, 25);
            this.PosRZ.TabIndex = 43;
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("新細明體", 20F);
            this.button15.Location = new System.Drawing.Point(427, 117);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(340, 46);
            this.button15.TabIndex = 44;
            this.button15.Text = "Position";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(224, 14);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 45;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(309, 14);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(85, 23);
            this.Disconnect.TabIndex = 46;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Jog_X
            // 
            this.Jog_X.AutoSize = true;
            this.Jog_X.Font = new System.Drawing.Font("新細明體", 20F);
            this.Jog_X.Location = new System.Drawing.Point(32, 54);
            this.Jog_X.Name = "Jog_X";
            this.Jog_X.Size = new System.Drawing.Size(82, 34);
            this.Jog_X.TabIndex = 47;
            this.Jog_X.Text = "JogX";
            this.Jog_X.Click += new System.EventHandler(this.Jog_X_Click);
            // 
            // Jog_Y
            // 
            this.Jog_Y.AutoSize = true;
            this.Jog_Y.Font = new System.Drawing.Font("新細明體", 20F);
            this.Jog_Y.Location = new System.Drawing.Point(174, 54);
            this.Jog_Y.Name = "Jog_Y";
            this.Jog_Y.Size = new System.Drawing.Size(82, 34);
            this.Jog_Y.TabIndex = 48;
            this.Jog_Y.Text = "JogY";
            // 
            // Jog_Z
            // 
            this.Jog_Z.AutoSize = true;
            this.Jog_Z.Font = new System.Drawing.Font("新細明體", 20F);
            this.Jog_Z.Location = new System.Drawing.Point(298, 54);
            this.Jog_Z.Name = "Jog_Z";
            this.Jog_Z.Size = new System.Drawing.Size(79, 34);
            this.Jog_Z.TabIndex = 49;
            this.Jog_Z.Text = "JogZ";
            // 
            // JogXup
            // 
            this.JogXup.Location = new System.Drawing.Point(27, 99);
            this.JogXup.Name = "JogXup";
            this.JogXup.Size = new System.Drawing.Size(45, 44);
            this.JogXup.TabIndex = 50;
            this.JogXup.Text = "+";
            this.JogXup.UseVisualStyleBackColor = true;
            this.JogXup.Click += new System.EventHandler(this.JogXup_Click);
            // 
            // JogXdown
            // 
            this.JogXdown.Location = new System.Drawing.Point(78, 99);
            this.JogXdown.Name = "JogXdown";
            this.JogXdown.Size = new System.Drawing.Size(44, 44);
            this.JogXdown.TabIndex = 51;
            this.JogXdown.Text = "-";
            this.JogXdown.UseVisualStyleBackColor = true;
            this.JogXdown.Click += new System.EventHandler(this.JogXdown_Click);
            // 
            // JogYup
            // 
            this.JogYup.Location = new System.Drawing.Point(162, 99);
            this.JogYup.Name = "JogYup";
            this.JogYup.Size = new System.Drawing.Size(44, 44);
            this.JogYup.TabIndex = 52;
            this.JogYup.Text = "+";
            this.JogYup.UseVisualStyleBackColor = true;
            this.JogYup.Click += new System.EventHandler(this.JogYup_Click);
            // 
            // JogYdown
            // 
            this.JogYdown.Location = new System.Drawing.Point(212, 99);
            this.JogYdown.Name = "JogYdown";
            this.JogYdown.Size = new System.Drawing.Size(44, 44);
            this.JogYdown.TabIndex = 53;
            this.JogYdown.Text = "-";
            this.JogYdown.UseVisualStyleBackColor = true;
            this.JogYdown.Click += new System.EventHandler(this.JogYdown_Click);
            // 
            // JogZup
            // 
            this.JogZup.Location = new System.Drawing.Point(295, 99);
            this.JogZup.Name = "JogZup";
            this.JogZup.Size = new System.Drawing.Size(44, 44);
            this.JogZup.TabIndex = 55;
            this.JogZup.Text = "+";
            this.JogZup.UseVisualStyleBackColor = true;
            this.JogZup.Click += new System.EventHandler(this.JogZup_Click);
            // 
            // JogZdown
            // 
            this.JogZdown.Location = new System.Drawing.Point(345, 100);
            this.JogZdown.Name = "JogZdown";
            this.JogZdown.Size = new System.Drawing.Size(44, 44);
            this.JogZdown.TabIndex = 56;
            this.JogZdown.Text = "-";
            this.JogZdown.UseVisualStyleBackColor = true;
            this.JogZdown.Click += new System.EventHandler(this.JogZdown_Click);
            // 
            // Move2Target
            // 
            this.Move2Target.Location = new System.Drawing.Point(783, 15);
            this.Move2Target.Name = "Move2Target";
            this.Move2Target.Size = new System.Drawing.Size(200, 36);
            this.Move2Target.TabIndex = 58;
            this.Move2Target.Text = "Move To Target";
            this.Move2Target.UseVisualStyleBackColor = true;
            this.Move2Target.Click += new System.EventHandler(this.Move2Target_Click);
            // 
            // Move2W
            // 
            this.Move2W.Font = new System.Drawing.Font("新細明體", 20F);
            this.Move2W.Location = new System.Drawing.Point(427, 64);
            this.Move2W.Name = "Move2W";
            this.Move2W.Size = new System.Drawing.Size(340, 46);
            this.Move2W.TabIndex = 64;
            this.Move2W.Text = "Working Area";
            this.Move2W.UseVisualStyleBackColor = true;
            this.Move2W.Click += new System.EventHandler(this.Move2W_Click);
            // 
            // WorldXValue
            // 
            this.WorldXValue.AutoSize = true;
            this.WorldXValue.Location = new System.Drawing.Point(104, 390);
            this.WorldXValue.Name = "WorldXValue";
            this.WorldXValue.Size = new System.Drawing.Size(0, 15);
            this.WorldXValue.TabIndex = 69;
            // 
            // WorldYValue
            // 
            this.WorldYValue.AutoSize = true;
            this.WorldYValue.Location = new System.Drawing.Point(296, 390);
            this.WorldYValue.Name = "WorldYValue";
            this.WorldYValue.Size = new System.Drawing.Size(0, 15);
            this.WorldYValue.TabIndex = 70;
            // 
            // JogRXup
            // 
            this.JogRXup.Location = new System.Drawing.Point(27, 215);
            this.JogRXup.Name = "JogRXup";
            this.JogRXup.Size = new System.Drawing.Size(45, 41);
            this.JogRXup.TabIndex = 71;
            this.JogRXup.Text = "+";
            this.JogRXup.UseVisualStyleBackColor = true;
            this.JogRXup.Click += new System.EventHandler(this.JogRXup_Click);
            // 
            // JogRX
            // 
            this.JogRX.AutoSize = true;
            this.JogRX.Font = new System.Drawing.Font("新細明體", 20F);
            this.JogRX.Location = new System.Drawing.Point(24, 172);
            this.JogRX.Name = "JogRX";
            this.JogRX.Size = new System.Drawing.Size(103, 34);
            this.JogRX.TabIndex = 72;
            this.JogRX.Text = "JogRX";
            // 
            // JogRXdown
            // 
            this.JogRXdown.Location = new System.Drawing.Point(78, 215);
            this.JogRXdown.Name = "JogRXdown";
            this.JogRXdown.Size = new System.Drawing.Size(44, 41);
            this.JogRXdown.TabIndex = 73;
            this.JogRXdown.Text = "-";
            this.JogRXdown.UseVisualStyleBackColor = true;
            this.JogRXdown.Click += new System.EventHandler(this.JogRXdown_Click);
            // 
            // JogRY
            // 
            this.JogRY.AutoSize = true;
            this.JogRY.Font = new System.Drawing.Font("新細明體", 20F);
            this.JogRY.Location = new System.Drawing.Point(160, 172);
            this.JogRY.Name = "JogRY";
            this.JogRY.Size = new System.Drawing.Size(103, 34);
            this.JogRY.TabIndex = 74;
            this.JogRY.Text = "JogRY";
            // 
            // JogRz
            // 
            this.JogRz.AutoSize = true;
            this.JogRz.Font = new System.Drawing.Font("新細明體", 20F);
            this.JogRz.Location = new System.Drawing.Point(295, 172);
            this.JogRz.Name = "JogRz";
            this.JogRz.Size = new System.Drawing.Size(100, 34);
            this.JogRz.TabIndex = 75;
            this.JogRz.Text = "JogRZ";
            // 
            // JogRYup
            // 
            this.JogRYup.Location = new System.Drawing.Point(162, 215);
            this.JogRYup.Name = "JogRYup";
            this.JogRYup.Size = new System.Drawing.Size(44, 41);
            this.JogRYup.TabIndex = 76;
            this.JogRYup.Text = "+";
            this.JogRYup.UseVisualStyleBackColor = true;
            this.JogRYup.Click += new System.EventHandler(this.JogRYup_Click);
            // 
            // JogRYdown
            // 
            this.JogRYdown.Location = new System.Drawing.Point(213, 215);
            this.JogRYdown.Name = "JogRYdown";
            this.JogRYdown.Size = new System.Drawing.Size(43, 41);
            this.JogRYdown.TabIndex = 77;
            this.JogRYdown.Text = "-";
            this.JogRYdown.UseVisualStyleBackColor = true;
            this.JogRYdown.Click += new System.EventHandler(this.JogRYdown_Click);
            // 
            // JogRZup
            // 
            this.JogRZup.Location = new System.Drawing.Point(295, 215);
            this.JogRZup.Name = "JogRZup";
            this.JogRZup.Size = new System.Drawing.Size(44, 41);
            this.JogRZup.TabIndex = 78;
            this.JogRZup.Text = "+";
            this.JogRZup.UseVisualStyleBackColor = true;
            this.JogRZup.Click += new System.EventHandler(this.JogRZup_Click);
            // 
            // JogRZdown
            // 
            this.JogRZdown.Location = new System.Drawing.Point(345, 215);
            this.JogRZdown.Name = "JogRZdown";
            this.JogRZdown.Size = new System.Drawing.Size(44, 41);
            this.JogRZdown.TabIndex = 79;
            this.JogRZdown.Text = "-";
            this.JogRZdown.UseVisualStyleBackColor = true;
            this.JogRZdown.Click += new System.EventHandler(this.JogRZdown_Click);
            // 
            // Reach
            // 
            this.Reach.Location = new System.Drawing.Point(783, 117);
            this.Reach.Name = "Reach";
            this.Reach.Size = new System.Drawing.Size(200, 35);
            this.Reach.TabIndex = 80;
            this.Reach.Text = "Reach";
            this.Reach.UseVisualStyleBackColor = true;
            this.Reach.Click += new System.EventHandler(this.Reach_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(783, 172);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(47, 19);
            this.checkBox1.TabIndex = 81;
            this.checkBox1.Text = "Set";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Reach2DP
            // 
            this.Reach2DP.Location = new System.Drawing.Point(783, 64);
            this.Reach2DP.Name = "Reach2DP";
            this.Reach2DP.Size = new System.Drawing.Size(200, 38);
            this.Reach2DP.TabIndex = 82;
            this.Reach2DP.Text = "Reach To Desired Position";
            this.Reach2DP.UseVisualStyleBackColor = true;
            this.Reach2DP.Click += new System.EventHandler(this.Reach2DP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(894, 562);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 83;
            this.label4.Text = "_";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(930, 562);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 15);
            this.label5.TabIndex = 84;
            this.label5.Text = "_";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(930, 612);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 87;
            this.label8.Text = "_";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(894, 612);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 15);
            this.label15.TabIndex = 86;
            this.label15.Text = "_";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(858, 612);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 15);
            this.label16.TabIndex = 85;
            this.label16.Text = "_";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(783, 218);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(200, 61);
            this.button5.TabIndex = 88;
            this.button5.Text = "Go";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 744);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Reach2DP);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Reach);
            this.Controls.Add(this.JogRZdown);
            this.Controls.Add(this.JogRZup);
            this.Controls.Add(this.JogRYdown);
            this.Controls.Add(this.JogRYup);
            this.Controls.Add(this.JogRz);
            this.Controls.Add(this.JogRY);
            this.Controls.Add(this.JogRXdown);
            this.Controls.Add(this.JogRX);
            this.Controls.Add(this.JogRXup);
            this.Controls.Add(this.WorldYValue);
            this.Controls.Add(this.WorldXValue);
            this.Controls.Add(this.Move2W);
            this.Controls.Add(this.Move2Target);
            this.Controls.Add(this.JogZdown);
            this.Controls.Add(this.JogZup);
            this.Controls.Add(this.JogYdown);
            this.Controls.Add(this.JogYup);
            this.Controls.Add(this.JogXdown);
            this.Controls.Add(this.JogXup);
            this.Controls.Add(this.Jog_Z);
            this.Controls.Add(this.Jog_Y);
            this.Controls.Add(this.Jog_X);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.PosRZ);
            this.Controls.Add(this.PosZ);
            this.Controls.Add(this.PosY);
            this.Controls.Add(this.PosRY);
            this.Controls.Add(this.PosRX);
            this.Controls.Add(this.PosX);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Image Process";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;


        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox PosX;
        private System.Windows.Forms.TextBox PosRX;
        private System.Windows.Forms.TextBox PosRY;
        private System.Windows.Forms.TextBox PosY;
        private System.Windows.Forms.TextBox PosZ;
        private System.Windows.Forms.TextBox PosRZ;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Label Jog_X;
        private System.Windows.Forms.Label Jog_Y;
        private System.Windows.Forms.Label Jog_Z;
        private System.Windows.Forms.Button JogXup;
        private System.Windows.Forms.Button JogXdown;
        private System.Windows.Forms.Button JogYup;
        private System.Windows.Forms.Button JogYdown;
        private System.Windows.Forms.Button JogZup;
        private System.Windows.Forms.Button JogZdown;
        private System.Windows.Forms.Button Move2Target;
        private System.Windows.Forms.Button Move2W;
        private System.Windows.Forms.Label WorldXValue;
        private System.Windows.Forms.Label WorldYValue;
        private System.Windows.Forms.Button JogRXup;
        private System.Windows.Forms.Label JogRX;
        private System.Windows.Forms.Button JogRXdown;
        private System.Windows.Forms.Label JogRY;
        private System.Windows.Forms.Label JogRz;
        private System.Windows.Forms.Button JogRYup;
        private System.Windows.Forms.Button JogRYdown;
        private System.Windows.Forms.Button JogRZup;
        private System.Windows.Forms.Button JogRZdown;
        private System.Windows.Forms.Button Reach;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Reach2DP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button5;
    }
}

