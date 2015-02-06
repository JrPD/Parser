namespace AmazonBookInfo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.ListPathTb = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.URLLb = new System.Windows.Forms.ListBox();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSelect);
            this.panel1.Controls.Add(this.ListPathTb);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 58);
            this.panel1.TabIndex = 0;
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(475, 16);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(93, 23);
            this.BtnSelect.TabIndex = 1;
            this.BtnSelect.Text = "Select list file";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // ListPathTb
            // 
            this.ListPathTb.Location = new System.Drawing.Point(13, 16);
            this.ListPathTb.Name = "ListPathTb";
            this.ListPathTb.Size = new System.Drawing.Size(431, 20);
            this.ListPathTb.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // URLLb
            // 
            this.URLLb.FormattingEnabled = true;
            this.URLLb.Location = new System.Drawing.Point(12, 76);
            this.URLLb.Name = "URLLb";
            this.URLLb.Size = new System.Drawing.Size(602, 134);
            this.URLLb.TabIndex = 1;
            // 
            // RTB
            // 
            this.RTB.Location = new System.Drawing.Point(12, 223);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(602, 214);
            this.RTB.TabIndex = 2;
            this.RTB.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 449);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.URLLb);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.TextBox ListPathTb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox URLLb;
        private System.Windows.Forms.RichTextBox RTB;
    }
}

