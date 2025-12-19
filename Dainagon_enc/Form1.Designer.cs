namespace Dainagon_enc
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            txtCorpusName = new TextBox();
            txtInputPath = new TextBox();
            txtOutputPath = new TextBox();
            txtSampleId = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // txtCorpusName
            // 
            txtCorpusName.Location = new Point(271, 71);
            txtCorpusName.Name = "txtCorpusName";
            txtCorpusName.Size = new Size(230, 23);
            txtCorpusName.TabIndex = 0;
            // 
            // txtInputPath
            // 
            txtInputPath.Location = new Point(271, 127);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.Size = new Size(230, 23);
            txtInputPath.TabIndex = 1;
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(271, 184);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(230, 23);
            txtOutputPath.TabIndex = 2;
            // 
            // txtSampleId
            // 
            txtSampleId.Location = new Point(271, 246);
            txtSampleId.Name = "txtSampleId";
            txtSampleId.Size = new Size(230, 23);
            txtSampleId.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(101, 71);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            textBox1.Text = "CorpusName";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(101, 127);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 5;
            textBox2.Text = "InputPath";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(101, 184);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 6;
            textBox3.Text = "OutputPath";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(101, 246);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 7;
            textBox4.Text = "SampleId";
            // 
            // button1
            // 
            button1.Location = new Point(542, 126);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "選択";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(542, 351);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 9;
            button2.Text = "実行";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(txtSampleId);
            Controls.Add(txtOutputPath);
            Controls.Add(txtInputPath);
            Controls.Add(txtCorpusName);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCorpusName;
        private TextBox txtInputPath;
        private TextBox txtOutputPath;
        private TextBox txtSampleId;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button1;
        private Button button2;
    }
}
