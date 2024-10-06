namespace testform
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button btnPickFile1;
        private Button btnPickFile2;
        private Button btnPickFile3;
        private Button btnExecute;
        private ComboBox formatComboBox;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            btnPickFile1 = new Button();
            btnPickFile2 = new Button();
            btnPickFile3 = new Button();
            btnExecute = new Button();
            formatComboBox = new ComboBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 35);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 60);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(300, 35);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 108);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(300, 35);
            textBox3.TabIndex = 2;
            // 
            // btnPickFile1
            // 
            btnPickFile1.Location = new Point(318, 12);
            btnPickFile1.Name = "btnPickFile1";
            btnPickFile1.Size = new Size(75, 25);
            btnPickFile1.TabIndex = 3;
            btnPickFile1.Text = "Pick File 1";
            btnPickFile1.UseVisualStyleBackColor = true;
            btnPickFile1.Click += BtnPickFile1_Click;
            // 
            // btnPickFile2
            // 
            btnPickFile2.Location = new Point(318, 60);
            btnPickFile2.Name = "btnPickFile2";
            btnPickFile2.Size = new Size(75, 25);
            btnPickFile2.TabIndex = 4;
            btnPickFile2.Text = "Pick File 2";
            btnPickFile2.UseVisualStyleBackColor = true;
            btnPickFile2.Click += BtnPickFile2_Click;
            // 
            // btnPickFile3
            // 
            btnPickFile3.Location = new Point(318, 108);
            btnPickFile3.Name = "btnPickFile3";
            btnPickFile3.Size = new Size(75, 25);
            btnPickFile3.TabIndex = 5;
            btnPickFile3.Text = "Pick File 3";
            btnPickFile3.UseVisualStyleBackColor = true;
            btnPickFile3.Click += BtnPickFile3_Click;
            //// 
            //// formatComboBox
            //// 
            formatComboBox.Location = new Point(12, 158);
            formatComboBox.Name = "formatComboBox";
            formatComboBox.Size = new Size(300, 35);
            formatComboBox.TabIndex = 6;
            formatComboBox.Items.AddRange(new object[] { "json", "csv", "text" });
            formatComboBox.SelectedIndex = 0;
            //
            // btnExecute
            //
            btnExecute.Location = new Point(318, 158);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(75, 25); 
            btnExecute.TabIndex = 7;
            btnExecute.Text = "Execute";
            btnExecute.UseVisualStyleBackColor = true;
            btnExecute.BackColor = Color.Red;
            btnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // Form1
            // 
            ClientSize = new Size(600, 400);
            Controls.Add(btnPickFile3);
            Controls.Add(btnPickFile2);
            Controls.Add(btnPickFile1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(btnExecute);
            Controls.Add(formatComboBox);
            Name = "Form1";
            Text = "File Reader";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
