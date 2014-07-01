namespace SBPDemo
{
    partial class AgenciesDetailsForm
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
            this.listBoxCountries = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelHQ = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPIN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxModels = new System.Windows.Forms.ListBox();
            this.labelType = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCountries
            // 
            this.listBoxCountries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.listBoxCountries.FormattingEnabled = true;
            this.listBoxCountries.Location = new System.Drawing.Point(13, 201);
            this.listBoxCountries.Name = "listBoxCountries";
            this.listBoxCountries.Size = new System.Drawing.Size(199, 212);
            this.listBoxCountries.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Registered Models:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelHQ);
            this.groupBox2.Controls.Add(this.labelDate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 76);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Foundation";
            // 
            // labelHQ
            // 
            this.labelHQ.AutoSize = true;
            this.labelHQ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelHQ.Location = new System.Drawing.Point(203, 48);
            this.labelHQ.Name = "labelHQ";
            this.labelHQ.Size = new System.Drawing.Size(35, 13);
            this.labelHQ.TabIndex = 15;
            this.labelHQ.Text = "label9";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelDate.Location = new System.Drawing.Point(203, 22);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(35, 13);
            this.labelDate.TabIndex = 14;
            this.labelDate.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(126, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Headquarters:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Founded:";
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonClose.Location = new System.Drawing.Point(241, 420);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 50);
            this.buttonClose.TabIndex = 25;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.labelPIN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 78);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name and ID";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelName.Location = new System.Drawing.Point(204, 48);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 6;
            this.labelName.Text = "label6";
            // 
            // labelPIN
            // 
            this.labelPIN.AutoSize = true;
            this.labelPIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelPIN.Location = new System.Drawing.Point(204, 22);
            this.labelPIN.Name = "labelPIN";
            this.labelPIN.Size = new System.Drawing.Size(35, 13);
            this.labelPIN.TabIndex = 5;
            this.labelPIN.Text = "label5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Registration Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Agency Name:";
            // 
            // listBoxModels
            // 
            this.listBoxModels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.listBoxModels.FormattingEnabled = true;
            this.listBoxModels.Location = new System.Drawing.Point(219, 201);
            this.listBoxModels.Name = "listBoxModels";
            this.listBoxModels.Size = new System.Drawing.Size(202, 212);
            this.listBoxModels.TabIndex = 35;
            this.listBoxModels.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxModels_MouseDoubleClick);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(12, 180);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(35, 13);
            this.labelType.TabIndex = 36;
            this.labelType.Text = "label5";
            // 
            // AgenciesDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 482);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.listBoxModels);
            this.Controls.Add(this.listBoxCountries);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AgenciesDetailsForm";
            this.Text = "AgenciesDetailsForm";
            this.Load += new System.EventHandler(this.AgenciesDetailsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCountries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxModels;
        private System.Windows.Forms.Label labelHQ;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPIN;
        private System.Windows.Forms.Label labelType;
    }
}